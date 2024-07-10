using UnityEngine;

/// <summary>
/// WebAPI主機資訊
/// </summary>
[CreateAssetMenu(fileName = "SO_WebAPIServerConfig", menuName = "ScriptableObject/WebAPI/WebAPI主機資訊")]
public class SO_WebAPIServerConfig : ScriptableObject
{
    [Header(">>> URL")]
    [SerializeField] private string url;

    [Header(">>> Port")]
    [SerializeField] private int port;

    public string URL => url;
    public int Port => port;

    /// <summary>
    /// 網址+埠號
    /// </summary>
    public string FullURL => $"{url}:{port}";

}
