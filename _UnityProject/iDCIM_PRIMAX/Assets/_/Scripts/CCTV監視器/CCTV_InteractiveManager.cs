using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VictorDev.Managers;

/// <summary>
/// 互動物件管理器 - CCTV監視器
/// </summary>
public class CCTV_InteractiveManager : InteractiveManager
{
    [Header(">>> CCTV_InteractiveManager")]
    [SerializeField] private ToggleGroup toggleGroup;

    [Header(">>> 當點擊到Indicator時")]
    public UnityEvent<CCTV_ModelDataHandler> onClickTarget;

    private Dictionary<Transform, CCTV_ModelDataHandler> modelHandlerDict { get; set; } = new Dictionary<Transform, CCTV_ModelDataHandler>();

    /// <summary>
    /// 當點選模型物件時
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
    /// 當點選圖標時
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
    /// 設置Indicator顯示/隱藏 (供Toggle使用)
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
    /// 設置目標DataHandler取消選取狀態
    /// </summary>
    public void SetTargetUnSelected(CCTV_ModelDataHandler dataHandler) => dataHandler.SetSelected(false);
    private void OnValidate() => toggleGroup ??= GetComponent<ToggleGroup>();
}
