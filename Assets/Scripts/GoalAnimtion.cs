using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoalAnimtion : MonoBehaviour
{
    public GameObject playerGameObject; // プレイヤーオブジェクト
    private PlayerController playerController; // プレイヤーコントローラー

    public GameObject ClearUpWindow;
    public Text timeText; // 時間を表示するテキスト

    public GameObject setumeiWindow; //  RadarSetumeiWindowで設定されたUI
    public GameObject setumeiWindow2; // RadarSetumeiWindowで設定されたUI
    public GameObject setumeiWindow3; // RadarSetumeiWindowで設定されたUI
    public GameObject setumeiWindow4; // RadarSetumeiWindowで設定されたUI


    public AudioSource source1;
    public AudioClip clip1;

    public GameObject[] countDownObjects; // 10秒前のオブジェクトを格納する配列

    private bool hasEnded = false; // ゲームが終了したかどうかを示すフラグ
    private StartCount startCount;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        startCount = FindObjectOfType<StartCount>();

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ClearUpWindow.SetActive(true);
            StartCoroutine(LoadSceneAfterDelay("Clear", 2f)); // 2秒後にゴールシーンをロード

            source1.Play();

            hasEnded = true; // ゲームが終了したことを示すフラグを設定
        }
    }

    private void Update()
    {
        // タイムアップウィンドウが表示された場合、時間を非表示にする
        if (ClearUpWindow.activeSelf)
        {
            // プレイヤーの動きを停止する
            if (playerController != null)
            {
                playerController.StopMovement2();
            }

            // 時間を非表示にする
            if (timeText != null)
            {
                timeText.gameObject.SetActive(false);
            }


            // ゴールに触れたときにRadarSetumeiWindowのsetumeiWindowを非表示にする
            if (setumeiWindow != null)
            {
                setumeiWindow.SetActive(false);
            }
            if (setumeiWindow2 != null)
            {
                setumeiWindow2.SetActive(false);
            }
            if (setumeiWindow3 != null)
            {
                setumeiWindow3.SetActive(false);
            }
            if (setumeiWindow4 != null)
            {
                setumeiWindow4.SetActive(false);
            }

            // ゲームが終了していない場合のみTimeUpWindowを非表示にする
            if (hasEnded)
            {
                startCount.TimeUpWindow.SetActive(false);
                startCount.source2.Stop();
                startCount.source1.Stop();
            }
           
            // ゴールしたらcountDownObjectsのオブジェクトを非表示にする
            if (countDownObjects != null)
            {
                foreach (GameObject obj in countDownObjects)
                {
                    obj.SetActive(false);
                }
            }
        }
    }

   
    IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay); // 指定した秒数待機
        SceneManager.LoadScene(sceneName); // シーンをロード
    }
}