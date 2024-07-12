using UnityEngine;
using VictorDev.Net.WebAPI;

public class WebAPIManager : MonoBehaviour
{
    [Header(">>>帳密登入")]
    [SerializeField] private WebAPI_Request request_SignIn;
    [Header(">>>取得所有DCR機櫃及內含設備")]
    [SerializeField] private WebAPI_Request request_GetAllDCRInfo;
    /*   [Header(">>>取得目標設備的COBIE資訊")]
     [SerializeField] private WebAPI_Request request_GetDeviceCOBIE;*/
    /*    [Header(">>>取得目標DCR機櫃底下的DCS列表")]
        [SerializeField] private WebAPI_Request requestSignIn;
        [Header(">>>依種類取得資產管理列表")]
        [SerializeField] private WebAPI_Request requestSignIn;*/

    [TextArea(1, 5)]
    [Header(">>> WebAPI 登入後取得的Token值")]
    [SerializeField] private string token;

    [ContextMenu(" - 帳密登入")]
    public void Sign_In()
    {
        Debug.Log($">>> [帳密登入] WebAPI Call: {request_SignIn.url}");
        WebAPI_Caller.SendRequest(request_SignIn, (responseCode, sourceData) =>
        {
            Debug.Log($"\t\tonSuccess [{responseCode}] - Token: {sourceData}");
            token = sourceData;
        });
    }

    [ContextMenu(" - 取得所有DCR機櫃及內含設備")]
    public void GetAllDCRInfo()
    {
        if (token == null)
        {
            Debug.LogWarning($"尚未事先取得Token!!");
            return;
        }

        request_GetAllDCRInfo.token = token;
        Debug.Log($">>> [取得所有DCR機櫃及內含設備] WebAPI Call: {request_GetAllDCRInfo.url}");
        WebAPI_Caller.SendRequest(request_GetAllDCRInfo, (responseCode, sourceData) =>
        {
            Debug.Log($"\t\tonSuccess [{responseCode}] - sourceData: {sourceData}");
        });
    }
}
