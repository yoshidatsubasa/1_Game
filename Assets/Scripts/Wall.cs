//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Wall : MonoBehaviour
//{
//    void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            // �v���C���[���ǂɓ���������A�v���C���[�̈ړ����~����
//            PlayerController playerController = other.GetComponent<PlayerController>();
//            if (playerController != null)
//            {
//                playerController.StopPlayerMovement();
//            }
//        }
//    }
//}