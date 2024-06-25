using UnityEngine;

/// <summary>
/// CCTV�쵲���}
/// </summary>
[CreateAssetMenu(fileName = "SO_CCTV", menuName = ">>VictorDev<</Net/RTSP/SO_CCTV")]
public class SO_CCTV : ScriptableObject
{
    [Header(">>> CCTV�쵲���}")]
    [SerializeField] private string url = "live/video/record/ch1";

    [Header(">>> CCTV���A����T")]
    [SerializeField] private SO_CCTV_ServerInfo cctvServerInfo;

    /// <summary>
    /// CCTV�����쵲���}
    /// </summary>
    public string URL => $"{cctvServerInfo.URL}/{url}";
}
