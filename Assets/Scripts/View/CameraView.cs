using UnityEngine;

public class CameraView : MonoBehaviour
{
    [SerializeField] private BallView _ballView;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(_ballView.transform.position.x + 3.5f, transform.position.y, _ballView.transform.position.z), 0.015f);
    }
}
