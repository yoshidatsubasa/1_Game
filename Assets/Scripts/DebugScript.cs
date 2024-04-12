using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DebugScript : MonoBehaviour
{

    public float delayTime = 1.0f; // �V�[���J�ڂ܂ł̑ҋ@����
    public AudioSource returnSound;
    public static float gameStartTime = 0f;

   
    [SerializeField] private Slider _slider;

    [SerializeField] private GameObject _loadingUI;
    IEnumerator LoadSceneAfterDelay(string nextScene)
    {
      
        yield return new WaitForSeconds(delayTime); // �w�肵�����Ԃ����ҋ@����
       //SceneManager.LoadScene(nextScene); // �w�肵���V�[���ɑJ�ڂ���
        AsyncOperation async = SceneManager.LoadSceneAsync(nextScene);
        while (!async.isDone)
        {
            _slider.value = async.progress;
            yield return null;
        }
    }

    public void ChangeScene(string nextScene)
    {
        StartCoroutine(LoadSceneAfterDelay(nextScene)); // �R���[�`�����J�n���ăV�[���J�ڂ��s��
        Time.timeScale = 1;
        Coincounter.coinCount = 0;
       
    }

    public void ChangeScene2(string nextScene)
    {
        StartCoroutine(LoadSceneAfterDelay(nextScene));
        Time.timeScale = 1;
        Coincounter.coinCount = 0;
    }
   
    public void ChangeScene3(string nextScene)
    {
        StartCoroutine(LoadSceneAfterDelay(nextScene)); // �R���[�`�����J�n���ăV�[���J�ڂ��s��
        Time.timeScale = 1;
        Coincounter2.coinCount = 0;
    }
    public void ChangeScene4(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
        Time.timeScale = 1;
        Coincounter.coinCount = 0;

    }

    public void ChangeScene5(string nextScene)
    {
        _loadingUI.SetActive(true);
        StartCoroutine(LoadSceneAfterDelay(nextScene)); // �R���[�`�����J�n���ăV�[���J�ڂ��s��
        Time.timeScale = 1;
        Coincounter.coinCount = 0;
        gameStartTime = Time.time;

    }

    public void ChangeScene6(string nextScene)
    {
        StartCoroutine(LoadSceneAfterDelay(nextScene));
        Time.timeScale = 1;
    }
    public void SceneReset()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(LoadSceneAfterDelay(activeSceneName));
        Time.timeScale = 1;
        Coincounter.coinCount = 0;
        Coincounter2.coinCount = 0;
    }

  

    void ReturnSound()
    {
        if (returnSound != null)
        {
            returnSound.Play(); // ���ʉ����Đ�����
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �V�[����Select�̎�����LB�{�^���̓��͂���������
        if (SceneManager.GetActiveScene().name == "Select")
        {
            if (Input.GetButtonDown("LBButton"))
            {
                ReturnSound();
                ChangeScene2("TitleScene");
            }
        }
        if (SceneManager.GetActiveScene().name == "Rule")
        {
            if (Input.GetButtonDown("LBButton2"))
            {
                ReturnSound();
                ChangeScene2("Option");
            }
        }
        if (SceneManager.GetActiveScene().name == "Option")
        {
            if (Input.GetButtonDown("LBButton3"))
            {
                ReturnSound();
                ChangeScene2("TitleScene");
            }
        }
        if (SceneManager.GetActiveScene().name == "PlayMove")
        {
            if (Input.GetButtonDown("LBButton4"))
            {
                ReturnSound();
                ChangeScene2("Option");
            }
        }

        //if (Input.GetKey(KeyCode.Q))
        //  {
        //    SceneManager.LoadScene("Level2");
        //    Time.timeScale = 1;
        //    Coincounter.coinCount = 0;
        //  }
      

    }
    

}
