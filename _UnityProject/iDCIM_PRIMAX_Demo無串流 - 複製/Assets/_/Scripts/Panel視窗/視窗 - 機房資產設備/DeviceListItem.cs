using TMPro;
using UnityEngine;
using VictorDev.UI;

public class DeviceListItem : ToggleItem<SO_DeviceInfo>
{
    [Header(">>> UI組件")]
    [SerializeField] private TextMeshProUGUI txtLabel;

    protected override void OnSetSoData()
    {
        throw new System.NotImplementedException();
    }

    private void OnValidate()
    {
        txtLabel ??= transform.Find("txtLabel").GetComponent<TextMeshProUGUI>();

    }
}
