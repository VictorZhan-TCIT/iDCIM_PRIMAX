using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CCTV_LineIndicator : LineIndicator
{
    [Header(">>> CCTV_LineIndicator")]
   
    [SerializeField] private Toggle toggle;

    [Header(">>> 當點選Toggle時")]
    public UnityEvent<Transform, bool> onToggleOn;

    public ToggleGroup toggleGroup { set => toggle.group = value; }

    public bool IsOn { set => toggle.isOn = value; }


    private void Awake()
    {
        toggle.onValueChanged.AddListener((isOn) =>
        {
            onToggleOn.Invoke(transform.parent, isOn);
        });
    }

    private void OnEnable()
    {
        toggle.isOn = false; //不見得會觸發OnValueChanged事件
        toggle.onValueChanged.Invoke(false);
    }
}
