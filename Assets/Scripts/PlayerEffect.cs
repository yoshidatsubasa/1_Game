using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    private Renderer playerRenderer;
    private Color originalColor;
    private bool isFlickering = false;

    public Color flickerColor = Color.red; // 点滅時の色

    private void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        originalColor = playerRenderer.material.color;
    }

    public void StartFlicker()
    {
        if (!isFlickering)
        {
            isFlickering = true;
            StartCoroutine(FlickerPlayer());
        }
    }

    private System.Collections.IEnumerator FlickerPlayer()
    {
        int flickerCount = 10; // 点滅する回数
        float flickerDuration = 0.05f; // 点滅の間隔

        for (int i = 0; i < flickerCount * 2; i++)
        {
            playerRenderer.material.color = flickerColor; // 赤くする
            yield return new WaitForSeconds(flickerDuration);
            playerRenderer.material.color = originalColor; // 元の色に戻す
            yield return new WaitForSeconds(flickerDuration);
        }

        playerRenderer.material.color = originalColor; // 最終的に元の色に戻す
        isFlickering = false;
    }
}