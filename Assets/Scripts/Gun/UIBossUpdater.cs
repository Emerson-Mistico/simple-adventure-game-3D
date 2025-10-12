using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBossUpdater : MonoBehaviour
{
    public Image uiImage;

    [Header("Boss life Animation")]
    public float durationAnimation = 0.1f;
    public Ease ease = Ease.OutBack;

    private Tween _currenTween;

    private void OnValidate()
    {
        if(uiImage == null)
        {
            uiImage = GetComponent<Image>();
        }
    }

    public void UpdateValue(float f)
    {        
        uiImage.fillAmount = f;        
    }

    public void UpdateValue(float max, float current)
    {
        if (_currenTween != null)
        {
            _currenTween.Kill();
        }

        // A barra agora começa cheia e vai esvaziando conforme perde vida
        float ratio = Mathf.Clamp01(current / max);
        _currenTween = uiImage.DOFillAmount(ratio, durationAnimation).SetEase(ease);
    }   

}