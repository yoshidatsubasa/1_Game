//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Bullet : MonoBehaviour
//{
//    public GameObject bulletPrefab; // 弾のプレハブ
//    public float bulletSpeed = 10f; // 弾の速度
//    public float triggerDistance = 10f; // プレイヤーを感知する距離

//    private bool playerInRange = false; // プレイヤーが範囲内にいるかどうかのフラグ

//    private void Update()
//    {
//        // プレイヤーのTransformを取得
//        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
//        if (playerObject != null)
//        {
//            Transform playerTransform = playerObject.transform;
//            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

//            // プレイヤーが指定した距離以内にいる場合
//            if (distanceToPlayer <= triggerDistance)
//            {
//                if (!playerInRange)
//                {
//                    // 範囲内にプレイヤーが入ったので弾を発射する
//                    ShootBullet();
//                    playerInRange = true; // 弾を発射したことを記録する
//                }
//            }
//            else
//            {
//                playerInRange = false; // プレイヤーが範囲外に出たらフラグをリセットする
//            }
//        }
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            PlayerController playerController = other.GetComponent<PlayerController>();
//            if (playerController != null)
//            {
//                playerController
//            }

          
//        }
//    }

//    private void ShootBullet()
//    {
//        // 弾の発射処理
//        GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
//        Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();

//        // 左側に弾を発射するベクトルを設定
//        Vector3 direction = Vector3.left;

//        // 弾に速度を与える（左方向に飛ばす）
//        bulletRigidbody.velocity = direction * bulletSpeed;

//        // 一定時間後に弾を破棄する
//        Destroy(newBullet, 5f);
//    }
//}