using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public List<AudioSource> audioSources; // ������AudioSource���Ǘ����邽�߂̃��X�g

    public Image imageWhenOn; // ON�̎��ɕ\������摜��Image�R���|�[�l���g
    public Image imageWhenOff; // OFF�̎��ɕ\������摜��Image�R���|�[�l���g



    private bool isOn = true;

    void Start()
    {
        // �Q�[���J�n����OFF�̉摜���\���ɂ���
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
            if (Input.GetButtonDown("RBButton")) // RB�{�^���������ꂽ��
            {
                ToggleMute(); // �T�E���h�̃I���E�I�t��؂�ւ���
            }
        }
       
        }
    public void ToggleMute()
    {
        isOn = !isOn;

        foreach (var audioSource in audioSources)
        {
            audioSource.mute = !isOn; // isMuted�̏�Ԃɉ����ĊeAudioSource�̃~���[�g��؂�ւ���
        }

        if (imageWhenOn != null && imageWhenOff != null)
        {
            imageWhenOn.enabled = isOn; // ON�̎��̉摜��\���܂��͔�\���ɂ���
            imageWhenOff.enabled = !isOn; // OFF�̎��̉摜��\���܂��͔�\���ɂ���
        }
    }
}