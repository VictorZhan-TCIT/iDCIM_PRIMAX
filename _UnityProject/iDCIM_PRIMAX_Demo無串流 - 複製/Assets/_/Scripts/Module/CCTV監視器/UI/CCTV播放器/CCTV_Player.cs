using UnityEngine;
using UnityEngine.Events;
//using VictorDev.Net.RTSP.UMPPlugin;

public class CCTV_Player : MonoBehaviour
{
    [SerializeField] private SO_CCTV _soData;

    //[SerializeField] private RtspScreen rtspScreen;
    [SerializeField] private MoveFadeController moveFadeController;
    [SerializeField] private PanelController panelController;

    /// <summary>
    /// 當點擊放大按鈕時
    /// </summary>
    [Header(">>>當點擊放大按鈕時")]
    public UnityEvent<SO_CCTV> onClickScaleButton;

    public SO_CCTV soData
    {
        set
        {
            _soData = value;
          //  if (gameObject.activeSelf) Play();
            Refresh();
        }
    }

    private void Awake()
    {
     //   moveFadeController.OnEnabledComplete.AddListener(Play);
        panelController.onClickScaleButton.AddListener(() => onClickScaleButton.Invoke(_soData));
    }

    /// <summary>
    /// 播放RTSP
    /// <para>+ 待OnEnabled：Move Fade後再進行播放</para>
    /// </summary>
   // public void Play() => Play(_soData.URL);
   // public void Play(string url) => rtspScreen.Play(url);

   // public void Stop() => rtspScreen.Stop(); 

    public void StopAndClose()
    {
        _soData.sourceToggle.isOn = false;
       // Stop();
        ObjectPoolManager.PushToPool<CCTV_Player>(this);
    }

    private void OnValidate()
    {
        moveFadeController ??= GetComponent<MoveFadeController>();
        panelController ??= GetComponent<PanelController>();

        Refresh();
    }

    private void Refresh()
    {
        if (_soData != null)
        {
            panelController.Title = $"{_soData.DeviceName}";
        }
    }
}
