using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class MoveFadeController : MonoBehaviour
{
    [Header(">>> 延遲(秒)")]
    [SerializeField] private float delay = 0f;
    [Header(">>> 動畫耗時(秒)")]
    [SerializeField] private float duration = 0.7f;
    [SerializeField] private Ease ease = Ease.OutQuart;

    [Header(">>> Position偏移值")]
    [SerializeField] private Vector2 offsetPos;

    [Header(">>> 是否進行Fade效果")]
    [SerializeField] private bool isFading = true;

    [Header(">>> 是否依照ChildIndex值來設定Delay")]
    [SerializeField] private bool isDelayByChildIndex = false;

    [Header(">>> OnEnabled動畫結束時")]
    public UnityEvent OnEnabledComplete;

    [Header(">>> UI組件")]
    [SerializeField] private CanvasGroup cg;
    [SerializeField] private RectTransform rectTrans;

    private Vector2 originalPos { get; set; }
    private Vector2 formPos { get; set; }

    private float currentAlpha { get; set; }
    private float targetDealy { get; set; }

    private void Awake()
    {
        originalPos = new Vector2(0, transform.localPosition.y);
        formPos = originalPos + offsetPos;
    }

    [ContextMenu("- Dotween: Play")]
    private void OnEnable()
    {
        targetDealy = isDelayByChildIndex ? delay * transform.GetSiblingIndex() : delay;

        // Fade
        if (isFading)
        {
            currentAlpha = (cg.alpha == 1) ? 0 : cg.alpha;
            cg.DOFade(1, duration).From(currentAlpha).SetEase(ease).SetDelay(targetDealy);
        }

        // LocalMove
        if (offsetPos.y == 0 && offsetPos.x != 0) 
            rectTrans.DOLocalMoveX(originalPos.x, duration).From(formPos.x).SetEase(ease).SetDelay(targetDealy).OnStart(OnStartHandler).OnComplete(OnCompleteHandler);
        else if (offsetPos.x == 0 && offsetPos.y != 0) 
            rectTrans.DOLocalMoveY(originalPos.y, duration).From(formPos.y).SetEase(ease).SetDelay(targetDealy).OnStart(OnStartHandler).OnComplete(OnCompleteHandler);
        else 
            rectTrans.DOLocalMove(originalPos, duration).From(formPos).SetEase(ease).SetDelay(targetDealy).OnStart(OnStartHandler).OnComplete(OnCompleteHandler);
    }

    private void OnStartHandler() => cg.interactable = false;
    private void OnCompleteHandler()
    {
        cg.interactable = true;
        OnEnabledComplete?.Invoke();
    }

    private void OnValidate()
    {
        cg ??= GetComponent<CanvasGroup>();
        rectTrans ??= GetComponent<RectTransform>();
    }
}
