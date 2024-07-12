using System.Collections.Generic;
using UnityEngine;
using VictorDev.Parser;

/// <summary>
/// [設備DCR] 機櫃
/// </summary>
public class SO_DCR : ScriptableObject
{
    public string elementId => _sourceDataDict["elementId"];
    public string deviceCode => _sourceDataDict["deviceCode"];
    public string code => _sourceDataDict["code"];
    public string buildingCode => _sourceDataDict["buildingCode"];
    public string area => _sourceDataDict["area"];
    public string floor => _sourceDataDict["floor"];
    public string system => _sourceDataDict["system"];
    public string type => _sourceDataDict["type"];
    public string desc => _sourceDataDict["desc"];
    public string deviceId => _sourceDataDict["deviceId"];
    public string manufacturer => _sourceDataDict["manufacturer"];
    public string modelnumber => _sourceDataDict["modelnumber"];
    public string useType => _sourceDataDict["useType"];
    public string assetCode => _sourceDataDict["assetCode"];
    public string mode => _sourceDataDict["mode"];
    public string needToInsp => _sourceDataDict["needToInsp"];

    /// <summary>
    /// information欄位
    /// </summary>
    public float length { get; private set; }
    public float width { get; private set; }
    public float height { get; private set; }
    public int heightU { get; private set; }
    public int watt { get; private set; }
    public int weight { get; private set; }

    /// <summary>
    /// 底下的DCS列表
    /// </summary>
    public List<SO_DCS> DCS_List { get; private set; }


    private Dictionary<string, string> _sourceDataDict { get; set; }
    /// <summary>
    /// JSON解析後的資料
    /// </summary>
    public Dictionary<string, string> sourceDataDict
    {
        set
        {
            _sourceDataDict = value;
            Dictionary<string, string> informationSet = JsonUtils.ParseJson(_sourceDataDict["information"]);
            length = float.Parse(informationSet["length"]);
            width = float.Parse(informationSet["width"]);
            height = float.Parse(informationSet["height"]);
            heightU = int.Parse(informationSet["heightU"]);
            watt = int.Parse(informationSet["watt"]) + Random.Range(10000, 30000);
            weight = int.Parse(informationSet["weight"]) + Random.Range(1000, 2000);

            List<Dictionary<string, string>> dcsSet = JsonUtils.ParseJsonArray(_sourceDataDict["contains"]);
            DCS_List = new List<SO_DCS>();
            dcsSet.ForEach(dataSet =>
            {
                SO_DCS soDCS = ScriptableObject.CreateInstance<SO_DCS>();
                soDCS.sourceDataDict = dataSet;
                DCS_List.Add(soDCS);
            });
        }
    }
}
