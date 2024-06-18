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

    [Header(">>> ���I����Indicator��")]
    public UnityEvent<SO_RT> OnClickIndicator;

    private List<RT_LineIndicator> indicatorList = new List<RT_LineIndicator>();

    protected override void AddMoreComponentToObject(Collider target)
    {
        base.AddMoreComponentToObject(target);

        RT_LineIndicator indicator = target.transform.GetChild(0).GetComponent<RT_LineIndicator>();
        indicator.toggleGroup = toggleGroup;
        indicator.onClickIndicator.AddListener(OnClickIndicator.Invoke);
        indicator.gameObject.SetActive(false);
        indicatorList.Add(indicator);

        indicator.onClickIndicator.AddListener(OnClickIndicatorHandler);
    }

    /// <summary>
    /// ���I����Indicator��
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
