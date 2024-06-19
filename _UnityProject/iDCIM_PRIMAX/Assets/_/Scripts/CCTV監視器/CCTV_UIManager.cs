using UnityEngine;

public class CCTV_UIManager : MonoBehaviour
{
    private CCTV_DataHandler cctvDataHandler;

    public void SetDataHandler(CCTV_DataHandler dataHandler)
    {
        Debug.Log($"CCTV_UIManager: {dataHandler.name} / isOn: {dataHandler.IsSelected}");
        cctvDataHandler = dataHandler;
    }

    [ContextMenu("Send Event")]
    private void onClickClosePanelHandler()
    {
        cctvDataHandler.IsSelected = false;
    }
}
