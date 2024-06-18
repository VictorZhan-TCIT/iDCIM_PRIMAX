using UnityEngine;
using UnityEngine.Events;

public class CCTV_UIManager : UIManager
{
    public UnityEvent<CCTV_ModelDataHandler> onClickClosePanel;

    private CCTV_ModelDataHandler cctvDataHandler;

    public void SetDataHandler(CCTV_ModelDataHandler dataHandler)
    {
        Debug.Log($"CCTV_UIManager:");
        cctvDataHandler = dataHandler;
    }

    [ContextMenu("Send Event")]
    private void onClickClosePanelHandler()
    {
        onClickClosePanel.Invoke(cctvDataHandler);
    }
}
