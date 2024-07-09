using UnityEngine;
using VictorDev.Async.CoroutineUtils;
using VictorDev.Common;

[RequireComponent(typeof(ClickableObject))]
[RequireComponent(typeof(LerpEmission))]
public class DataHandler_DCS : MonoBehaviour
{
    [SerializeField] private ClickableObject clickableObject;
    [SerializeField] private LerpEmission lerpEmission;

    private void Awake()
    {
        clickableObject.OnMouseEnterEvent.AddListener((target)=>lerpEmission.StartLerp());
        clickableObject.OnMouseExitEvent.AddListener((target)=>lerpEmission.Stop());
    }


    private void OnValidate()
    {
        clickableObject ??= GetComponent<ClickableObject>();
        lerpEmission ??= GetComponent<LerpEmission>();
    }
}
