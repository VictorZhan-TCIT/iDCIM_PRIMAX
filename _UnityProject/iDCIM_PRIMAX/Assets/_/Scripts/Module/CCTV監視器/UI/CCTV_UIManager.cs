using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CCTV_UIManager : MonoBehaviour
{

    [Header(">>> CCTV播放器Prefab")]
    [SerializeField] private CCTV_Player cctvPlayerPrefab;

    [Header(">>> 放播放器的容器")]
    [SerializeField] private ScrollRect containerOfPlayer;

    [Header(">>> CCTV列表")]
    [SerializeField] private CCTVList cctvList;

    [Header(">>> CCTV大尺吋播放器")]
    [SerializeField] private CCTV_Player fullScreenCCTVPlayer;

    [Header(">>> 當列表項目Toggle變化時Invoke")]
    public UnityEvent<SO_CCTV, bool> onToggleChanged;

    private CCTV_DataHandler dataHandler;

    private Dictionary<SO_CCTV, CCTV_Player> cctvPlayerDict { get; set; } = new Dictionary<SO_CCTV, CCTV_Player>();

    private void Awake()
    {
        cctvList.onToggleChanged.AddListener(onToggleChanged.Invoke);
        cctvList.onToggleChanged.AddListener(OnListItemToggleChanged);
        ObjectPoolManager.PushToPool<CCTV_Player>(containerOfPlayer.content);
    }

    private void OnListItemToggleChanged(SO_CCTV soData, bool isOn)
    {
        if (isOn)
        {
            //若目前不存在此播放器，則新增
            if (cctvPlayerDict.ContainsKey(soData) == false)
            {
                CCTV_Player player = ObjectPoolManager.GetInstanceFromQueuePool<CCTV_Player>(cctvPlayerPrefab);
                player.soData = soData;
                player.transform.parent = containerOfPlayer.content;
                player.transform.SetAsFirstSibling();
                player.onClickScaleButton.AddListener(OnClickScaleButtonHandler);

                containerOfPlayer.verticalNormalizedPosition = 1;
                cctvPlayerDict[soData] = player;
            }
        }
        else
        {
            if (cctvPlayerDict.ContainsKey(soData))
            {
                cctvPlayerDict[soData].StopAndClose();
                cctvPlayerDict.Remove(soData);
            }
        }
    }

    private void OnClickScaleButtonHandler(SO_CCTV soData)
    {
        fullScreenCCTVPlayer.soData = soData;
        fullScreenCCTVPlayer.gameObject.SetActive(true);
    }

    public void SetDataHandler(CCTV_DataHandler dataHandler)
    {
        if (dataHandler.IsSelected)
        {
            Debug.Log($"開啟: {dataHandler.name} / RTSP: {dataHandler.RTSP_URL}");
            this.dataHandler = dataHandler;
        }
        else
        {
            Debug.Log($"關閉: {dataHandler.name}");
        }
    }

    [ContextMenu("Send Event")]
    private void onClickClosePanelHandler()
    {
        dataHandler.IsSelected = false;
    }
}
