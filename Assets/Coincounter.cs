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

    float lifeTime = 0.5f;  // Šl“¾Œã‚Ì¶‘¶ŠÔ 

    float speed = 100f;

    bool isGet;             // Šl“¾Ï‚İƒtƒ‰ƒO

  
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            playerController.AddCoinCount();
            Destroy(gameObject);
            

            isGet = true;
        }
    }

    public void AddCoinCount()
    {
        coinCount = coinCount + 1;
        Debug.Log("CoinCount;" + coinCount);

        textComponent.text = "X" + coinCount;
    }

    public static int getscore()
    {
        return coinCount;
    }
   

    private void Start()
    {
        GameObject managerObject = GameObject.Find("Coin");

        playerController = managerObject.GetComponent<Coincounter>();

    }

    private void Update()
    {
        // Šl“¾Œã
        if (isGet)
        {
            // ‘f‘‚­‰ñ“]
            transform.Rotate(Vector3.up * speed * 10f * Time.deltaTime, Space.World);

            // ¶‘¶ŠÔ‚ğŒ¸‚ç‚·
            lifeTime -= Time.deltaTime;

            // ¶‘¶ŠÔ‚ª0ˆÈ‰º‚É‚È‚Á‚½‚çÁ–Å
            if (lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }
        // Šl“¾‘O
        else
        {
            // ‚ä‚Á‚­‚è‰ñ“]
            transform.Rotate(Vector3.up * speed * Time.deltaTime, Space.World);
        }

    }

   
}