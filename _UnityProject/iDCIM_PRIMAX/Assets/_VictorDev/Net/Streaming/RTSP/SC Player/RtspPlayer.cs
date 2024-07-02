using Sttplay.MediaPlayer;
using System;
using UnityEngine;
using Debug = VictorDev.Common.DebugHandler;

namespace VictorDev.Net.RTSP.SCPlayerPlugin
{
    /// <summary>
    /// rtsp 視訊串流 Player
    /// <para> + 使用RawImage，需放在Canvas下</para>
    /// </summary>
    public class RtspPlayer : MonoBehaviour
    {
        [Header(">>> rtsp 網址Uri")]
        public string uri;

        [Header(">>> 尺吋長寬")]
        public Vector2 size = new(720, 480);

        [Header(">>> 是否一開始就自動播放")]
        public bool isAutoPlay = true;

        /// <summary>
        /// 當成功接收rtsp串流影像時
        /// </summary>
        public Action<Texture> onRenderFrameEvent;

        private SCUGUIRenderer scRenderer;
        private UnitySCPlayerPro scPlayer;
        private MediaType openMode { get; set; } = MediaType.Link;
        private string options { get; set; } = "packet_cache 1; open_timeout 5000; fflags nobuffer";

        private void Awake()
        {
            GetComponent<RectTransform>().sizeDelta = size;

            scRenderer = GetComponentInChildren<SCUGUIRenderer>();
            // 發送事件
            //scRenderer.onRenderFrameEvent += (texture) => onRenderFrameEvent?.Invoke(texture);

            scPlayer = scRenderer.GetComponentInChildren<UnitySCPlayerPro>();
            scPlayer.options = options;
            if (isAutoPlay && string.IsNullOrEmpty(uri) == false)
            {
                Debug.Log($">>> RtspPlayer AutoPlay: {uri}");
                OpenAndPlay(uri);
            }
        }

        /// <summary>
        /// 開啟
        /// </summary>
        public void Open(string uri = null)
        {
            this.uri = uri;
            scPlayer.Open(openMode, uri);
            LogMsg($"Open uri: {uri}");
        }

        public void Play()
        {
            scPlayer.Play();
            LogMsg($"Play uri: {uri}");
        }

        public void OpenAndPlay(string uri = null)
        {
            if(uri != null) this.uri = uri;
            Open(this.uri);
            Play();
            LogMsg($"OpenAndPlay uri: {this.uri}");
        }
        public void Pause()
        {
            scPlayer.Pause();
            LogMsg($"Pause");
        }

        public void Close()
        {
            scPlayer.Close();
            LogMsg($"Close");
        }

        public void ReplayPaused()
        {
            scPlayer.Replay(true);
            LogMsg($"ReplayPaused");
        }

        public void ReplayPlay()
        {
            scPlayer.Replay(false);
            LogMsg($"ReplayPlay");
        }

        private void LogMsg(string msg) => Debug.Log($"[rtsp player] >>> {msg}");
    }
}
