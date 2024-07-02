using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CCTV_UIManager : MonoBehaviour
{
    [Header(">>> CCTV�C��")]
    [SerializeField] private CCTVList cctvList;

    [Header(">>> ���W��CCTV���� - ���ФJ�f")]
    [SerializeField] private CCTV_Player cctvPlayer_Entrance;
    [Header(">>> �k�U��CCTV����")]
    [SerializeField] private CCTV_Player cctvPlayer;
    [Header(">>> CCTV���ù�����")]
    [SerializeField] private CCTV_Player fullScreenCCTVPlayer;
    [Header(">>> �ʱ��`������")]
    [SerializeField] private Button btnMonitor;
    [SerializeField] private CCTV_MonitorPage page_Monitor;

    [Header(">>> ��C����Toggle�ܤƮ�Invoke")]
    public UnityEvent<SO_CCTV, bool> onToggleChanged;

    private CCTV_DataHandler dataHandler;

    private Dictionary<SO_CCTV, CCTV_Player> cctvPlayerDict { get; set; } = new Dictionary<SO_CCTV, CCTV_Player>();

    private void Awake()
    {
        cctvList.onToggleChanged.AddListener(onToggleChanged.Invoke);
        cctvList.onToggleChanged.AddListener(onToggleChangedHandler);

        cctvPlayer_Entrance.onClickScaleButton.AddListener(PlayFullScreenCCTV);
        cctvPlayer.onClickScaleButton.AddListener(PlayFullScreenCCTV);

        btnMonitor.onClick.AddListener(() =>
        {
            page_Monitor.SetDataList(cctvList.SoDataList);
        });
    }

    private void onToggleChangedHandler(SO_CCTV soData, bool isOn)
    {
        if (isOn) cctvPlayer.Play(soData);
    }

    public void PlayFullScreenCCTV(SO_CCTV soData)
    {
        fullScreenCCTVPlayer.soData = soData;
        fullScreenCCTVPlayer.transform.parent.gameObject.SetActive(true);
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
}
