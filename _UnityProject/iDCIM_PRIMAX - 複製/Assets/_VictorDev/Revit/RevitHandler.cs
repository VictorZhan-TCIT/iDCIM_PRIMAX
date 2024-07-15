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

        public static Vector3 GetPositionFromRackU(int rackLocationU)
        {
            float posTop = 1.933f;
            float posBottom = 0.08952563f;

            float eachU = (posTop - posBottom) / 42;
            return new Vector3(0, eachU * rackLocationU, 0);
        }
    }
}
