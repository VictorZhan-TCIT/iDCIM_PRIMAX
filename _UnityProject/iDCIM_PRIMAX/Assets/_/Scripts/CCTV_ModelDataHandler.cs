using UnityEngine;

/// <summary>
/// 管理CCTV模型相關資料
/// </summary>
public class CCTV_ModelDataHandler : MonoBehaviour
{
    [Header(">>> CCTV資料")]
    [SerializeField] private SO_CCTV soCCTV;

    [Header(">>> 閃爍Shader Material")]
    [SerializeField] private Material material;

    private CCTV_LineIndicator cctvIndicator;

    /// <summary>
    /// CCTV資料
    /// </summary>
    public SO_CCTV soCCTVData => soCCTV;

    /// <summary>
    /// Shader是否啟動中
    /// </summary>
    public bool IsActivated { get; private set; }
    /// <summary>
    /// Shader是否被選取中
    /// </summary>
    public bool IsSelected { get; private set; }

    /// <summary>
    /// 設置圖標物件
    /// </summary>
    public void SetLineIndicator(CCTV_LineIndicator indicator) => cctvIndicator = indicator;

    private void Awake() => OnValidate();

    /// <summary>
    /// 設定Shader開啟與否
    /// </summary>
    public void SetActivated(bool isActivated)
    {
        IsActivated = isActivated;
        cctvIndicator.gameObject.SetActive(IsActivated);
        material.SetInt("_IsActivated", (IsActivated ? 1 : 0));
        if (IsActivated == false) SetSelected(false);
    }

    /// <summary>
    /// 是否被點選
    /// </summary>
    public void SetSelected(bool isSelected)
    {
        IsSelected = isSelected;
        cctvIndicator.IsOn = IsSelected;
        material.SetInt("_IsSelected", (IsSelected ? 1 : 0));
    }

    private void OnValidate()
    {
        material ??= GetComponent<MeshRenderer>().material;
    }
}
