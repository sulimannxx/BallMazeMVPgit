using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LineRenderer))]

public class LineView : MonoBehaviour
{
    private Vector3 _ballPosition;
    private Vector3 _endPosition;
    private LineRenderer _line;
    private AbstractInput _inputManager;

    public event UnityAction RequestBallPosition;

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (_inputManager.GetInputDetection())
        {
            RequestBallPosition?.Invoke();
            _endPosition = _inputManager.CalculateInputPosition(true);
            _line.SetPosition(0, _ballPosition);
            _line.SetPosition(1, _endPosition);
        }
    }

    public void InitView(AbstractInput input)
    {
        _inputManager = input;
    }

    public void SetBallPosition(Vector3 ballPosition)
    {
        _ballPosition = ballPosition;
    }

    public void EnableLine()
    {
        _line.gameObject.SetActive(true);
    }

    public void DisableLine()
    {
        _line.gameObject.SetActive(false);
    }
}