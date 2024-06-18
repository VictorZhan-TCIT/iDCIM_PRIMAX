using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VictorDev.Managers;

/// <summary>
/// ���ʪ���޲z�� - CCTV�ʵ���
/// </summary>
public class CCTV_InteractiveManager : InteractiveManager
{
    [Header(">>> CCTV_InteractiveManager")]
    [SerializeField] private ToggleGroup toggleGroup;

    [Header(">>> ���I����Indicator��")]
    public UnityEvent<CCTV_ModelDataHandler> onClickTarget;

    private Dictionary<Transform, CCTV_ModelDataHandler> modelHandlerDict { get; set; } = new Dictionary<Transform, CCTV_ModelDataHandler>();

    /// <summary>
    /// ���I��ҫ������
    /// </summary>
    private void Awake() => onClickInteractiveItem.AddListener((targetTrans) =>
    {
        OnClickTargetHandler(targetTrans, !modelHandlerDict[targetTrans].IsSelected);
    });

    protected override void AddMoreComponentToObject(Collider target)
    {
        CCTV_LineIndicator indicator = target.transform.GetChild(0).GetComponent<CCTV_LineIndicator>();
        indicator.toggleGroup = toggleGroup;
        indicator.onToggleOn.AddListener(OnClickTargetHandler);

        CCTV_ModelDataHandler dataHandler = target.AddComponent<CCTV_ModelDataHandler>();
        dataHandler.SetLineIndicator(indicator);

        modelHandlerDict[target.transform] = dataHandler;
    }

    /// <summary>
    /// ���I��ϼЮ�
    /// </summary>
    private void OnClickTargetHandler(Transform targetTrans, bool isOn)
    {
        if (isOn == false) modelHandlerDict[targetTrans].SetSelected(false);
        else
        {
            foreach (Transform target in modelHandlerDict.Keys)
            {
                if (targetTrans == target)
                {
                    modelHandlerDict[target].SetSelected(true);
                    onClickTarget.Invoke(modelHandlerDict[target]);
                }
                else
                {
                    modelHandlerDict[target].SetSelected(false);
                }
            }
        }
    }
    /// <summary>
    /// �]�mIndicator���/���� (��Toggle�ϥ�)
    /// </summary>
    public void SetIndicatorVisible(bool isVisible)
    {
        SetOutlineVisible(isVisible);

        foreach (Transform target in modelHandlerDict.Keys)
        {
            modelHandlerDict[target].SetActivated(isVisible);
        }
    }
    /// <summary>
    /// �]�m�ؼ�DataHandler����������A
    /// </summary>
    public void SetTargetUnSelected(CCTV_ModelDataHandler dataHandler) => dataHandler.SetSelected(false);
    private void OnValidate() => toggleGroup ??= GetComponent<ToggleGroup>();
}
