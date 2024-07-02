using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CCTV_UIManager : MonoBehaviour
{
    [Header(">>> CCTV列表")]
    [SerializeField] private CCTVList cctvList;

    [Header(">>> 左上角CCTV播放器 - 機房入口")]
    [SerializeField] private CCTV_Player cctvPlayer_Entrance;
    [Header(">>> 右下角CCTV播放器")]
    [SerializeField] private CCTV_Player cctvPlayer;
    [Header(">>> CCTV全螢幕播放器")]
    [SerializeField] private CCTV_Player fullScreenCCTVPlayer;

    [Header(">>> 當列表項目Toggle變化時Invoke")]
    public UnityEvent<SO_CCTV, bool> onToggleChanged;

    private CCTV_DataHandler dataHandler;

    private Dictionary<SO_CCTV, CCTV_Player> cctvPlayerDict { get; set; } = new Dictionary<SO_CCTV, CCTV_Player>();

    private void Awake()
    {
        //cctvList.onToggleChanged.AddListener(onToggleChanged.Invoke);
        //cctvList.onToggleChanged.AddListener(onToggleChangedHandler);

        cctvPlayer_Entrance.onClickScaleButton.AddListener(OnClickScaleButtonHandler);
        cctvPlayer.onClickScaleButton.AddListener(OnClickScaleButtonHandler);
    }

    private void onToggleChangedHandler(SO_CCTV soData, bool isOn)
    {
        cctvPlayer.soData = soData;
        cctvPlayer.gameObject.SetActive(isOn);
    }

    private void OnClickScaleButtonHandler(SO_CCTV soData)
    {
        fullScreenCCTVPlayer.soData = soData;
        fullScreenCCTVPlayer.transform.parent.gameObject.SetActive(true);
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
}
