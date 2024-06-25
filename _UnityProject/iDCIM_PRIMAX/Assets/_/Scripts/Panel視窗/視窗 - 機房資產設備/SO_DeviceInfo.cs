using UnityEngine;

public class SO_DeviceInfo : ScriptableObject
{
    [SerializeField] private string deviceName;
    [SerializeField] private int amount;

    public string DeviceName => deviceName;
    public int Amount => amount;
}
