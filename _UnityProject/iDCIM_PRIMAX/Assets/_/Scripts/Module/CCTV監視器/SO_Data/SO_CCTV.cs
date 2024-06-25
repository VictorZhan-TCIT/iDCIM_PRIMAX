using UnityEngine;

/// <summary>
/// CCTV鏈結網址
/// </summary>
[CreateAssetMenu(fileName = "SO_CCTV", menuName = ">>VictorDev<</Net/RTSP/SO_CCTV")]
public class SO_CCTV : ScriptableObject
{
    [Header(">>> CCTV鏈結網址")]
    [SerializeField] private string url = "live/video/record/ch1";

    [Header(">>> CCTV伺服器資訊")]
    [SerializeField] private SO_CCTV_ServerInfo cctvServerInfo;

    /// <summary>
    /// CCTV完整鏈結網址
    /// </summary>
    public string URL => $"{cctvServerInfo.URL}/{url}";
}
