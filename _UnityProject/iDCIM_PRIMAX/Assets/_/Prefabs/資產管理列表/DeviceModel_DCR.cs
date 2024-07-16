using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VictorDev.RevitUtils;

public class DeviceModel_DCR : DeviceModel<SO_DCR>
{

    /// <summary>
    /// 動態建立DCS模型
    /// </summary>
    public void CreateDeviceDCSfromDict(Dictionary<string, Texture> dcsTextureDictionary, Transform prefab)
    {
        soData.DCS_List.ForEach(soDCS =>
        {
            Transform device = RevitHandler.CreateDeviceDCSfromDict(soDCS, dcsTextureDictionary, prefab, this.transform);
            DeviceModel_DCSDCN modelDCSDCN = device.AddComponent<DeviceModel_DCSDCN>();
            modelDCSDCN.onToggleChanged.AddListener((deviceModel =>
            {
                onToggleChanged?.Invoke(deviceModel);
            }));
            modelDCSDCN.soData = soDCS;
        });
    }
}

