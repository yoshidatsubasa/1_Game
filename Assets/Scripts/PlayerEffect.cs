using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerEffect : MonoBehaviour
{
    private Renderer[] renderers;
    private Color[] originalColors;
    private bool isFlickering = false;

    public Color flickerColor = Color.red; // 点滅時の色

    private void Start()
    {
        renderers = GetComponentsInChildren<Renderer>(); // モデルのすべてのRendererコンポーネントを取得
        originalColors = new Color[renderers.Length]; // 各Rendererの初期色を保存
        for (int i = 0; i < renderers.Length; i++)
        {
            originalColors[i] = renderers[i].material.color;
        }
    }

    public void StartFlicker()
    {
        if (!isFlickering)
        {
            isFlickering = true;
            StartCoroutine(FlickerPlayer());
        }
    }

    private IEnumerator FlickerPlayer()
    {
        int flickerCount = 10; // 点滅する回数
        float flickerDuration = 0.05f; // 点滅の間隔

        for (int i = 0; i < flickerCount * 2; i++)
        {
            // すべてのRendererの色を赤くする
            foreach (Renderer renderer in renderers)
            {
                renderer.material.color = flickerColor;
            }
            yield return new WaitForSeconds(flickerDuration);

            // 元の色に戻す
            for (int j = 0; j < renderers.Length; j++)
            {
                renderers[j].material.color = originalColors[j];
            }
            yield return new WaitForSeconds(flickerDuration);
        }

        // 最終的に元の色に戻す
        for (int j = 0; j < renderers.Length; j++)
        {
            renderers[j].material.color = originalColors[j];
        }
        isFlickering = false;
    }
}