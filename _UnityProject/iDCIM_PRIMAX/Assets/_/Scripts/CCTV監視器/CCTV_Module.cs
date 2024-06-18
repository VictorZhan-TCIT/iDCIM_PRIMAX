using UnityEngine;

public class CCTV_Module : Module
{
    [SerializeField] private CCTV_InteractiveManager interactiveManager;
    [SerializeField] private CCTV_UIManager uiManager;

    private void Awake()
    {
        interactiveManager.onClickTarget.AddListener(OnClickModel);
        uiManager.onClickClosePanel.AddListener(interactiveManager.SetTargetUnSelected);
    }

    private void OnClickModel(CCTV_ModelDataHandler dataHandler)
    {
        onClickModel.Invoke(dataHandler.transform);
        uiManager.SetDataHandler(dataHandler);
    }
}
