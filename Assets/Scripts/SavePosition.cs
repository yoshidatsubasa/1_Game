using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePosition : MonoBehaviour
{
    // 中間フラッグの位置
    public Transform checkpoint;

    // プレイヤーの初期位置
    private Vector3 initialPosition;

    // シーンが遷移してもオブジェクトが破棄されないようにするためのフラグ
    private static bool created = false;

    void Start()
    {
        // シーンが遷移してもオブジェクトが破棄されないようにする
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }

        // プレイヤーの初期位置を保存
        initialPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        // 中間フラッグに触れた場合
        if (other.CompareTag("Checkpoint"))
        {
            Debug.Log("Player touched the checkpoint!");
            // プレイヤーの現在の位置からy座標-20を超えた場合
            if (transform.position.y < checkpoint.position.y - 20f)
            {
                // プレイヤーを中間フラッグの位置に戻す
                ResetToCheckpoint();
            }

            // 中間フラッグを非アクティブにする
            other.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // プレイヤーが一定の高さより下に落ちたら
        if (transform.position.y < -20f)
        {
            // プレイヤーを中間フラッグの位置に戻す
            ResetToCheckpoint();
        }
    }

    // プレイヤーを中間フラッグの位置に戻すメソッド
    private void ResetToCheckpoint()
    {
        transform.position = new Vector3(checkpoint.position.x, checkpoint.position.y, checkpoint.position.z);
    }

    // シーン遷移前に中間フラッグの位置を保存
    public static void SaveCheckpointPosition(Vector3 position)
    {
        checkpointPosition = position;
    }

    // 中間フラッグの位置を取得するためのプロパティ
    public static Vector3 CheckpointPosition
    {
        get { return checkpointPosition; }
    }

    // 中間フラッグの位置
    private static Vector3 checkpointPosition;
}