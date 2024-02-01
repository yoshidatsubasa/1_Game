using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Coincounter : MonoBehaviour
{
    public Coincounter playerController;

    public Text textComponent;

    public static int coinCount;

    float lifeTime = 0.5f;  // 獲得後の生存時間 

    float speed = 100f;

    bool isGet;             // 獲得済みフラグ

   
    public AudioSource coinSound;  // コインが取得された時に再生する音声


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player" && !isGet)
        {
            playerController.AddCoinCount();
            //Destroy(gameObject);
            

            isGet = true;
            transform.position += Vector3.up * 0.5f;

            //サウンドを再生
            if (coinSound != null)
            {
                coinSound.Play();
            }
        }
    }

    public void AddCoinCount()
    {
        coinCount = coinCount + 1;
        Debug.Log("CoinCount;" + coinCount);

        textComponent.text = "x" + coinCount;
    }

    public static int getscore()
    {
        return coinCount;
    }
   

    private void Start()
    {
        GameObject managerObject = GameObject.Find("Coin");

        playerController = managerObject.GetComponent<Coincounter>();

        // AudioSourceコンポーネントを取得する
       coinSound = GetComponent<AudioSource>();

    }

    private void Update()
    {
        // 獲得後
        if (isGet)
        {

            // 素早く回転
            transform.Rotate(Vector3.up * speed * 10f * Time.deltaTime, Space.World);

            // 生存時間を減らす
            lifeTime -= Time.deltaTime;

            // 生存時間が0以下になったら消滅
            if (lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }
        // 獲得前
        else
        {
            // ゆっくり回転
            transform.Rotate(Vector3.up * speed * Time.deltaTime, Space.World);
        }

    }

   
}