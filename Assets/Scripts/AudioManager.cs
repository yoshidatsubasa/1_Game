using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public List<AudioSource> audioSources; // 複数のAudioSourceを管理するためのリスト

    public Image imageWhenOn; // ONの時に表示する画像のImageコンポーネント
    public Image imageWhenOff; // OFFの時に表示する画像のImageコンポーネント



    private bool isOn = true;

    void Start()
    {
        // ゲーム開始時にOFFの画像を非表示にする
        if (imageWhenOff != null)
        {
            imageWhenOff.enabled = false;
        }
    }
    void Update()
    {
        
        bool isSelectScene = SceneManager.GetActiveScene().name == "Select";
        bool isSelectScene2 = SceneManager.GetActiveScene().name == "TitleScene";

        if (isSelectScene || isSelectScene2|| Time.timeScale == 0)
        {
            if (Input.GetButtonDown("RBButton")) // RBボタンが押された時
            {
                ToggleMute(); // サウンドのオン・オフを切り替える
            }
        }
       
        }
    public void ToggleMute()
    {
        isOn = !isOn;

        foreach (var audioSource in audioSources)
        {
            audioSource.mute = !isOn; // isMutedの状態に応じて各AudioSourceのミュートを切り替える
        }

        if (imageWhenOn != null && imageWhenOff != null)
        {
            imageWhenOn.enabled = isOn; // ONの時の画像を表示または非表示にする
            imageWhenOff.enabled = !isOn; // OFFの時の画像を表示または非表示にする
        }
    }
}