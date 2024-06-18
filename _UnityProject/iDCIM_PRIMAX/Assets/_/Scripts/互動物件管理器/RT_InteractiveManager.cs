using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VictorDev.Managers;

public class RT_InteractiveManager : InteractiveManagerWithShader
{
    [Header(">>> RT_InteractiveManager")]
    [SerializeField] private ToggleGroup toggleGroup;

    [Header(">>> 當點擊到Indicator時")]
    public UnityEvent<Transform, SO_RT> onClickIndicator;

    private List<RT_LineIndicator> indicatorList = new List<RT_LineIndicator>();

    private void Awake()
    {
        onClickInteractiveItem.AddListener((targetTrans) =>
        {
            RT_LineIndicator indicator = targetTrans.GetChild(0).GetComponent<RT_LineIndicator>();
            onClickIndicator.Invoke(targetTrans, indicator.soRTData);
        });
    }

    protected override void AddMoreComponentToObject(Collider target)
    {
        base.AddMoreComponentToObject(target);

        RT_LineIndicator indicator = target.transform.GetChild(0).GetComponent<RT_LineIndicator>();
        indicator.toggleGroup = toggleGroup;
        indicator.gameObject.SetActive(false);
        indicatorList.Add(indicator);

        indicator.onClickIndicator.AddListener(soData => onClickIndicator.Invoke(target.transform, soData));
        indicator.onClickIndicator.AddListener(OnClickIndicatorHandler);
    }

    /// <summary>
    /// 當點擊到Indicator時
    /// </summary>
    private void OnClickIndicatorHandler(SO_RT soData)
    {
        Debug.Log($"SO_RT OnClickIndicator:");
        //Debug.Log($"SO_RT OnClickIndicator: {soData.Degree}");
    }


    override public void SetIndicatorVisible(bool isVisible)
    {
        base.SetIndicatorVisible(isVisible);

        SetOutlineVisible(isVisible);
        indicatorList.ForEach(indicator => indicator.gameObject.SetActive(isVisible));
    }
}
