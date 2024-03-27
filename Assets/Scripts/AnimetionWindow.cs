using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class AnimetionWindow : MonoBehaviour
{

    //使ってないスクリプト------------------------------------------------------------------------------------------------
    private Animator animator;
    public GameObject pausePanel; // SerializedField を削除し、public に変更
    public GameObject target;

    private bool animationTriggered = false; // アニメーションのトリガーが発動されたかどうかを追跡する変数

    void Start()
    {
        animator = GetComponent<Animator>();
        //pausePanel.SetActive(false);
    }

    void Update()
    { 

        Vector3 player = target.transform.position;
        float dis = Vector3.Distance(player, transform.position);

        if (dis < 10f && !animationTriggered) // アニメーションがまだ発動していない場合にのみ実行
        {
            SphereGravity();
            pausePanel.SetActive(true);
        }
        else
        {
            pausePanel.SetActive(false);
        }

    }

    void SphereGravity()
    {
        //pausePanel.SetActive(true); // オブジェクトを表示させる
        animator.SetBool("OpenWindow", true); // アニメーションのトリガーを true に設定
        animationTriggered = true; // トリガーが発動されたことを記録
        Invoke("HidePanel", 5f); // 3秒後に HidePanel 関数を呼び出す
    }

    void HidePanel()
    {
        pausePanel.SetActive(false); // オブジェクトを非表示にする
    }
}