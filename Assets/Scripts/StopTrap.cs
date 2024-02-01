using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StopTrap : MonoBehaviour
{
    

    private PlayerController ball;
   

    void Start()
    {
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           

 
            // Ballの動きを司るスクリプトをオフにする。
            ball = collision.gameObject.GetComponent<PlayerController>();
            ball.enabled = false;

            // ２秒後にボールが動けるようにする。
            Invoke("StopOff", 2.0f);

            Debug.Log("ゴール");

          
        }

    }

    void StopOff()
    {
  
        // Ballの動きを司るスクリプトをオンにする。
        ball.enabled = true;
    }

    void Update()
    {
      
    }

}
