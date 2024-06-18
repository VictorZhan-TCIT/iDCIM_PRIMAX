using System.Collections.Generic;
using UnityEngine;
using VictorDev.Managers;

public class InteractiveManagerWithShader : InteractiveManager
{
    private Dictionary<Transform, Material> materialDict { get; set; } = new Dictionary<Transform, Material>();

    private void Awake()
    {
        onClickInteractiveItem.AddListener(onClickHandler);
    }

    private void onClickHandler(Transform target)
    {
        SetIndicatorVisible(true);
        materialDict[target].SetInt("_IsSelected", 1);
    }

    protected override void AddMoreComponentToObject(Collider target)
    {
        if (target.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer))
        {
            materialDict[target.transform] = meshRenderer.material;
        }
        else
        {
            materialDict[target.transform] = target.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial;
        }
    }

    public virtual void SetIndicatorVisible(bool isVisible)
    {
        SetOutlineVisible(isVisible);
        foreach (Material material in materialDict.Values)
        {
            material.SetInt("_IsActivated", (isVisible ? 1 : 0));
            material.SetInt("_IsSelected", 0);
        }
    }

    private void OnApplicationQuit()
    {
        SetIndicatorVisible(false);
    }
}
