using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    //回転スピード
    [SerializeField] private Vector3 RotationSpeed = new Vector3();　//←追加

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //オイラー角を変更する
        transform.eulerAngles += RotationSpeed * Time.deltaTime;　//←追加
    }
}