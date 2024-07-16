using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using VictorDev.RevitUtils;

public class Panel_DCR_RU : MonoBehaviour
{
    [Header(">>> DCS項目")]
    [SerializeField] private RU_DCSListItem dcsPrefab;
    [Header(">>> 放置DCS項目之容器")]
    [SerializeField] private Transform ruDeviceContainer;
    [Header(">>> DCS ToggleGroup")]
    [SerializeField] private ToggleGroup toggleGroup;

    [Header(">>> 電力")]
    [SerializeField] private ProgressBarController pbWatt;
    [Header(">>> 負重")]
    [SerializeField] private ProgressBarController pbWeight;

    /// <summary>
    /// RU每單位之高度
    /// </summary>
    private float ruHeight => 25;

    private SO_DCR _soDCR { get; set; }

    public SO_DCR soDCR
    {
        get => _soDCR;
        set
        {
            _soDCR = value;
            pbWatt.value = _soDCR.watt;
            pbWeight.value = _soDCR.weight;

            SetDCSList();
        }
    }

    /// <summary>
    /// 設定DCS列表RU排列
    /// </summary>
    private void SetDCSList()
    {
        ObjectPoolManager.PushToPool<RU_DCSListItem>(ruDeviceContainer);

        IEnumerator CreateDevice()
        {
            soDCR.DCS_List.ForEach(soDCS =>
            {
                RU_DCSListItem item = ObjectPoolManager.GetInstanceFromQueuePool(dcsPrefab, ruDeviceContainer);
                item.toggleGroup = toggleGroup;
                item.soData = soDCS;
                item.name = $"{RevitHandler.GetGameObjectNameFormat(soDCS)} - LocationU: {soDCS.rackLocation} / HeightU: {soDCS.heightU}";

                Vector2 size = item.GetComponent<RectTransform>().sizeDelta;
                size.y = soDCS.heightU * ruHeight;
                item.GetComponent<RectTransform>().sizeDelta = size;

                float posY = (soDCS.rackLocation - 1) * ruHeight + 3 - 1056;
                item.transform.localPosition = new Vector2(0, posY);
            });
            yield return null;
        }
        StartCoroutine(CreateDevice());
    }

    void SetPositionByAnchor(RectTransform rectTransform, Vector2 targetPosition)
    {
        if (rectTransform == null)
        {
            Debug.LogError("RectTransform is null.");
            return;
        }

        RectTransform parentRectTransform = rectTransform.parent as RectTransform;
        if (parentRectTransform == null)
        {
            Debug.LogError("Parent is not a RectTransform.");
            return;
        }

        Vector2 anchorMin = rectTransform.anchorMin;
        Vector2 anchorMax = rectTransform.anchorMax;
        Vector2 pivot = rectTransform.pivot;

        // Calculate the position based on the anchor
        Vector2 anchorPosition = new Vector2(
            (anchorMin.x + anchorMax.x) / 2 * parentRectTransform.rect.width,
            (anchorMin.y + anchorMax.y) / 2 * parentRectTransform.rect.height
        );

        // Adjust the target position based on pivot
        Vector2 pivotOffset = new Vector2(
            (pivot.x - 0.5f) * rectTransform.rect.width,
            (pivot.y - 0.5f) * rectTransform.rect.height
        );

        // Set the localPosition relative to the anchor position
        rectTransform.localPosition = new Vector3(
            targetPosition.x - anchorPosition.x + pivotOffset.x,
            targetPosition.y - anchorPosition.y + pivotOffset.y,
            rectTransform.localPosition.z
        );
    }
}
