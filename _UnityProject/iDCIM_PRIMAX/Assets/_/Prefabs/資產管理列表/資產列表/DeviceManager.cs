using System.Collections.Generic;
using UnityEngine;
using VictorDev.Parser;

public class DeviceManager : MonoBehaviour
{
    [Header(">>> 場景上模型 - DCR")]
    [SerializeField] private List<DeviceModel_DCR> modelDCRList;

    [Header(">>> 從WebAPI取得的DCR列表")]
    [SerializeField] private List<SO_DCR> soDCRList;


    public Panel_DeviceInfo panel_DeviceInfo;
    public Panel_DCRInfo panel_DCRInfo;

    /*   [Header(">>> 設備列表")]
       [SerializeField] private DeviceList deviceList;

       /// <summary>
       /// soData資料列表
       /// </summary>
       private List<SO_Device> soDataList_DCR { get; set; } = new List<SO_Device>();
       private List<SO_Device> soDataList_DCE { get; set; } = new List<SO_Device>();
       private List<SO_Device> soDataList_DCP { get; set; } = new List<SO_Device>();


       private void Start()
       {
           SwitchToDCR();
       }

   *  public void SwitchToDCR()
       {
           deviceList.SetDataList(soDataList_DCR);
       }*/

    private Dictionary<string, DeviceModel_DCR> modelDcrDict = new Dictionary<string, DeviceModel_DCR>();

    /// <summary>
    /// 上一次點選的設備模型
    /// </summary>
    private DeviceModel_DCR currentSelectedModel = null;

    private void Awake()
    {
        modelDCRList.ForEach(modelDCR =>
        {
            modelDcrDict[modelDCR.elementId] = modelDCR;
            modelDCR.onToggleChanged.AddListener((deviceModel) =>
            {
                if (currentSelectedModel != null) currentSelectedModel.isSelected = false;
                currentSelectedModel = modelDCR;

                panel_DeviceInfo.soDCR = currentSelectedModel.soData;
                panel_DCRInfo.soDCR = currentSelectedModel.soData;

                panel_DeviceInfo.gameObject.SetActive(true);
                panel_DCRInfo.gameObject.SetActive(true);
            });
        });
    }


    /// <summary>
    /// 取得WebAP資料後，設置給每一台DCR
    /// </summary>
    public void Parse_AllDCRInfo(string jsonString)
    {
        // 解析JSON
        List<Dictionary<string, string>> dataSet_DCR = JsonUtils.ParseJsonArray(jsonString);

        soDCRList = new List<SO_DCR>();
        dataSet_DCR.ForEach(dataSet =>
        {
            SO_DCR soDCR = ScriptableObject.CreateInstance<SO_DCR>();
            soDCR.sourceDataDict = dataSet;
            soDCRList.Add(soDCR);
        });

        soDCRList.ForEach(soDCR =>
        {
            modelDcrDict[soDCR.elementId].soData = soDCR;
        });
    }
}
