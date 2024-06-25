using TMPro;
using UnityEngine;

public class DeviceListItem : ToggleItem<SO_DeviceInfo>
{
    [Header(">>> UI組件")]
    [SerializeField] private TextMeshProUGUI txtLabel;

    private void OnValidate()
    {
        txtLabel ??= transform.Find("txtLabel").GetComponent<TextMeshProUGUI>();

    }
}
