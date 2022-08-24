using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private const int PlankCollisionLayer = 6;
    private const float DestroyDelay = 5f;

    [SerializeField] private ParticleSystem _wallHitEffect;

    private Vector3 _ballVelocity;
    private Vector3 _previousBallPosition;
    private Ray _ray;
    private RaycastHit _raycastHit;
    private Camera _camera;
    private AbstractInput _inputManager;

    public event Action<Vector3, Vector3> RequestNewVelocity;
    public event Action<Vector3> RequestRotation;
    public event Action MouseClickedDown;
    public event Action MouseClickedUp;
    public event Action<Vector3, Vector3> WallIsHit;
    public event Action<Vector3> VelocityChanged;
    public event Action<Vector3, Vector3> RequestLineVelocity;

    public Vector3 Position { get; private set; }

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        _raycastHit = new RaycastHit();
        _previousBallPosition = transform.position;
        Position = transform.position;
    }

    private void Update()
    {
        if (_ballVelocity != Vector3.zero)
        {
            transform.Translate(_ballVelocity * Time.deltaTime);
            RequestRotation?.Invoke(_ballVelocity);
            RequestNewVelocity?.Invoke(transform.position, _previousBallPosition);
            _previousBallPosition = transform.position;
            Position = transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == PlankCollisionLayer)
        {
            ContactPoint contactPoint = collision.contacts[0];
            ParticleSystem particle = Instantiate(_wallHitEffect, contactPoint.point, Quaternion.LookRotation(-transform.position));
            Destroy(particle.gameObject, DestroyDelay);
            WallIsHit?.Invoke(_ballVelocity, contactPoint.normal);
            VelocityChanged?.Invoke(_ballVelocity);
        }
    }

    public void Init(AbstractInput input)
    {
        _inputManager = input;
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
        _ray = _camera.ScreenPointToRay(_inputManager.CalculateInputPosition());

        if (Physics.Raycast(_ray.origin, _ray.direction, out _raycastHit, Mathf.Infinity))
        {
            RequestLineVelocity?.Invoke(_raycastHit.point, transform.position);
        }

        VelocityChanged?.Invoke(_ballVelocity);
    }
}
