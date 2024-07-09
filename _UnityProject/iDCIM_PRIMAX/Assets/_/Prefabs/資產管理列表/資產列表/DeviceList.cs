using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleGroup))]
public class DeviceList : ScrollRectToggleList<DeviceListItem, SO_Device>
{
    [SerializeField] private ToggleGroup toggleGroup;

    private void Awake()
    {
        this.onToggleChanged.AddListener((soData, isOn) =>
        {
            Debug.Log($"soData: {soData.DeviceName} / isOn:{isOn}");
        });
    }

    protected override void OnCreateEachItem(DeviceListItem item, SO_Device soData)
    {
        base.OnCreateEachItem(item, soData);
        item.toggleGroup = toggleGroup;
    }

    private void OnValidate()
    {
        toggleGroup ??= GetComponent<ToggleGroup>();
    }
}
