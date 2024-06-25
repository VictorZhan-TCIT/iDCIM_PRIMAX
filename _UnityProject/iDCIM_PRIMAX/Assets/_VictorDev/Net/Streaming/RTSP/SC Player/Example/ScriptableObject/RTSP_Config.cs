using UnityEngine;

/// <summary>
///  Uri網址設定檔Config
/// </summary>
[CreateAssetMenu(fileName = "RTSP設定檔", menuName = ">>VictorDev<</Net/RTSP/Uri網址設定檔Config")]
public class RTSP_Config : ScriptableObject
{
    //rtsp://root:TCIT5i2020@192.168.0.12/live1s2.sdp
    [Header(">>> RTSP網址Uri")]
    [SerializeField] private string uri;

    public string Uri => uri;
}
