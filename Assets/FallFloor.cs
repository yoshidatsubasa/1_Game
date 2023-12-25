using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloor : MonoBehaviour
{
    bool floor_touch; //床に触れたかの判定`
    public float downSpeed; //落ちるスピード
    float fallCount; //床が落ちるまでの時間
    Rigidbody2D rb; //Rigidbodyの宣言

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Rigidbodyの取得
        fallCount = 0; //fullCpuntを初期化
    }

    // Update is called once per frame
    void Update()
    {
        //床に触れたら
        if (floor_touch == true)
        {
            //fallCountを1秒ずつ増やす。
            fallCount += Time.deltaTime;
            //DownStart関数を使う
            DownStart();
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        //プレイヤータグが付いているオブジェクトに当たったら
        if (col.gameObject.tag == "Player")
        {
            fallCount = 0; //fallCountを初期化
            floor_touch = true; //floor_touchをtrueにする。
        }
    }

    //数秒後に床が落ちる
    void DownStart()
    {
        //fallCountが何秒かたったら
        if (fallCount >= 3.0f)
        {
            transform.Translate(0, -downSpeed, 0); //Y座標をdownSpeedずつ変える。
        }
    }
}
