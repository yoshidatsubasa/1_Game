using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgm : MonoBehaviour
{
    public AudioSource bgmAudioSource; // BGM���Đ����邽�߂�AudioSource
  
    void Start()
    {
        // AudioSource��T���i�Ⴆ�΁A�V�[�����̔C�ӂ̃I�u�W�F�N�g�ɃA�^�b�`���ꂽAudioSource��T���j
        bgmAudioSource = GetComponent<AudioSource>();

        // AudioSource�����������ꍇ�ABGM���Đ�����
        if (bgmAudioSource != null)
        {
            bgmAudioSource.Play();
        }
    }
}