using System.Collections.Generic;
using UnityEngine;
using VictorDev.Net.WebAPI;

public class WebAPIManager : MonoBehaviour
{
    [SerializeField] private WebAPI_RequestPackage requestLoing;

    [ContextMenu(" - 帳密登入")]
    void Login()
    {
        Debug.Log($"requestLoing.url: {requestLoing.url}");
        WebAPI_Handler.CallWebAPI(requestLoing, onSuccess, onFailed);
    }

    private void onSuccess(long responseCode, Dictionary<string, string> dictionary)
    {
        Debug.Log($"onSuccess [{responseCode}] - {dictionary.Count}");
    }

    private void onFailed(long responseCode, string msg)
    {
        Debug.Log($"onFailed [{responseCode}] - {msg}");
    }

}
