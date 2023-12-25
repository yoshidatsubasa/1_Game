using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosition : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 currentPosition;
    private Vector3 initialRot;

    //変数を作る
    Rigidbody rb;

    void Start()
    {
        // 初期位置（ゲームスタート時点の位置）
        initialPosition = transform.position;

        // 初期角度（ゲームスタート時点の角度）
        initialRot = transform.eulerAngles;

        //Rigidbodyを取得
        rb = GetComponent<Rigidbody>();


    }

    void Update()
    {
        // 現在の位置
        currentPosition = transform.position;

        if (currentPosition.y < -20f)
        {
            // 初期位置に戻す。
            transform.position = initialPosition;

            // 初期角度に戻す。
            transform.eulerAngles = initialRot;

            //Rigidbodyを停止
            rb.velocity = Vector3.zero;
        }
    }
}