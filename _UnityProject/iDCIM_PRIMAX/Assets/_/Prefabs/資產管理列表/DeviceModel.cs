using System;
using UnityEngine;
using UnityEngine.Events;
using VictorDev.Async.CoroutineUtils;
using VictorDev.Common;

/// <summary>
///  �]�Ƽҫ� (DCR�BDCS�BDCN�BDCE�BDCP)
/// </summary>
[RequireComponent(typeof(ClickableObject))]
[RequireComponent(typeof(LerpEmission))]
public class DeviceModel : MonoBehaviour
{
    [Header(">>> �O�_�Q�I��")]
    [SerializeField] private bool _isSelected;

    [Header(">>> �]�Ƹ�T")]
    [SerializeField] private SO_Device soData;
    public SO_Device soDataInfo => soData;

    [Header(">>> ��Q�I���ӧ���Toggle���A��")]
    public UnityEvent<DeviceModel> onSelectChanged;

    [Header(">>> �ե�")]
    [SerializeField] private ClickableObject clickableObject;
    [SerializeField] private LerpEmission lerpEmission;

    /// <summary>
    /// �ثe�O�_���Q�I�窱�A
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
            onSelectChanged.Invoke(this);
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