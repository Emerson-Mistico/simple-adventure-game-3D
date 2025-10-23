using ESM.Core.Singleton;
using TMPro;
using UnityEngine;
using DG.Tweening;


[DefaultExecutionOrder(-999)]
public class MessageManager : Singleton<MessageManager>
{
    [Header("Mesh Settings")]
    public TextMeshProUGUI uiMainMessage;

    [Header("Messages default")]
    public string startMessage = "Start Game!";

    [Header("Animation")]
    public float fadeInDuration = 0.3f;
    public float fadeOutDuration = 0.4f;
    public Ease fadeEase = Ease.OutQuad;

    private string _msgNull = "";
    private Tween _currentTween;
    protected override void Awake()
    {
        base.Awake();

        if (uiMainMessage == null)
            uiMainMessage = GetComponentInChildren<TextMeshProUGUI>(true);

        if (uiMainMessage == null)
        {
            Debug.LogError("uiMainMessage not exists in MessageManager.");
            return;
        }

        ChangeToNull();
    }

    private void Start()
    {
        ChangeMessageTemporary(startMessage);
    }

    public void ChangeMessageTemporary(string msg, float timeToShow = 2.5f)
    {
        _currentTween?.Kill();
        uiMainMessage.alpha = 0f;
        uiMainMessage.text = msg;

        Sequence seq = DOTween.Sequence();
        seq.Append(uiMainMessage.DOFade(1f, fadeInDuration).SetEase(fadeEase));
        seq.AppendInterval(timeToShow);
        seq.Append(uiMainMessage.DOFade(0f, fadeOutDuration).SetEase(fadeEase));
        seq.OnComplete(() => uiMainMessage.text = _msgNull);

        _currentTween = seq;
    }

    public void ChangeToNull()
    {
        uiMainMessage.text = _msgNull;
        uiMainMessage.alpha = 0f;
    }

    [NaughtyAttributes.Button]
    public void MessageTest()
    {
        ChangeMessageTemporary("Hello There!", 2f);
    }
}
