using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    [SerializeField]
    GameObject clearUI;

    [SerializeField]
    Text coinNumText, resultCoinText;

    private PlayerController ball;
    private TimeCounter time;
    public GameObject nextButtan;
    int stageCoinNum;
    int coinCount;




    void Start()
    {
        // ステージ内のコインの枚数を取得
        stageCoinNum = GameObject.FindGameObjectsWithTag("Coin").Length;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            coinCount = Coincounter.getscore();

            // ポイント
            // Ballの動きを司るスクリプトをオフにする。
            ball = collision.gameObject.GetComponent< PlayerController>();
            ball.enabled = false;

           
            // ポイント
            // ２秒後にボールが動けるようにする。
            Invoke("StopOff", 2.0f);

            Debug.Log("ゴール");

            Time.timeScale = 0;

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
        // ポイント
        // Ballの動きを司るスクリプトをオンにする。
        ball.enabled = true;
    }

    void Update()
    {
        coinNumText.text = coinCount.ToString();
    }

}
