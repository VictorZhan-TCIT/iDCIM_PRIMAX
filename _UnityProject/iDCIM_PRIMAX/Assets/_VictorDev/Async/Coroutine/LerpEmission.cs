using System.Collections;
using UnityEngine;

namespace VictorDev.Async.CoroutineUtils
{
    /// <summary>
    /// 對物件的Material進行變色與Intensity
    /// <para>+ 掛載在目標物件</para>
    /// </summary>
    public class LerpEmission : MonoBehaviour
    {
        [Header(">>> Emission設定")]
        public Color emissionColor = Color.yellow;
        public float minIntensity = 0f;
        public float maxIntensity = 0.5f;
        public float duration = 1f;

        [Space(10)]
        [SerializeField] private Material material;
        private IEnumerator iEnumerator { get; set; }

        /// <summary>
        /// 進行循環變色與Intensity
        /// </summary>
        [ContextMenu("- StartLoop")]
        public void StartLoop()
        {
            iEnumerator = LoopEmission();
            CoroutineHandler.RunCoroutine(iEnumerator);
        }
        private IEnumerator LoopEmission()
        {
            // 启用Emission
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", emissionColor);

            while (true)
            {
                float time = 0f;
                while (time < duration)
                {
                    time += Time.deltaTime;
                    // 使用 Sin 和 Lerp 函数来循环调整发光强度
                    float intensity = Mathf.Lerp(-5f, 2f, (Mathf.Sin(time * Mathf.PI / duration) + 1) / 2);
                    SetColor(intensity);
                    yield return null;
                }
            }
        }

        /// <summary>
        /// 進行變色與Intensity
        /// </summary>
        [ContextMenu("- StartLerp")]
        public void StartLerp()
        {
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", emissionColor);
            iEnumerator = CoroutineHandler.LerpValue(minIntensity, maxIntensity, SetColor, duration);
        }

        [ContextMenu("- Stop")]
        public void Stop()
        {
            material.DisableKeyword("_EMISSION");
            CoroutineHandler.CancellCoroutine(iEnumerator);
        }

        private void SetColor(float indensity)
        {
            Color finalColor = emissionColor * Mathf.LinearToGammaSpace(indensity);
            material.SetColor("_EmissionColor", finalColor);
        }
        private void OnValidate() => material = GetComponent<MeshRenderer>().materials[0];
    }
}
