using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PostRequest : MonoBehaviour
{
    public string url = "https://example.com/api/login";
    public string account = "TCIT2024";
    public string password = "123";

    public string token;
    

    [ContextMenu("Login")]
    void Start()
    {
        StartCoroutine(PostData());
    }

    IEnumerator PostData()
    {

        /*
        WWWForm form = new WWWForm();
        form.AddField("account", account);
        form.AddField("pw", password);*/

        // UnityWebRequest www = UnityWebRequest.Post(url, form);

        url = "http://220.135.33.221:5053/api/Eqpt/RackContain?BuildingCode=TPE&DeviceCode=HWACOM/TPE/IDC/FL1/DCR/Schneider-ER8222搭電源/Rack1";
        UnityWebRequest www = new UnityWebRequest(url, "POST");

        www.SetRequestHeader("Authorization", "Bearer " + token);

        // Print form data to debug
        //Debug.Log("Sending form data: account=" + account + ", pw=" + password);

        www.uploadHandler = new UploadHandlerRaw(new byte[0]);
        www.downloadHandler = new DownloadHandlerBuffer();

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log("Response: " + www.downloadHandler.text);
        }
    }
}
