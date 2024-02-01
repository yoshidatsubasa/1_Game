using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class ResultActive2 : MonoBehaviour
{

    int stageCoinNum;
    int coinCount;

    [SerializeField]
    Text coinNumText, resultCoinText;

    void Start()
    {
      
        // ステージ内のコインの枚数を取得
        stageCoinNum = GameObject.FindGameObjectsWithTag("Coin").Length;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            coinCount = Coincounter2.getscore();

            resultCoinText.text = coinCount.ToString().PadLeft(3) + "/" + stageCoinNum;

        }

    }


    void Update()
    {
        coinNumText.text = coinCount.ToString();
    }

}