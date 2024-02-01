using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Goal2 : MonoBehaviour
{
    [SerializeField]
    GameObject clearUI;

    
    public Text coinNumText, resultCoinText;

    private PlayerController ball;
   
    [SerializeField] private GameObject pausePanel;
   
    private TimeCounter time;
    public GameObject selectButtan;
    public GameObject titleButtan;
    int stageCoinNum;
    int coinCount;

    //[SerializeField] Font customFont; //�J�X�^���t�H���g

    private Pause pauseScript; // Pause�X�N���v�g�ւ̎Q��
   
    void Start()
    {
        // �X�e�[�W���̃R�C���̖������擾
        stageCoinNum = GameObject.FindGameObjectsWithTag("Coin").Length;

        // Pause�X�N���v�g�ւ̎Q�Ƃ��擾
        pauseScript = FindObjectOfType<Pause>();

        ////�J�X�^���t�H���g��K�p����
        //coinNumText.font = customFont;
        //resultCoinText.font = customFont;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            coinCount = Coincounter.getscore();

            
            // Ball�̓������i��X�N���v�g���I�t�ɂ���B
            ball = collision.gameObject.GetComponent<PlayerController>();
            ball.enabled = false;

           

            // �Q�b��Ƀ{�[����������悤�ɂ���B
            //Invoke("StopOff", 2.0f);

            Debug.Log("�S�[��");

            Time.timeScale = 0;

            pausePanel.SetActive(false);

            selectButtan.SetActive(true);

            titleButtan.SetActive(true);

            clearUI.SetActive(true);

            resultCoinText.text = coinCount.ToString().PadLeft(3) + "/" + stageCoinNum;

            pauseScript.GameOver();
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

        SceneManager.LoadScene("Over");

    }

    //void StopOff()
    //{
    //    // �|�C���g
    //    // Ball�̓������i��X�N���v�g���I���ɂ���B
    //    ball.enabled = true;
    //}

    void Update()
    {
       
        coinNumText.text = coinCount.ToString();
    }

}
