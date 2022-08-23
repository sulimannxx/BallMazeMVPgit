using System.Collections;
using UnityEngine;

public class BallCompressionView : MonoBehaviour
{
    private readonly Vector3 _defaultScale = Vector3.one;
    private readonly Vector3 _compressedScale = new(1f, 1f, 0.33f);
    private Transform _ballTransform;

    private void Awake()
    {
        _ballTransform = GetComponent<Transform>();
    }

    public void CompressBall()
    {
        if (_ballTransform.localScale == _defaultScale)
        {
            StartCoroutine(Compress());
        }
    }

    private IEnumerator Compress()
    {
        while (_ballTransform.localScale.z > 0.4f)
        {
            _ballTransform.localScale = Vector3.Lerp(_ballTransform.localScale, _compressedScale, 0.2f);
            yield return null;
        }

        while (_ballTransform.localScale.z < 0.98f)
        {
            _ballTransform.localScale = Vector3.Lerp(_ballTransform.localScale, _defaultScale, 0.2f);
            yield return null;
        }

        _ballTransform.localScale = _defaultScale;
    }
}
