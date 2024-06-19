using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    [Header(">>>視窗標題")]
    [SerializeField] string titleText = "視窗標題";

    /// <summary>
    /// 當點擊關閉按鈕時
    /// </summary>
    [Header(">>>當點擊關閉按鈕時")]
    public UnityEvent onClickCloseButton;

    [Header(">>>UI組件")]
    [SerializeField] private TextMeshProUGUI txtTitle;
    [SerializeField] private Button btnClose;

    public string Title => txtTitle.text;
    public bool CloseButtonVisible { set => btnClose.gameObject.SetActive(value); }

    private void Awake() => btnClose.onClick.AddListener(onClickCloseButton.Invoke);
    private void OnValidate()
    {
        txtTitle ??= transform.Find("txtTitle").GetComponent<TextMeshProUGUI>();
        btnClose ??= txtTitle.transform.GetChild(0).GetComponent<Button>();
        txtTitle.SetText(titleText);

        name = $"Panel視窗 - {titleText}";
    }
}
