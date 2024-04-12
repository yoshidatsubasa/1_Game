using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarSetumeiWindow : MonoBehaviour
{

    public GameObject setumeiWindow1; // �V�[��1�p��UI
    public GameObject setumeiWindow2; // �V�[��2�p��UI
    public GameObject setumeiWindow3; // �V�[��3�p��UI
    public GameObject player; // �v���C���[

    private bool animationTriggered1 = false; // �V�[��1�p�̃A�j���[�V�����̃g���K�[���������ꂽ���ǂ�����ǐՂ���ϐ�
    private bool animationTriggered2 = false; // �V�[��2�p�̃A�j���[�V�����̃g���K�[���������ꂽ���ǂ�����ǐՂ���ϐ�
    private bool animationTriggered3 = false; // �V�[��2�p�̃A�j���[�V�����̃g���K�[���������ꂽ���ǂ�����ǐՂ���ϐ�

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = player.transform.position;

        // �V�[��1��UI�𐧌�
        if (Vector3.Distance(transform.position, playerPosition) < 5f && !animationTriggered1)
        {
            StartCoroutine(ShowNewUIAfterDelay(1f, 1));   // 1�b��ɃV�[��1��UI��\������
            StartCoroutine(HideNewUIAfterDelay(9f, 1));   // 9�b��ɃV�[��1��UI���\���ɂ���
        }

        // �V�[��2��UI�𐧌�
        if (Vector3.Distance(transform.position, playerPosition) < 16f && !animationTriggered2)
        {
            StartCoroutine(ShowNewUIAfterDelay(1f, 2));   // 1�b��ɃV�[��2��UI��\������
            StartCoroutine(HideNewUIAfterDelay(9f, 2));   // 9�b��ɃV�[��2��UI���\���ɂ���
        }

        // �V�[��3��UI�𐧌�
        if (Vector3.Distance(transform.position, playerPosition) < 10f && !animationTriggered3)
        {
            StartCoroutine(ShowNewUIAfterDelay(1f, 3));   // 1�b��ɃV�[��3��UI��\������
            StartCoroutine(HideNewUIAfterDelay(9f, 3));   // 9�b��ɃV�[��3��UI���\���ɂ���
        }

        if (Input.GetButton("BButton"))
        {
            setumeiWindow1.SetActive(false);
            setumeiWindow2.SetActive(false);
            setumeiWindow3.SetActive(false);
          
        }
    }

    // UiWindow�\��
    IEnumerator ShowNewUIAfterDelay(float delay, int sceneNumber)
    {
        if (sceneNumber == 1)
            animationTriggered1 = true; // �V�[��1�p�̃g���K�[���������ꂽ���Ƃ��L�^
        else if (sceneNumber == 2)
            animationTriggered2 = true; // �V�[��2�p�̃g���K�[���������ꂽ���Ƃ��L�^
        else if (sceneNumber == 3)
            animationTriggered3 = true; // �V�[��2�p�̃g���K�[���������ꂽ���Ƃ��L�^

        yield return new WaitForSeconds(delay);

        // �V�[��1��UI��\��
        if (sceneNumber == 1)
        {
            setumeiWindow1.SetActive(true);
        }
        // �V�[��2��UI��\��
        else if (sceneNumber == 2)
        {
            setumeiWindow2.SetActive(true);
        }

        // �V�[��3��UI��\��
        else if (sceneNumber == 3)
        {
            setumeiWindow3.SetActive(true);
        }
    }

    // UiWindow��\��
    IEnumerator HideNewUIAfterDelay(float delay, int sceneNumber)
    {
        yield return new WaitForSeconds(delay);

        // �V�[��1��UI���\��
        if (sceneNumber == 1)
        {
            setumeiWindow1.SetActive(false);
        }
        // �V�[��2��UI���\��
        else if (sceneNumber == 2)
        {
            setumeiWindow2.SetActive(false);
        }
        // �V�[��3��UI���\��
        else if (sceneNumber == 3)
        {
            setumeiWindow3.SetActive(false);
        }
    }
}