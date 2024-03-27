//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class tamesi : MonoBehaviour
//{
//    private Animator animator;
//    private Rigidbody playerRigidbody;
//    private GameObject rideableObject;
//    private GameObject warptimeObject; // "warptime" �I�u�W�F�N�g�ւ̎Q�Ƃ�ێ�����ϐ�
//    private bool isRiding = false;

//    void Start()
//    {
//        animator = GetComponent<Animator>();
//        playerRigidbody = GetComponent<Rigidbody>();
//    }

//    void OnCollisionEnter(Collision collision)
//    {
//        if (!isRiding && collision.gameObject.CompareTag("Player"))
//        {
//            isRiding = true;
//            rideableObject = collision.gameObject;
//            animator.SetBool("Warp1", true);

//            // "warptime" �I�u�W�F�N�g���������ĎQ�Ƃ�ێ�
//            warptimeObject = GameObject.Find("warptime");
//            if (warptimeObject != null)
//            {
//                // "warptime" �I�u�W�F�N�g���\���ɂ���
//                Rigidbody warptimeRigidbody = warptimeObject.GetComponent<Rigidbody>();
//                if (warptimeRigidbody != null)
//                {
//                    warptimeRigidbody.velocity = Vector3.zero;
//                    warptimeRigidbody.angularVelocity = Vector3.zero;
//                }
//                warptimeObject.SetActive(false);
//            }
//            else
//            {
//                Debug.LogWarning("warptime object not found.");
//            }

//            // 2�b��ɏ���Ă���I�u�W�F�N�g���\���ɂ���
//            StartCoroutine(HideRideableObjectAfterDelay(2f));

//            // �v���C���[�̓������~����
//            if (playerRigidbody != null)
//            {
//                playerRigidbody.velocity = Vector3.zero;
//                playerRigidbody.angularVelocity = Vector3.zero;
//            }
//            else
//            {
//                Debug.LogWarning("Player Rigidbody is not attached.");
//            }
//        }
//    }

//    void OnCollisionExit(Collision collision)
//    {
//        if (isRiding && collision.gameObject == rideableObject)
//        {
//            isRiding = false;
//            animator.SetBool("Warp1", false);

//            // "warptime" �I�u�W�F�N�g��\������
//            if (warptimeObject != null)
//            {
//                warptimeObject.SetActive(true);
//            }
//        }
//    }

//    IEnumerator HideRideableObjectAfterDelay(float delay)
//    {
//        yield return new WaitForSeconds(delay);

//        if (rideableObject != null)
//        {
//            // ����Ă���I�u�W�F�N�g���\���ɂ���
//            rideableObject.SetActive(false);
//        }
//        else
//        {
//            Debug.LogWarning("Rideable object is not set.");
//        }
//    }
//}