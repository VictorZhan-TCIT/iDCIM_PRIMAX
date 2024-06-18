using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private Module[] modules;
    private void Awake()
    {
        modules.ToList().ForEach(module =>
        {
            module.onClickModel.AddListener(cameraManager.LookAtTarget);
        });
    }
}
