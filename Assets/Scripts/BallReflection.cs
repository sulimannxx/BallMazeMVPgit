using UnityEngine;

public class BallReflection
{
    public Vector3 ReflectVelocity(Vector3 ballVelocity, Vector3 contactPointNormal)
    {
        Vector3 direction = Vector3.Reflect(ballVelocity, contactPointNormal);
        return direction;
    }
}
