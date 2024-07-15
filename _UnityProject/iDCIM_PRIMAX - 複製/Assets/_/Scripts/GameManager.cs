using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private Module[] modules;


    [SerializeField] private DeviceManager deviceManager;
    [SerializeField] private WebAPIManager webAPIManager;
    private void Awake()
    {
        modules.ToList().ForEach(module =>
        {
            module.onClickModel.AddListener(cameraManager.LookAtTarget);
        });
    }

    private void Start()
    {
        webAPIManager.onGetAllDCRInfo.AddListener(deviceManager.Parse_AllDCRInfo);
        webAPIManager.GetAllDCRInfo();
    }
}
