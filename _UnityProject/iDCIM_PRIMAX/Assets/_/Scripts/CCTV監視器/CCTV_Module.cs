using UnityEngine;

public class CCTV_Module : Module
{
    [SerializeField] private CCTV_InteractiveManager cctvInteractiveManager;
    [SerializeField] private CCTV_UIManager cctvUIManager;

    private void Awake()
    {
        cctvInteractiveManager.onToggleChanged.AddListener(OnClickModel);
    }

    private void OnClickModel(CCTV_DataHandler dataHandler)
    {
        cctvUIManager.SetDataHandler(dataHandler);
        if (dataHandler.IsSelected) onClickModel.Invoke(dataHandler.transform);
    }
}
