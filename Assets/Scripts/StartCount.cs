using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartCount : MonoBehaviour
{
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

    public AudioSource source1;
    public AudioClip clip1;

    public AudioSource source2;
    public AudioClip clip2;

    bool startBGM = false; // BGM���Đ�����t���O
    bool countdownStarted = false; // �J�E���g�_�E�����J�n���ꂽ���ǂ����̃t���O

    bool goalReached = false; // �S�[���ɓ��B�������ǂ����̃t���O

    public GameObject setumeiWindow; // �����Ƃ��Ă���Ui
    public GameObject setumeiWindow2;
    public GameObject setumeiWindow3;
    public GameObject setumeiWindow4;
    public GameObject TimeUpWindow;
    public GameObject[] countDownObjects; // 10�b�O�̃I�u�W�F�N�g���i�[����z��


    public GameObject playerGameObject; // �v���C���[�I�u�W�F�N�g
    private PlayerController playerController; // �v���C���[�R���g���[���[

    bool playedCountdownSound = false; // �J�E���g�_�E�����ʉ����Đ��������ǂ����̃t���O
    void Start()
    {
        // ����������11�b�O�̃I�u�W�F�N�g���A�N�e�B�u�ɂ���
        foreach (var obj in countDownObjects)
        {
            obj.SetActive(false);
        }

        // �v���C���[�I�u�W�F�N�g����PlayerController�R���|�[�l���g���擾
        playerController = playerGameObject.GetComponent<PlayerController>();
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
            CountText.text = "";
            CountText2.text = "Start!!";
            CountText3.text = "";
            totaltime -= Time.deltaTime;
            retTime = (float)totaltime;
            timeText.text = retTime.ToString("f1");
            if(retTime<= 0 && !goalReached)
            {
                StartCoroutine(LoadSceneAfterDelay("Over", 2f)); // 2�b��ɃS�[���V�[�������[�h
            }
            else if (retTime <= 0.5 && !goalReached)
            {
                TimeUpWindow.SetActive(true);
            }
            else if (retTime <= 2 && !goalReached)
            {
                source1.Play();
            }

            // 10�b�O�̃I�u�W�F�N�g��\������ۂɈ�x�������ʉ����Đ�����
            if (totaltime<=10.8f && !playedCountdownSound)
            {
                // �J�E���g�_�E���̌��ʉ����Đ�����
                if (clip2 != null)
                {
                    source2.PlayOneShot(clip2);
                    playedCountdownSound = true; // �J�E���g�_�E�����ʉ����Đ�����
                }
            }

        }
        if(CountText2.text== "Start!!" && !startBGM)
        {
            Destroy(CountText2, 0.5f);
            StartCoroutine(PlayBGMAfterDelay(0.5f));   // �J�E���g�_�E���I����A1�b���BGM�Đ�
            StartCoroutine(ShowNewUIAfterDelay(1f));   // 1�b��ɐV����UI��\������
            StartCoroutine(HideNewUIAfterDelay(9f));   // 9�b��ɐV����UI���\���ɂ���
            StartCoroutine(ShowNewUIAfterDelay2(8f));  // �����8�b��Ɏ���UI��\������
            StartCoroutine(HideNewUIAfterDelay2(17f)); // 17�b��ɐV����UI���\���ɂ���
            StartCoroutine(ShowNewUIAfterDelay3(15f)); // �����15�b��Ɏ���UI��\������
            StartCoroutine(HideNewUIAfterDelay3(25f)); // 25�b��ɐV����UI���\���ɂ���
            StartCoroutine(ShowNewUIAfterDelay4(1f));  // 1�b��Ɏ���UI��\������
            StartCoroutine(HideNewUIAfterDelay4(12f)); // 13�b��ɐV����UI���\���ɂ���
        }

        
        if (totaltime <= 10.0f && totaltime >= 5.0f)
        {
            timeText.color = Color.red; // 5�b�ȏ�10�b�ȉ��̊ԁAtimeText�̐F��Ԃ�����
        }
        // �J�E���g�_�E���I�u�W�F�N�g�̕\����ݒ肷��
        if (totaltime <= 11 && totaltime >= 1)
        {
            int indexToShow = 11 - Mathf.RoundToInt(totaltime); // �\������I�u�W�F�N�g�̃C���f�b�N�X���v�Z
            if (indexToShow >= 0 && indexToShow < countDownObjects.Length)
            {
                countDownObjects[indexToShow].SetActive(true); // �Ή�����I�u�W�F�N�g��\��
                StartCoroutine(HideCountDownObjectDelayed(countDownObjects[indexToShow])); // 1�b��ɔ�\���ɂ���
            }
        }

        if (Input.GetButton("BButton"))
        {
            setumeiWindow.SetActive(false);
            setumeiWindow2.SetActive(false);
            setumeiWindow3.SetActive(false);
            setumeiWindow4.SetActive(false);
        }

        // �^�C���A�b�v�E�B���h�E���\�����ꂽ�ꍇ�A�v���C���[�̓������~����
        if (TimeUpWindow.activeSelf)
        {
           
            // �v���C���[�R���g���[���[��null�łȂ����Ƃ��m�F���Ă���A�������~����
            if (playerController != null)
            {
                playerController.StopMovement(); // �v���C���[�̓������~���郁�\�b�h���Ăяo��
            }

            setumeiWindow.SetActive(false);
            setumeiWindow2.SetActive(false);
            setumeiWindow3.SetActive(false);
            setumeiWindow4.SetActive(false);
        }
    }

   
    //UiWindow�\��
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

    //UiWindow��\��
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

    //BGM�Đ�
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
            Debug.Log("�S�[���ɓ��B���܂����I");
            goalReached = true;
        }
    }

    IEnumerator HideCountDownObjectDelayed(GameObject obj)
    {
        yield return new WaitForSeconds(1f); // 1�b�ҋ@
        obj.SetActive(false); // �I�u�W�F�N�g���\���ɂ���
    }

    IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        totaltime = 0;
        yield return new WaitForSeconds(delay); // �w�肵���b���ҋ@
        SceneManager.LoadScene(sceneName); // �V�[�������[�h
    }

}