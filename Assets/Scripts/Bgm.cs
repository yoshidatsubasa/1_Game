using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgm : MonoBehaviour
{
    public AudioSource bgmAudioSource; // BGMを再生するためのAudioSource
  
    void Start()
    {
        // AudioSourceを探す（例えば、シーン内の任意のオブジェクトにアタッチされたAudioSourceを探す）
        bgmAudioSource = GetComponent<AudioSource>();

        // AudioSourceが見つかった場合、BGMを再生する
        if (bgmAudioSource != null)
        {
            bgmAudioSource.Play();
        }
    }
}