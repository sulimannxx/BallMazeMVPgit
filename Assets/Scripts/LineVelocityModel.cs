using UnityEngine;

public class LineVelocityModel
{
    public Vector3 CalculateLineVelocity(Vector3 targetPoint, Vector3 startPoint)
    {
        Vector3 lineVelocity = targetPoint - startPoint;
        lineVelocity = new Vector3(lineVelocity.x, 0f, lineVelocity.z);
        return lineVelocity;
    }
}
