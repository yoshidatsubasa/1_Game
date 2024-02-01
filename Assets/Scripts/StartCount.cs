using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartCount : MonoBehaviour
{
    //[SerializeField] private GameObject pausePanel;

    //���Ԃ�\������Text�^�̕ϐ�
   
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

    public AudioSource bgmAudioSource; // BGM�p��AudioSource
    public AudioClip bgmClip; // BGM��AudioClip

    public AudioSource source; // ���ʉ��p��AudioSource
    public AudioClip countdownClip; // �J�E���g�_�E�����ʉ���AudioClip

    bool startBGM = false; // BGM���Đ�����t���O
    bool countdownStarted = false; // �J�E���g�_�E�����J�n���ꂽ���ǂ����̃t���O

    bool goalReached = false; // �S�[���ɓ��B�������ǂ����̃t���O

    void Start()
    {
        
   
        //pausePanel.SetActive(true);
       
        //Time.timeScale = 0;  // ���Ԓ�~
        
    }

    private void Update()
    {
        if (!countdownStarted)
        {
            countdownStarted = true;

            // �J�E���g�_�E�����n�܂�u�ԂɌ��ʉ����Đ�����
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
            CountText3.text = "���ԓ��ɃS�[������ڎw���I";

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
            StartCoroutine(PlayBGMAfterDelay(0.5f)); // �J�E���g�_�E���I����A1�b���BGM�Đ�

        }


        if (totaltime <= 10.0f && totaltime >= 5.0f)
        {
            timeText.color = Color.red; // 5�b�ȏ�10�b�ȉ��̊ԁAtimeText�̐F��Ԃ�����
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
        //    Debug.Log("�A�C�e�����擾���܂����I");
        //    totaltime += 10.0f;
        //    UpdateTimeText();
        //    Destroy(other.gameObject);
        //}
        if(other.CompareTag("Goal") && !goalReached)
        {
            Debug.Log("�S�[���ɓ��B���܂����I");
            goalReached = true;
        }
    }

    void UpdateTimeText()
    {
        // UI�Ɏ��Ԃ�\������
        if (timeText != null)
        {
            timeText.text = totaltime.ToString("f1");
        }
    }
    //private void Pouse()
    //{
    //    Time.timeScale = 0;  // ���Ԓ�~
    //    pausePanel.SetActive(true);



    //}

    //private void Resume()
    //{
    //    Time.timeScale = 1;  // �ĊJ
    //    pausePanel.SetActive(false);


    //}
}