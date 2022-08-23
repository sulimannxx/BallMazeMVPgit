using UnityEngine;

public class BallRotationView : MonoBehaviour
{
    public void Rotate(Vector3 velocity)
    {
        if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.z))
        {
            transform.Rotate(new Vector3(Mathf.Abs(velocity.x), 0, 0), Space.Self);
        }
        else
        {
            transform.Rotate(new Vector3(Mathf.Abs(velocity.z), 0, 0), Space.Self);
        }
    }

    public void ChangeDirection(Vector3 velocity)
    {
        transform.localRotation = Quaternion.LookRotation(new Vector3(velocity.x, transform.rotation.y, velocity.z));
    }
}
