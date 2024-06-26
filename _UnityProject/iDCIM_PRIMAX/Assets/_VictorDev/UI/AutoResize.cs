using UnityEngine;

namespace VictorDev.UI
{
    /// <summary>
    /// �۰ʽվ�ئT�j�p
    /// <para>+ ����������GameObject�U�Y�i</para>
    /// </summary>
    public class AutoResize : MonoBehaviour
    {
        [Header(">>> �ѪR��")]
        [SerializeField] private Vector2 resolution = new Vector2(1920, 1080);
        [Header(">>> �O�_�j�������w�ѪR��")]
        [SerializeField] private bool isForcedResolution = false;

        [Space(10)]
        [SerializeField] private RectTransform rectTransform;

        private void Start()
        {

        }

        private void OnValidate()
        {
            rectTransform ??= GetComponent<RectTransform>();
            Resize();
        }

        private void Resize()
        {
            Canvas.ForceUpdateCanvases(); //�j��Canvas��s�@�V

            //���w�ئT
            if (isForcedResolution) rectTransform.sizeDelta = resolution;
            else
            {
                // �]�m�s���ؤo�A�ھڷs���e�׵���ҽվ㰪��
                float newHeight = resolution.y * (rectTransform.rect.width / resolution.x);
                // �]�m�s���ؤo
                rectTransform.sizeDelta = new Vector2(rectTransform.rect.width, newHeight);
            }
        }
    }
}
