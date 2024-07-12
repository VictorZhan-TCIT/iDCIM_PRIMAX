using System;
using UnityEngine;
using UnityEngine.Events;
using VictorDev.Async.CoroutineUtils;
using VictorDev.Common;
using VictorDev.RevitUtils;

/// <summary>
///  設備模型 (DCR、DCS、DCN、DCE、DCP)
/// </summary>
[RequireComponent(typeof(ClickableObject))]
[RequireComponent(typeof(LerpEmission))]
public abstract class DeviceModel<T> : MonoBehaviour where T : ScriptableObject
{
    [Header(">>> 是否被點選")]
    [SerializeField] private bool _isSelected;

    [Header(">>> 設備資訊")]
    public T soData;

    [Header(">>> 當被點擊而改變Toggle狀態時")]
    public UnityEvent<DeviceModel<T>> onToggleChanged;

    [Header(">>> 組件")]
    [SerializeField] private ClickableObject clickableObject;
    [SerializeField] private LerpEmission lerpEmission;

    public string elementId => RevitHandler.GetRevit_elementId(name);

    /// <summary>
    /// 目前是否為被點選狀態
    /// </summary>
    public bool isSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected == value) return;
            _isSelected = value;
            if (isSelected) lerpEmission.StartLoop();
            else lerpEmission.Stop();
            onToggleChanged.Invoke(this);
        }
    }

    private void Awake()
    {
        void CheckStatus(Action action)
        {
            if (_isSelected == false) action.Invoke();
        }
        clickableObject.OnMouseEnterEvent.AddListener((target) => CheckStatus(lerpEmission.StartLerp));
        clickableObject.OnMouseExitEvent.AddListener((target) => CheckStatus(lerpEmission.Stop));
        clickableObject.OnMouseClickEvent.AddListener((target) => isSelected = true);
    }

    private void OnValidate()
    {
        clickableObject ??= GetComponent<ClickableObject>();
        lerpEmission ??= GetComponent<LerpEmission>();
    }
}