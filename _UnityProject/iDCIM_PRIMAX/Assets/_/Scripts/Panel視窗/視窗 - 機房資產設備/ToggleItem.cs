using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleItem<SO> : MonoBehaviour where SO : ScriptableObject
{
    [Header(">>> ScriptableObject資料")]
    [SerializeField] protected SO soData;

    [Header(">>> 點選Toggle時")]
    public UnityEvent<SO, bool> onToggleChanged;

    [SerializeField] private Toggle toggle;

    public ToggleGroup toggleGroup { set => toggle.group = value; }

    private void Awake()
    {
        toggle.onValueChanged.AddListener((isOn) => onToggleChanged?.Invoke(soData, toggle.isOn));
    }

    private void OnValidate() => toggle ??= GetComponent<Toggle>();
}
