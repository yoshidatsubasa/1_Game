using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Lazer : MonoBehaviour
{
    public GameObject player; // プレイヤーの参照
    public ParticleSystem particleEffect; // パーティクルエフェクトの参照

    //private bool hasPlayed = false; // エフェクトが再生されたかどうかを示すフラ

    public int damageAmount = 1; // パーティクルエフェクトが与えるダメージ量

    private void Start()
    {
        // プレイヤーの参照を取得
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // パーティクルエフェクトの初期状態は非アクティブにする
        if (particleEffect != null)
        {
            particleEffect.Stop();
        }
    }
   
    private void Update()
    {
        if (player.GetComponent<LazerErea>().invaded == true)
        {
            if (particleEffect != null)
            {
                particleEffect.Play();
            }
        }
      }
    private void OnParticleCollision(GameObject other)
    {
        // 衝突したオブジェクトがプレイヤーであるかどうかを確認する
        if (other.CompareTag("Player"))
        {
            // プレイヤーにダメージを与える
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                // プレイヤーがパーティクルエフェクトの範囲内にいる場合のみダメージを与える
                if (particleEffect != null && particleEffect.IsAlive(true))
                {
                    playerController.TakeDamage(damageAmount);
                }
            }
        }
    }

}