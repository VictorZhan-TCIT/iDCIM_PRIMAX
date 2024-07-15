using UnityEngine;

public class Panel_DCRInfo : MonoBehaviour
{
    public ProgressBarController pbWatt;
    public ProgressBarController pbWeight;

    public SO_DCR soDCR
    {
        set
        {
            pbWatt.value = value.watt;
            pbWeight.value = value.weight;
        }
    }
}
