using TMPro;
using UnityEngine;
using VictorDev.RevitUtils;
using VictorDev.UI;

public class RU_DCSListItem : ToggleItem<SO_DCS>
{
    [SerializeField] private TextMeshProUGUI txtDeviceName;

    protected override void OnSetSoData()
    {
        string deviceType = RevitHandler.GetDCSTypeFromDeviceID(soData.deviceId);
        txtDeviceName.SetText(deviceType);
    }
}
