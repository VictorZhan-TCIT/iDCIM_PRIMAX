using System.Collections.Generic;
using UnityEngine;

public class DeviceManager : MonoBehaviour
{
    [Header(">>> 設備 - DCR")]
    [SerializeField] private List<DeviceModel> dcrList;
    [Header(">>> 設備 - DCE")]
    [SerializeField] private List<DeviceModel> dceList;
    [Header(">>> 設備 - DCP")]
    [SerializeField] private List<DeviceModel> dcpList;


    [Header(">>> 設備列表")]
    [SerializeField] private DeviceList deviceList;

    /// <summary>
    /// soData資料列表
    /// </summary>
    private List<SO_Device> soDataList_DCR { get; set; } = new List<SO_Device>();
    private List<SO_Device> soDataList_DCE { get; set; } = new List<SO_Device>();
    private List<SO_Device> soDataList_DCP { get; set; } = new List<SO_Device>();


    private void Start()
    {
        dcrList.ForEach(deviceModel => { soDataList_DCR.Add(deviceModel.soDataInfo); });
        dceList.ForEach(deviceModel => { soDataList_DCE.Add(deviceModel.soDataInfo); });
        dcpList.ForEach(deviceModel => { soDataList_DCP.Add(deviceModel.soDataInfo); });

        SwitchToDCR();
    }

    public void SwitchToDCR()
    {
        deviceList.SetDataList(soDataList_DCR);
    }
}
