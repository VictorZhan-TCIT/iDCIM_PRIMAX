using UnityEngine;

public class CCTV_UIManager : MonoBehaviour
{
    private CCTV_DataHandler dataHandler;

    public void SetDataHandler(CCTV_DataHandler dataHandler)
    {
        if (dataHandler.IsSelected)
        {
            Debug.Log($"¶}±Ò: {dataHandler.name} / RTSP: {dataHandler.RTSP_URL}");
            this.dataHandler = dataHandler;
        }
        else
        {
            Debug.Log($"Ãö³¬: {dataHandler.name}");
        }
    }

    [ContextMenu("Send Event")]
    private void onClickClosePanelHandler()
    {
        dataHandler.IsSelected = false;
    }
}
