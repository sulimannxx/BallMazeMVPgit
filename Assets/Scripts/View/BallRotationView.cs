using UnityEngine;

public class BallRotationView : MonoBehaviour
{
    public void Rotate(Vector3 velocity)
    {
        float velocityX = Mathf.Abs(velocity.x);
        float velocityZ = Mathf.Abs(velocity.z);
        transform.Rotate(new Vector3(velocityX > velocityZ ? velocityX : velocityZ, 0, 0), Space.Self);
    }

    public void ChangeDirection(Vector3 velocity)
    {
        transform.localRotation = Quaternion.LookRotation(new Vector3(velocity.x, transform.rotation.y, velocity.z));
    }
}
