using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartCount : MonoBehaviour
{
    //時間を表示するText型の変数
    public Text timeText;
    public Text CountText;
    public Text CountText2;
    public Text CountText3;
    public float totaltime = 10.0f;
    float retTime;
    float countdown = 4f;
    int count;
    new public Rigidbody rigidbody;

    public float jumppower = 5;

    public AudioSource bgmAudioSource; // BGM用のAudioSource
    public AudioClip bgmClip; // BGMのAudioClip

    public AudioSource source; // 効果音用のAudioSource
    public AudioClip countdownClip; // カウントダウン効果音のAudioClip

    public AudioSource source1;
    public AudioClip clip1;

    public AudioSource source2;
    public AudioClip clip2;

    bool startBGM = false; // BGMを再生するフラグ
    bool countdownStarted = false; // カウントダウンが開始されたかどうかのフラグ

    bool goalReached = false; // ゴールに到達したかどうかのフラグ

    public GameObject setumeiWindow; // 説明としてだすUi
    public GameObject setumeiWindow2;
    public GameObject setumeiWindow3;
    public GameObject setumeiWindow4;
    public GameObject TimeUpWindow;
    public GameObject[] countDownObjects; // 10秒前のオブジェクトを格納する配列


    public GameObject playerGameObject; // プレイヤーオブジェクト
    private PlayerController playerController; // プレイヤーコントローラー

    bool playedCountdownSound = false; // カウントダウン効果音を再生したかどうかのフラグ
    void Start()
    {
        // 初期化時に11秒前のオブジェクトを非アクティブにする
        foreach (var obj in countDownObjects)
        {
            obj.SetActive(false);
        }

        // プレイヤーオブジェクトからPlayerControllerコンポーネントを取得
        playerController = playerGameObject.GetComponent<PlayerController>();
    }
    
    private void Update()
    {
        if (!countdownStarted)
        {
            countdownStarted = true;

            // カウントダウンが始まる瞬間に効果音を再生する
            if (countdownClip != null)
            {
                source.PlayOneShot(countdownClip);
            }
        }
        if (countdown>=1)
        {
            countdown -= Time.deltaTime;
            count = (int)countdown;
            CountText.text = count.ToString();
            CountText3.text = "時間内にゴール旗を目指せ！";

            rigidbody.velocity = Vector3.zero;
            rigidbody.velocity = Vector3.up * jumppower;
        }
        if (countdown<=1)
        {
            CountText.text = "";
            CountText2.text = "Start!!";
            CountText3.text = "";
            totaltime -= Time.deltaTime;
            retTime = (float)totaltime;
            timeText.text = retTime.ToString("f1");
            if(retTime<= 0 && !goalReached)
            {
                StartCoroutine(LoadSceneAfterDelay("Over", 2f)); // 2秒後にゴールシーンをロード
            }
            else if (retTime <= 0.5 && !goalReached)
            {
                TimeUpWindow.SetActive(true);
            }
            else if (retTime <= 2 && !goalReached)
            {
                source1.Play();
            }

            // 10秒前のオブジェクトを表示する際に一度だけ効果音を再生する
            if (totaltime<=10.8f && !playedCountdownSound)
            {
                // カウントダウンの効果音を再生する
                if (clip2 != null)
                {
                    source2.PlayOneShot(clip2);
                    playedCountdownSound = true; // カウントダウン効果音を再生した
                }
            }

        }
        if(CountText2.text== "Start!!" && !startBGM)
        {
            Destroy(CountText2, 0.5f);
            StartCoroutine(PlayBGMAfterDelay(0.5f));   // カウントダウン終了後、1秒後にBGM再生
            StartCoroutine(ShowNewUIAfterDelay(1f));   // 1秒後に新しいUIを表示する
            StartCoroutine(HideNewUIAfterDelay(9f));   // 9秒後に新しいUIを非表示にする
            StartCoroutine(ShowNewUIAfterDelay2(8f));  // さらに8秒後に次のUIを表示する
            StartCoroutine(HideNewUIAfterDelay2(17f)); // 17秒後に新しいUIを非表示にする
            StartCoroutine(ShowNewUIAfterDelay3(15f)); // さらに15秒後に次のUIを表示する
            StartCoroutine(HideNewUIAfterDelay3(25f)); // 25秒後に新しいUIを非表示にする
            StartCoroutine(ShowNewUIAfterDelay4(1f));  // 1秒後に次のUIを表示する
            StartCoroutine(HideNewUIAfterDelay4(12f)); // 13秒後に新しいUIを非表示にする
        }

        
        if (totaltime <= 10.0f && totaltime >= 5.0f)
        {
            timeText.color = Color.red; // 5秒以上10秒以下の間、timeTextの色を赤くする
        }
        // カウントダウンオブジェクトの表示を設定する
        if (totaltime <= 11 && totaltime >= 1)
        {
            int indexToShow = 11 - Mathf.RoundToInt(totaltime); // 表示するオブジェクトのインデックスを計算
            if (indexToShow >= 0 && indexToShow < countDownObjects.Length)
            {
                countDownObjects[indexToShow].SetActive(true); // 対応するオブジェクトを表示
                StartCoroutine(HideCountDownObjectDelayed(countDownObjects[indexToShow])); // 1秒後に非表示にする
            }
        }

        if (Input.GetButton("BButton"))
        {
            setumeiWindow.SetActive(false);
            setumeiWindow2.SetActive(false);
            setumeiWindow3.SetActive(false);
            setumeiWindow4.SetActive(false);
        }

        // タイムアップウィンドウが表示された場合、プレイヤーの動きを停止する
        if (TimeUpWindow.activeSelf)
        {
           
            // プレイヤーコントローラーがnullでないことを確認してから、動きを停止する
            if (playerController != null)
            {
                playerController.StopMovement(); // プレイヤーの動きを停止するメソッドを呼び出す
            }

            setumeiWindow.SetActive(false);
            setumeiWindow2.SetActive(false);
            setumeiWindow3.SetActive(false);
            setumeiWindow4.SetActive(false);
        }
    }

   
    //UiWindow表示
    IEnumerator ShowNewUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        setumeiWindow.SetActive(true);
    }
    IEnumerator ShowNewUIAfterDelay2(float delay)
    {
        yield return new WaitForSeconds(delay);
        setumeiWindow2.SetActive(true); 
    }

    IEnumerator ShowNewUIAfterDelay3(float delay)
    {
        yield return new WaitForSeconds(delay);
        setumeiWindow3.SetActive(true); 
    }

    IEnumerator ShowNewUIAfterDelay4(float delay)
    {
        yield return new WaitForSeconds(delay);
        setumeiWindow4.SetActive(true); 
    }

    //UiWindow非表示
    IEnumerator HideNewUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        setumeiWindow.SetActive(false);
    }

    IEnumerator HideNewUIAfterDelay2(float delay)
    {
        yield return new WaitForSeconds(delay);
        setumeiWindow2.SetActive(false);
    }

    IEnumerator HideNewUIAfterDelay3(float delay)
    {
        yield return new WaitForSeconds(delay);
        setumeiWindow3.SetActive(false);
    }

    IEnumerator HideNewUIAfterDelay4(float delay)
    {
        yield return new WaitForSeconds(delay);
        setumeiWindow4.SetActive(false);
    }

    //BGM再生
    IEnumerator PlayBGMAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (bgmAudioSource != null && bgmClip != null)
        {
            bgmAudioSource.clip = bgmClip;
            bgmAudioSource.Play();
            startBGM = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
      
        if(other.CompareTag("Goal") && !goalReached)
        {
            Debug.Log("ゴールに到達しました！");
            goalReached = true;
        }
    }

    IEnumerator HideCountDownObjectDelayed(GameObject obj)
    {
        yield return new WaitForSeconds(1f); // 1秒待機
        obj.SetActive(false); // オブジェクトを非表示にする
    }

    IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        totaltime = 0;
        yield return new WaitForSeconds(delay); // 指定した秒数待機
        SceneManager.LoadScene(sceneName); // シーンをロード
    }

}