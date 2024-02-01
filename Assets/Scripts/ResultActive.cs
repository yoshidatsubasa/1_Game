using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class ResultActive : MonoBehaviour
{


    private PlayerController ball;
    private TimeCounter time;
    public GameObject nextButtan;
    public GameObject resultButtan;
    int stageCoinNum;
    int coinCount;
   


    [SerializeField]
    GameObject clearUI;

    [SerializeField] private GameObject haikei;

    [SerializeField]
    Text coinNumText, resultCoinText;

    [SerializeField] private GameObject pausePanel;

    private Pause pauseScript; // Pause�X�N���v�g�ւ̎Q��


    void Start()
    {
        //resultButtan.SetActive(false);
        // �X�e�[�W���̃R�C���̖������擾
        stageCoinNum = GameObject.FindGameObjectsWithTag("Coin").Length;

        // Pause�X�N���v�g�ւ̎Q�Ƃ��擾
        pauseScript = FindObjectOfType<Pause>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            coinCount = Coincounter.getscore();
            

            // �|�C���g
            // Ball�̓������i��X�N���v�g���I�t�ɂ���B
            ball = collision.gameObject.GetComponent<PlayerController>();
            ball.enabled = false;


            // �|�C���g
            // �Q�b��Ƀ{�[����������悤�ɂ���B
            Invoke("StopOff", 2.0f);

            Debug.Log("�S�[��");

            Time.timeScale = 0;

            pausePanel.SetActive(false);

            nextButtan.SetActive(true);
            resultButtan.SetActive(true);

            clearUI.SetActive(true);

            haikei.SetActive(true);

            resultCoinText.text = coinCount.ToString().PadLeft(3) + "/" + stageCoinNum;

            pauseScript.GameOver();

        }




    }

    public void ChangeScene(string nextScene)
    {
        SceneManager.LoadScene(nextScene);

        Time.timeScale = 1;
        //Coincounter.coinCount = 0;


    }

    public void ChangeScene2(string nextScene)
    {
        SceneManager.LoadScene("TitleScene");
        Time.timeScale = 1;
        Coincounter.coinCount = 0;


    }
    void StopOff()
    {
        // �|�C���g
        // Ball�̓������i��X�N���v�g���I���ɂ���B
        ball.enabled = true;
    }




    void Update()
    {
        coinNumText.text = coinCount.ToString();
    }

}