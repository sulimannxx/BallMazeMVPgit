using UnityEngine;

public class DesktopInput : AbstractInput
{
    public override bool GetInputDetection()
    {
        return Input.GetMouseButton(0);
    }

    public override Vector3 CalculateInputPosition(bool cameraToScreenWorldPoint = false)
    {
        if (cameraToScreenWorldPoint)
        {
           return Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.nearClipPlane));
        }

        return new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.nearClipPlane);
    }
}