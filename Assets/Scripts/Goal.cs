using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    [SerializeField]
    GameObject clearUI;

    [SerializeField]
    Text coinNumText, resultCoinText;

    [SerializeField] private GameObject pausePanel;

   

    private PlayerController ball;
    private TimeCounter time;
    public GameObject nextButtan;
    int stageCoinNum;
    int coinCount;

    [SerializeField] Font customFont; //�J�X�^���t�H���g
    private Pause pauseScript; // Pause�X�N���v�g�ւ̎Q��


    void Start()
    {
        // �X�e�[�W���̃R�C���̖������擾
        stageCoinNum = GameObject.FindGameObjectsWithTag("Coin").Length;

        // Pause�X�N���v�g�ւ̎Q�Ƃ��擾
        pauseScript = FindObjectOfType<Pause>();


        //�J�X�^���t�H���g��K�p����
        coinNumText.font = customFont;
        resultCoinText.font = customFont;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            coinCount = Coincounter.getscore();

            // �|�C���g
            // Ball�̓������i��X�N���v�g���I�t�ɂ���B
            ball = collision.gameObject.GetComponent< PlayerController>();
            ball.enabled = false;

           
            // �|�C���g
            // �Q�b��Ƀ{�[����������悤�ɂ���B
            Invoke("StopOff", 2.0f);

            Debug.Log("�S�[��");

            Time.timeScale = 0;
            pausePanel.SetActive(false);

            nextButtan.SetActive(true);

            clearUI.SetActive(true);

            resultCoinText.text = coinCount.ToString().PadLeft(3) + "/" + stageCoinNum;
        }
     
    }

    public void ChangeScene(string nextScene)
    {
        SceneManager.LoadScene(nextScene);

        Time.timeScale = 1;
        Coincounter.coinCount = 0;
        
    }
    public void ChangeResult(string nextScene)
    {
        
        SceneManager.LoadScene("Result");
       
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