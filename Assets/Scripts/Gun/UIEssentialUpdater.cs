using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEssentialUpdater : MonoBehaviour
{
    public Image uiImage;

    [Header("Amunition Animation")]
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
        // uiImage.fillAmount = 1 - (current / max);
        if (_currenTween != null)
        {
            _currenTween.Kill();
        }
        uiImage.DOFillAmount(1 - (current / max), durationAnimation).SetEase(ease);
    }   

}