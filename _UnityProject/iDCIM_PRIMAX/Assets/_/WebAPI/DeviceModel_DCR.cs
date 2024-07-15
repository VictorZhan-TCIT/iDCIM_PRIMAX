using System.Collections.Generic;
using UnityEngine;
using VictorDev.RevitUtils;

public class DeviceModel_DCR : DeviceModel<SO_DCR>
{
    public void CreateDeviceDCSfromDict(Dictionary<string, Transform> dcsPrefabDictionary)
    {
      /*  soData.DCS_List.ForEach(soDCS =>
        {
            string deviceType = RevitHandler.GetDCSTypeFromDeviceID(soDCS.deviceId);

            // 建立DCR內的DCS
            if (dcsPrefabDictionary.ContainsKey(deviceType))
            {
                Transform dcs = ObjectPoolManager.GetInstanceFromQueuePool(dcsPrefabDictionary[deviceType], this.transform);
                dcs.localScale = new Vector3(soDCS.width, soDCS.height - 0.05f, soDCS.length) * 0.01f;

                Vector3 pos = RevitHandler.GetPositionFromRackU(soDCS.rackLocation);
                pos.y += dcs.localScale.y * 0.5f;
                pos.z = -0.26f;
                dcs.transform.localPosition = pos;
                dcs.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else Debug.Log($"沒有材質圖：{deviceType}");
        });*/
    }

    internal void CreateDeviceDCSfromDict(Dictionary<string, Texture> dcsTextureDictionary, Transform prefab)
    {
        soData.DCS_List.ForEach(soDCS =>
        {
            Transform device = RevitHandler.CreateDeviceDCSfromDict(soDCS, dcsTextureDictionary, prefab, this.transform);

         /*   string deviceType = RevitHandler.GetDCSTypeFromDeviceID(soDCS.deviceId);

            // 建立DCR內的DCS
            if (dcsTextureDictionary.ContainsKey(deviceType))
            {
                Transform dcs = ObjectPoolManager.GetInstanceFromQueuePool(prefab, this.transform);
                dcs.GetComponent<MeshRenderer>().material.mainTexture = dcsTextureDictionary[deviceType];
                dcs.name = deviceType;
                dcs.localScale = new Vector3(soDCS.width, soDCS.height - 0.5f, soDCS.length) * 0.01f;

                Vector3 pos = RevitHandler.GetPositionFromRackU(soDCS.rackLocation);
                pos.y += dcs.localScale.y * 0.5f;
                pos.z = -0.58f + dcs.localScale.z * 0.5f;
                dcs.transform.localPosition = pos;
                dcs.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else Debug.Log($"沒有材質圖：{deviceType}");*/
        });
    }
}

