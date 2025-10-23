using DG.Tweening;
using UnityEngine;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderesToBlink;
    public SkinnedMeshRenderer skinnedMeshRenderer;

    public Color color = Color.red;
    public float duration = .1f;

    // private Color _defaultColor;

    private Tween _currentTween;

    private void OnValidate()
    {
        if(meshRenderesToBlink == null)
        {
            meshRenderesToBlink = GetComponent<MeshRenderer>();
        }
        if (skinnedMeshRenderer == null) 
        {
            skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        }
    }

    private void Start()
    {
        // _defaultColor = meshRenderesToBlink.material.GetColor("_EmissionColor");
    }

    [NaughtyAttributes.Button]
    public void Flash()
    {
        if (meshRenderesToBlink != null && !_currentTween.IsActive())
        {
            _currentTween = meshRenderesToBlink.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);
        }

        if (skinnedMeshRenderer != null && !_currentTween.IsActive())
        {
            _currentTween = skinnedMeshRenderer.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);
        }        
    }
}
