using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalClearScore : MonoBehaviour
{
    public Text textComponent;
    public Text textComponent2;

    int coinCount;
    float elapsedTime;

    void Start()
    {
        float startTime = DebugScript.gameStartTime;
        Debug.Log("ゲーム開始時刻：" + startTime);

        coinCount = Coincounter.getscore();
        elapsedTime = GetElapsedTime();

        textComponent.text =coinCount.ToString("00");
                            
        textComponent2.text= elapsedTime.ToString("F2");
    }

    float GetElapsedTime()
    {
        float elapsed = Time.time - DebugScript.gameStartTime;
        return elapsed;
    }
}