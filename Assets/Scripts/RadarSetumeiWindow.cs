using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarSetumeiWindow : MonoBehaviour
{

    public GameObject setumeiWindow1; // シーン1用のUI
    public GameObject setumeiWindow2; // シーン2用のUI
    public GameObject setumeiWindow3; // シーン3用のUI
    public GameObject player; // プレイヤー

    private bool animationTriggered1 = false; // シーン1用のアニメーションのトリガーが発動されたかどうかを追跡する変数
    private bool animationTriggered2 = false; // シーン2用のアニメーションのトリガーが発動されたかどうかを追跡する変数
    private bool animationTriggered3 = false; // シーン2用のアニメーションのトリガーが発動されたかどうかを追跡する変数

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = player.transform.position;

        // シーン1のUIを制御
        if (Vector3.Distance(transform.position, playerPosition) < 5f && !animationTriggered1)
        {
            StartCoroutine(ShowNewUIAfterDelay(1f, 1));   // 1秒後にシーン1のUIを表示する
            StartCoroutine(HideNewUIAfterDelay(9f, 1));   // 9秒後にシーン1のUIを非表示にする
        }

        // シーン2のUIを制御
        if (Vector3.Distance(transform.position, playerPosition) < 16f && !animationTriggered2)
        {
            StartCoroutine(ShowNewUIAfterDelay(1f, 2));   // 1秒後にシーン2のUIを表示する
            StartCoroutine(HideNewUIAfterDelay(9f, 2));   // 9秒後にシーン2のUIを非表示にする
        }

        // シーン3のUIを制御
        if (Vector3.Distance(transform.position, playerPosition) < 10f && !animationTriggered3)
        {
            StartCoroutine(ShowNewUIAfterDelay(1f, 3));   // 1秒後にシーン3のUIを表示する
            StartCoroutine(HideNewUIAfterDelay(9f, 3));   // 9秒後にシーン3のUIを非表示にする
        }

        if (Input.GetButton("BButton"))
        {
            setumeiWindow1.SetActive(false);
            setumeiWindow2.SetActive(false);
            setumeiWindow3.SetActive(false);
          
        }
    }

    // UiWindow表示
    IEnumerator ShowNewUIAfterDelay(float delay, int sceneNumber)
    {
        if (sceneNumber == 1)
            animationTriggered1 = true; // シーン1用のトリガーが発動されたことを記録
        else if (sceneNumber == 2)
            animationTriggered2 = true; // シーン2用のトリガーが発動されたことを記録
        else if (sceneNumber == 3)
            animationTriggered3 = true; // シーン2用のトリガーが発動されたことを記録

        yield return new WaitForSeconds(delay);

        // シーン1のUIを表示
        if (sceneNumber == 1)
        {
            setumeiWindow1.SetActive(true);
        }
        // シーン2のUIを表示
        else if (sceneNumber == 2)
        {
            setumeiWindow2.SetActive(true);
        }

        // シーン3のUIを表示
        else if (sceneNumber == 3)
        {
            setumeiWindow3.SetActive(true);
        }
    }

    // UiWindow非表示
    IEnumerator HideNewUIAfterDelay(float delay, int sceneNumber)
    {
        yield return new WaitForSeconds(delay);

        // シーン1のUIを非表示
        if (sceneNumber == 1)
        {
            setumeiWindow1.SetActive(false);
        }
        // シーン2のUIを非表示
        else if (sceneNumber == 2)
        {
            setumeiWindow2.SetActive(false);
        }
        // シーン3のUIを非表示
        else if (sceneNumber == 3)
        {
            setumeiWindow3.SetActive(false);
        }
    }
}