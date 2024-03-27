using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class AnimetionWindow : MonoBehaviour
{

    //�g���ĂȂ��X�N���v�g------------------------------------------------------------------------------------------------
    private Animator animator;
    public GameObject pausePanel; // SerializedField ���폜���Apublic �ɕύX
    public GameObject target;

    private bool animationTriggered = false; // �A�j���[�V�����̃g���K�[���������ꂽ���ǂ�����ǐՂ���ϐ�

    void Start()
    {
        animator = GetComponent<Animator>();
        //pausePanel.SetActive(false);
    }

    void Update()
    { 

        Vector3 player = target.transform.position;
        float dis = Vector3.Distance(player, transform.position);

        if (dis < 10f && !animationTriggered) // �A�j���[�V�������܂��������Ă��Ȃ��ꍇ�ɂ̂ݎ��s
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
        //pausePanel.SetActive(true); // �I�u�W�F�N�g��\��������
        animator.SetBool("OpenWindow", true); // �A�j���[�V�����̃g���K�[�� true �ɐݒ�
        animationTriggered = true; // �g���K�[���������ꂽ���Ƃ��L�^
        Invoke("HidePanel", 5f); // 3�b��� HidePanel �֐����Ăяo��
    }

    void HidePanel()
    {
        pausePanel.SetActive(false); // �I�u�W�F�N�g���\���ɂ���
    }
}