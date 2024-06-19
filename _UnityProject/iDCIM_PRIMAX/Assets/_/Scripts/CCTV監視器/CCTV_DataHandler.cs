using UnityEngine;

/// <summary>
/// 管理CCTV模型相關資料
/// </summary>
public class CCTV_DataHandler : MonoBehaviour
{
    [Header(">>> CCTV資料")]
    [SerializeField] private SO_CCTV soCCTV;

    [Header(">>> 閃爍Shader Material")]
    [SerializeField] private Material shaderMaterial;

    [Header(">>> 圖標物件")]
    [SerializeField] private CCTV_LineIndicator cctvIndicator;


    /// <summary>
    /// CCTV資料
    /// </summary>
    public SO_CCTV soCCTVData => soCCTV;

    public Material ShaderMaterial { set => shaderMaterial = value; }

    /// <summary>
    /// 是否為啟動中
    /// </summary>
    public bool IsActivated
    {
        get => cctvIndicator.gameObject.activeSelf;
        set
        {
            cctvIndicator.gameObject.SetActive(value);
            shaderMaterial.SetInt("_IsActivated", (value ? 1 : 0));
            if (value == false) IsSelected = false;
        }
    }
    /// <summary>
    /// 是否為被選取
    /// </summary>
    public bool IsSelected
    {
        get => cctvIndicator.IsOn;
        set
        {
            cctvIndicator.IsOn = value;
            shaderMaterial.SetInt("_IsSelected", (IsSelected ? 1 : 0));
        }
    }

    private void OnValidate()
    {
        cctvIndicator ??= transform.GetChild(0).GetComponent<CCTV_LineIndicator>();
    }
}
