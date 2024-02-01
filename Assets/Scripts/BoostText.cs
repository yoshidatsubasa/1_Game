using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BoostText : MonoBehaviour
{
    //テキスト点滅プログラム
    public float speed = 2.0f;
    private Text text;
    private float time;
    void Start()
    {
        text = this.gameObject.GetComponent<Text>();
    }
    private void Update()
    {
        text.color = GetAlphaColor(text.color);
    }

    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5.0f * speed;
        color.a = Mathf.Sin(time);

        return color;
    }
}
