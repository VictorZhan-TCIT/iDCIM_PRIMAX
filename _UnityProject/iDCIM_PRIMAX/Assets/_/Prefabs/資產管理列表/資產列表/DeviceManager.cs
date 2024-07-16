using System.Collections.Generic;
using UnityEngine;
using VictorDev.Parser;

public class DeviceManager : MonoBehaviour
{
    [SerializeField] private List<Transform> dcsPrefabList;
    private Dictionary<string, Transform> dcsDictionary { get; set; } = new Dictionary<string, Transform>();

    [SerializeField] private Transform dcsPrefab;
    [SerializeField] private List<Texture> dcsTextureList;
    private Dictionary<string, Texture> dcsTextureDictionary { get; set; } = new Dictionary<string, Texture>();



    [Header(">>> 場景上模型 - DCR")]
    [SerializeField] private List<DeviceModel_DCR> modelDCRList;

    [Header(">>> 從WebAPI取得的DCR列表")]
    [SerializeField] private List<SO_DCR> soDCRList;

    public Panel_DeviceInfo panel_DeviceInfo;
    public Panel_DCR_RU panel_DCRInfo;

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
    /// <para>+ 父類別有T泛型，所以不能把子類別指派給父類別</para>
    /// <para>+ 可以用Interface來進行指派</para>
    /// <para>+ 將父類別物件轉型成子類別物件，即可抓取其子類別參數</para>
    /// </summary>
    private IDeviceModel currentSelectedModel = null;

    private void Awake()
    {
        //設定DCS Prefab Dictionary
        dcsPrefabList.ForEach(itemTrans =>
        {
            dcsDictionary[itemTrans.name] = itemTrans;
        });

        dcsTextureList.ForEach(texture =>
        {
            dcsTextureDictionary[texture.name] = texture;
        });

        //設定DCS點擊後，傳遞soData給資訊面板 
        modelDCRList.ForEach(deviceModelDCR =>
        {
            modelDcrDict[deviceModelDCR.elementId] = deviceModelDCR;
            deviceModelDCR.onToggleChanged.AddListener((deviceModel) =>
            {
                //取消上一個模型的選取狀態
                if (currentSelectedModel != null) currentSelectedModel.isSelected = false;
                currentSelectedModel = deviceModel;

                panel_DeviceInfo.gameObject.SetActive(true);
                panel_DCRInfo.gameObject.SetActive(true);

                //   panel_DeviceInfo.soDCR = (currentSelectedModel as DeviceModel_DCR).soData;
                // panel_DCRInfo.soDCR = (currentSelectedModel as DeviceModel_DCR).soData;

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

        //建立DCR資料List
        soDCRList = new List<SO_DCR>();
        dataSet_DCR.ForEach(dataSet =>
        {
            SO_DCR soDCR = ScriptableObject.CreateInstance<SO_DCR>();
            soDCR.SetSourceDataDict(dataSet);
            soDCRList.Add(soDCR);
        });

        //資料儲存給每個一DCR模型
        soDCRList.ForEach(soDCR =>
        {
            modelDcrDict[soDCR.elementId].soData = soDCR;
            modelDcrDict[soDCR.elementId].CreateDeviceDCSfromDict(dcsTextureDictionary, dcsPrefab);
        });
    }
}