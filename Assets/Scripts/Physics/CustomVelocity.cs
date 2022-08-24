using UnityEngine;

public static class CustomVelocity
{
    private const float InterpolationRatio = 0.005f;

    public static Vector3 ReflectVelocity(Vector3 ballVelocity, Vector3 contactPointNormal)
    {
        return Vector3.Reflect(ballVelocity, contactPointNormal);
    }
    public static Vector3 CalculateVelocity(Vector3 currentPosition, Vector3 previousBallPosition)
    {
        Vector3 ballVelocity = (currentPosition - previousBallPosition) / Time.deltaTime;
        ballVelocity = Vector3.Lerp(ballVelocity, Vector3.zero, InterpolationRatio);
        return ballVelocity;
    }
}
