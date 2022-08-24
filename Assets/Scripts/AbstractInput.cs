using UnityEngine;

public abstract class AbstractInput
{
    private Camera _camera;

    protected Camera Camera => _camera;

    public void Init()
    {
        _camera = Camera.main;
    }
    public abstract bool GetInputDetection();
    public abstract Vector3 CalculateInputPosition(bool cameraToScreenWorldPoint = false);
}