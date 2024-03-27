//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class tamesi : MonoBehaviour
//{
//    private Animator animator;
//    private Rigidbody playerRigidbody;
//    private GameObject rideableObject;
//    private GameObject warptimeObject; // "warptime" オブジェクトへの参照を保持する変数
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

//            // "warptime" オブジェクトを検索して参照を保持
//            warptimeObject = GameObject.Find("warptime");
//            if (warptimeObject != null)
//            {
//                // "warptime" オブジェクトを非表示にする
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

//            // 2秒後に乗っているオブジェクトを非表示にする
//            StartCoroutine(HideRideableObjectAfterDelay(2f));

//            // プレイヤーの動きを停止する
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

//            // "warptime" オブジェクトを表示する
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
//            // 乗っているオブジェクトを非表示にする
//            rideableObject.SetActive(false);
//        }
//        else
//        {
//            Debug.LogWarning("Rideable object is not set.");
//        }
//    }
//}