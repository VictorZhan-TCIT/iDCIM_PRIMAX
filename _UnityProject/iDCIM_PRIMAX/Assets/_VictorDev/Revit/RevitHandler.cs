using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace VictorDev.RevitUtils
{
    /// <summary>
    /// Revit相關處理
    /// </summary>
    public abstract class RevitHandler
    {
        public static string GetRevit_elementId(string value)
        {
            // 创建正则表达式来匹配方括号内的内容
            Regex regex = new Regex(@"\[(.*?)\]");

            // 通过正则表达式匹配
            Match match = regex.Match(value);

            // 提取匹配到的内容，match.Groups[0] = "[值]"
            if (match.Success) return match.Groups[1].Value;
            else return null;
        }

        /// <summary>
        /// 透過DCS設備的deviceId，取得其設備的高度U
        /// </summary>
        public static int GetHightUFromDeviceID(string deviceId)
        {
            string pattern = @"(\d+)(?=[^\d]*$)";
            return int.Parse(RegHandler(deviceId, pattern));
        }

        /// <summary>
        /// deviceId: "HWACOM+TPE+IDC+FL1+1+DCS++Server-1: Brocade-7X-1+98",
        /// </summary>
        public static string GetDCSTypeFromDeviceID(string deviceId)
        {
            string result = deviceId.Split(":")[1];
            return result.Split("+")[0].Trim();
        }

        private static string RegHandler(string deviceId, string pattern)
        {
            Match match = Regex.Match(deviceId, pattern);
            return (match.Success) ? (match.Groups[1].Value) : "";
        }

        /// <summary>
        /// 以機櫃U換算實際高度座標
        /// </summary>
        /// <param name="rackLocationU"></param>
        /// <returns></returns>
        public static Vector3 GetPositionFromRackU(int rackLocationU)
        {
            float posTop = 1.933f;  //機櫃頂部座標
            float posBottom = 0.08952563f;  //機櫃底部座標
            float eachU = (posTop - posBottom) / 42;    //總共42U
            return new Vector3(0, eachU * rackLocationU, 0);
        }

        /// <summary>
        /// 比對材質名稱Dictionary，建立DCS/DCN
        /// </summary>
        /// <param name="soDCS">ScriptableObject</param>
        /// <param name="dcsTextureDictionary">設備材質Dictionary</param>
        /// <param name="prefab">設備Prefab</param>
        /// <param name="container">放在哪個容器</param>
        public static Transform CreateDeviceDCSfromDict(SO_DCS soDCS, Dictionary<string, Texture> dcsTextureDictionary, Transform prefab, Transform container)
        {
            Transform result = null;
            string deviceType = RevitHandler.GetDCSTypeFromDeviceID(soDCS.deviceId);
            // 建立DCR內的DCS
            if (dcsTextureDictionary.ContainsKey(deviceType))
            {
                result = ObjectPoolManager.GetInstanceFromQueuePool(prefab, container);
                result.GetComponent<MeshRenderer>().material.mainTexture = dcsTextureDictionary[deviceType];
                result.name = deviceType;
                result.localScale = new Vector3(soDCS.width, soDCS.height - 0.5f, soDCS.length) * 0.01f;      // 高度-0.5f微調，避免重疊； 單位除100

                Vector3 pos = RevitHandler.GetPositionFromRackU(soDCS.rackLocation);
                pos.y += result.localScale.y * 0.5f; //物件Pivot為中心點，所以再加上自身高度*0.5f
                pos.z = -0.58f + result.localScale.z * 0.5f;      // 機櫃口座標0.58，減掉物件自身長度*0.5f
                result.transform.localPosition = pos;
                result.transform.localRotation = Quaternion.Euler(0, 180, 0);  //轉向
            }
            else Debug.Log($"沒有材質圖：{deviceType}");
            return result;
        }
    }
}
