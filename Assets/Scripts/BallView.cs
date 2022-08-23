using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private const int PlankCollisionLayer = 6;

    [SerializeField] private ParticleSystem _wallHitEffect;

    private Vector3 _ballVelocity;
    private Vector3 _previousBallPosition;
    private Ray _ray;
    private RaycastHit _raycastHit;

    public event Action<Vector3, Vector3> RequestNewVelocity;
    public event Action<Vector3> RequestRotation;
    public event Action MouseClickedDown;
    public event Action MouseClickedUp;
    public event Action<Vector3, Vector3> WallIsHit;
    public event Action<Vector3> VelocityChanged;

    public Vector3 Position { get; private set; }

    private void Start()
    {
        _previousBallPosition = transform.position;
    }

    private void Update()
    {
        transform.Translate(_ballVelocity * Time.deltaTime);
        RequestRotation?.Invoke(_ballVelocity);
        RequestNewVelocity?.Invoke(transform.position, _previousBallPosition);
        _previousBallPosition = transform.position;
        Position = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == PlankCollisionLayer)
        {
            var particle = Instantiate(_wallHitEffect, collision.contacts[0].point, Quaternion.LookRotation(-transform.position));
            Destroy(particle.gameObject, 5f);
            WallIsHit?.Invoke(_ballVelocity, collision.contacts[0].normal);
            VelocityChanged?.Invoke(_ballVelocity);
        }
    }

    public void SetVelocity(Vector3 velocity)
    {
        _ballVelocity = velocity;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        MouseClickedDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        MouseClickedUp?.Invoke();
        _raycastHit = new RaycastHit();
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray.origin, _ray.direction, out _raycastHit, Mathf.Infinity))
        {
            _ballVelocity = (_raycastHit.point - transform.position);
            _ballVelocity = new Vector3(_ballVelocity.x, 0f, _ballVelocity.z);
        }

        VelocityChanged?.Invoke(_ballVelocity);
    }
}
