using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageBlink : MonoBehaviour
{
    public Image imageToBlink; // 点滅させたいイメージ
    public float blinkInterval = 0.5f; // 点滅の間隔（秒）

    private void Start()
    {
        // コルーチンを開始する
        StartCoroutine(BlinkImage());
    }
    void Update()
    {
    }
        IEnumerator BlinkImage()
    {
        while (true)
        {
            // イメージの透明度を切り替える（0から1まで）
            imageToBlink.CrossFadeAlpha(0f, blinkInterval, false);
            yield return new WaitForSeconds(blinkInterval);
            imageToBlink.CrossFadeAlpha(1f, blinkInterval, false);
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
