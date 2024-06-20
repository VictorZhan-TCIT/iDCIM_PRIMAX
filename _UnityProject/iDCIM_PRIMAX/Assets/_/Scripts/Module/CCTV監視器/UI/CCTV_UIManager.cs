using UnityEngine;

public class CCTV_UIManager : MonoBehaviour
{
    private CCTV_DataHandler dataHandler;

    public void SetDataHandler(CCTV_DataHandler dataHandler)
    {
        Debug.Log($"CCTV_UIManager: {dataHandler.name} / isOn: {dataHandler.IsSelected}");
        this.dataHandler = dataHandler;
    }

    [ContextMenu("Send Event")]
    private void onClickClosePanelHandler()
    {
        dataHandler.IsSelected = false;
    }
}
