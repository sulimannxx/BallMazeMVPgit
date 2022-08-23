using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LineRenderer))]

public class LineView : MonoBehaviour
{
    private Vector3 _ballPosition;
    private Vector3 _endPosition;
    private LineRenderer _line;

#if UNITY_ANDROID || UNITY_IOS
    private float _width;
    private float _height;
#endif

    public event UnityAction RequestBallPosition;

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();

#if UNITY_ANDROID || UNITY_IOS
        _width = (float)Screen.width / 2.0f;
        _height = (float)Screen.height / 2.0f;
#endif
    }

    private void Update()
    {

#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            RequestBallPosition?.Invoke();
            _endPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            _line.SetPosition(0, _ballPosition);
            _line.SetPosition(1, _endPosition);
        }
#endif

#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 position = touch.position;
            position.x = (position.x - _width) / _width;
            position.y = (position.y - _height) / _height;
            RequestBallPosition?.Invoke();
            _endPosition = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, Camera.main.nearClipPlane));
            _line.SetPosition(0, _ballPosition);
            _line.SetPosition(1, _endPosition);
        }
#endif
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
