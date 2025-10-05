using DG.Tweening;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderesToBlink;

    public Color color = Color.red;
    public float duration = .1f;

    private Color _defaultColor;

    private Tween _currentTween;

    private void Start()
    {
        _defaultColor = meshRenderesToBlink.material.GetColor("_EmissionColor");
    }

    [NaughtyAttributes.Button]
    public void Flash()
    {
        if (!_currentTween.IsActive())
        {
            _currentTween = meshRenderesToBlink.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);
        }
        
    }

}
