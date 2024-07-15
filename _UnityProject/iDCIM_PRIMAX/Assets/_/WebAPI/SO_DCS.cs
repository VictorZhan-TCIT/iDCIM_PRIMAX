using System.Collections.Generic;
using UnityEngine;
using VictorDev.Parser;
using VictorDev.RevitUtils;

/// <summary>
/// [設備DCS] 伺服器主機
/// </summary>
public class SO_DCS : ScriptableObject
{
    /// <summary>
    /// 用於連接MQTT位址(選填)
    /// </summary>
    public string deviceCode => _sourceDataDict["deviceCode"];
    public string deviceId => _sourceDataDict["deviceId"];

    /// <summary>
    /// 位於機櫃第幾U
    /// </summary>
    public int rackLocation { get; private set; }
    /// <summary>
    /// 深度(公分)
    /// </summary>
    public float length { get; private set; }
    /// <summary>
    /// 寬度(公分)
    /// </summary>
    public float width { get; private set; }
    /// <summary>
    /// 高度(公分)
    /// </summary>
    public float height { get; private set; }
    /// <summary>
    /// 高度(RU)
    /// </summary>
    public float heightU => RevitHandler.GetHightUFromDeviceID(deviceId);

    /// <summary>
    /// DCS / DCN
    /// </summary>
    public string DeviceType => RevitHandler.GetDeviceTypeFromDeviceID(deviceId);

    private Dictionary<string, string> _sourceDataDict { get; set; }
    /// <summary>
    /// JSON解析後的資料
    /// </summary>
    public Dictionary<string, string> sourceDataDict
    {
        set
        {
            _sourceDataDict = value;
            rackLocation = int.Parse(_sourceDataDict["rackLocation"]);

            Dictionary<string, string> informationSet = JsonUtils.ParseJson(_sourceDataDict["information"]);
            length = float.Parse(informationSet["length"]);
            width = float.Parse(informationSet["width"]);
            height = float.Parse(informationSet["height"]);
        }
    }
}

