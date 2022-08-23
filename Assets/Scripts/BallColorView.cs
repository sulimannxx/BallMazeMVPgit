using System.Collections;
using UnityEngine;

public class BallColorView : MonoBehaviour
{
    private const float TimeMultiplier = 3f; 

    private readonly Color _defaultColor = Color.white;

    [SerializeField] private Color _targetColor;

    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void FlashColor()
    {
        StartCoroutine(SetDefaultColor());
    }

    private IEnumerator SetRedColor()
    {
        float time = 0f;

        while (_meshRenderer.material.color != _targetColor)
        {
            _meshRenderer.material.color = Color.Lerp(_defaultColor, _targetColor, time);
            time += Time.deltaTime * TimeMultiplier;
            yield return null;
        }
    }

    private IEnumerator SetDefaultColor()
    {
        yield return SetRedColor();

        float time = 0f;

        while (_meshRenderer.material.color != _defaultColor)
        {
            _meshRenderer.material.color = Color.Lerp(_targetColor, _defaultColor, time);
            time += Time.deltaTime * TimeMultiplier;
            yield return null;
        }
    }
}
