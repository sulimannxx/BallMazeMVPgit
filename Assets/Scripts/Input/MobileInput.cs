using UnityEngine;

public class MobileInput : AbstractInput
{
    public override bool GetInputDetection()
    {
        return Input.touchCount > 0;
    }

    public override Vector3 CalculateInputPosition(bool cameraToScreenWorldPoint = false)
    {
        Vector3 inputPosition = Input.GetTouch(0).position;
        if (cameraToScreenWorldPoint)
        {
            return Camera.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, Camera.nearClipPlane));
        }

        return new Vector3(inputPosition.x, inputPosition.y, Camera.nearClipPlane);
    }
}