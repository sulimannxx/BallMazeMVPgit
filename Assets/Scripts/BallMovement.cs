using UnityEngine;

public class BallMovement
{
    public Vector3 CalculateVelocity(Vector3 currentPosition, Vector3 previousBallPosition)
    {
        Vector3 ballVelocity = (currentPosition - previousBallPosition) / Time.deltaTime;
        ballVelocity = Vector3.Lerp(ballVelocity, Vector3.zero, 0.005f);
        return ballVelocity;
    }
}
