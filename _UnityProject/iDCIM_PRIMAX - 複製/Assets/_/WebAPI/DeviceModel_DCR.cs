using System.Collections.Generic;
using UnityEngine;
using VictorDev.RevitUtils;

public class DeviceModel_DCR : DeviceModel<SO_DCR>
{
    public void CreateDeviceDCSfromDict(Dictionary<string, Transform> dcsPrefabDictionary)
    {
        soData.DCS_List.ForEach(soDCS =>
        {
            string deviceType = RevitHandler.GetDCSTypeFromDeviceID(soDCS.deviceId);
            Debug.Log($"Device: {deviceType} / {soDCS.rackLocation}");


            // 建立DCR內的DCS
            if (dcsPrefabDictionary.ContainsKey(deviceType))
            {
                Transform dcs = ObjectPoolManager.GetInstanceFromQueuePool(dcsPrefabDictionary[deviceType], this.transform);
                dcs.localScale = new Vector3(soDCS.width, soDCS.height, soDCS.length) * 0.01f;

                Vector3 pos = RevitHandler.GetPositionFromRackU(soDCS.rackLocation);
                pos.y += dcs.localScale.y * 0.5f;
                pos.z = -0.26f;
                dcs.transform.localPosition = pos;
                dcs.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        });
    }
}
