using UnityEngine;

/// <summary>
/// SO資產(DCR、DCE、DCP)
/// </summary>
[CreateAssetMenu(fileName = "SO_Device", menuName = "資產列表/SO_Device")]
public class SO_Device : ScriptableObject
{
    [Header(">>> 設備類別")]
    [SerializeField] private string deviceType;

    [Header(">>> 設備簡碼")]
    [SerializeField] private string deviceNo;

    [Header(">>> 設備類別")]
    [SerializeField] private string deviceName = "0005 系統Server";

    [Header(">>> 設備編碼")]
    [SerializeField] private string deviceCode = "TG/TPE/IDC/01F/WE/Schneider-ER8202/153953";

    [Header(">>> 模型編碼")]
    [SerializeField] private string modelCode;

    [Header(">>> 系統")]
    [SerializeField] private string systemName;

    [Header(">>> 型號")]
    [SerializeField] private string modelType;

    [Header(">>> 廠牌")]
    [SerializeField] private string brand;

    public DeviceListItem deviceListItem { get; set; }

    public string DeviceName => deviceName;
    public string DeviceCode => deviceCode;
}
