//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Wall : MonoBehaviour
//{
//    void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            // プレイヤーが壁に当たったら、プレイヤーの移動を停止する
//            PlayerController playerController = other.GetComponent<PlayerController>();
//            if (playerController != null)
//            {
//                playerController.StopPlayerMovement();
//            }
//        }
//    }
//}