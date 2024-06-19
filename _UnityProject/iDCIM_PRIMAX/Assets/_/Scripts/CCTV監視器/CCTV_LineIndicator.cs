using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CCTV_LineIndicator : LineIndicator
{
    [Header(">>> CCTV_LineIndicator")]

    [SerializeField] private Toggle toggle;

    [Header(">>> 當點選Toggle時")]
    public UnityEvent<Transform, bool> onToggleChanged;

    public ToggleGroup toggleGroup { set => toggle.group = value; }

    public bool IsOn
    {
        get => toggle.isOn;
        set => toggle.isOn = value;
    }
 
    private void Awake()
    {
        toggle.onValueChanged.AddListener((isOn) =>
        {
            onToggleChanged.Invoke(transform.parent, isOn);
        });
    }
}
