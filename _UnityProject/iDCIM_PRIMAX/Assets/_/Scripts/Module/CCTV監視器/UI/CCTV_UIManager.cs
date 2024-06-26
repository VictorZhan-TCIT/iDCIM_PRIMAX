using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CCTV_UIManager : MonoBehaviour
{

    [Header(">>> CCTV����Prefab")]
    [SerializeField] private CCTV_Player cctvPlayerPrefab;

    [Header(">>> �񼽩񾹪��e��")]
    [SerializeField] private ScrollRect containerOfPlayer;

    [Header(">>> CCTV�C��")]
    [SerializeField] private CCTVList cctvList;

    [Header(">>> CCTV�j�ئT����")]
    [SerializeField] private CCTV_Player fullScreenCCTVPlayer;

    [Header(">>> ��C����Toggle�ܤƮ�Invoke")]
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
            //�Y�ثe���s�b�����񾹡A�h�s�W
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
            Debug.Log($"�}��: {dataHandler.name} / RTSP: {dataHandler.RTSP_URL}");
            this.dataHandler = dataHandler;
        }
        else
        {
            Debug.Log($"����: {dataHandler.name}");
        }
    }

    [ContextMenu("Send Event")]
    private void onClickClosePanelHandler()
    {
        dataHandler.IsSelected = false;
    }
}
