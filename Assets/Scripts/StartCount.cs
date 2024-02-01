using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartCount : MonoBehaviour
{
    //[SerializeField] private GameObject pausePanel;

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

    bool startBGM = false; // BGMを再生するフラグ
    bool countdownStarted = false; // カウントダウンが開始されたかどうかのフラグ

    bool goalReached = false; // ゴールに到達したかどうかのフラグ

    void Start()
    {
        
   
        //pausePanel.SetActive(true);
       
        //Time.timeScale = 0;  // 時間停止
        
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
           //pausePanel.SetActive(false);
            CountText.text = "";
            CountText2.text = "Start!!";
            CountText3.text = "";
            totaltime -= Time.deltaTime;
            retTime = (float)totaltime;
            timeText.text = retTime.ToString("f1");
            if(retTime<= 0 && !goalReached)
            {
                SceneManager.LoadScene("Over");
            }

        }
        if(CountText2.text== "Start!!" && !startBGM)
        {
            Destroy(CountText2, 0.5f);
            StartCoroutine(PlayBGMAfterDelay(0.5f)); // カウントダウン終了後、1秒後にBGM再生

        }


        if (totaltime <= 10.0f && totaltime >= 5.0f)
        {
            timeText.color = Color.red; // 5秒以上10秒以下の間、timeTextの色を赤くする
        }
    }
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
        //if (other.CompareTag("Time"))
        //{
        //    Debug.Log("アイテムを取得しました！");
        //    totaltime += 10.0f;
        //    UpdateTimeText();
        //    Destroy(other.gameObject);
        //}
        if(other.CompareTag("Goal") && !goalReached)
        {
            Debug.Log("ゴールに到達しました！");
            goalReached = true;
        }
    }

    void UpdateTimeText()
    {
        // UIに時間を表示する
        if (timeText != null)
        {
            timeText.text = totaltime.ToString("f1");
        }
    }
    //private void Pouse()
    //{
    //    Time.timeScale = 0;  // 時間停止
    //    pausePanel.SetActive(true);



    //}

    //private void Resume()
    //{
    //    Time.timeScale = 1;  // 再開
    //    pausePanel.SetActive(false);


    //}
}