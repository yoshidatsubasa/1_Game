using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoalAnimtion : MonoBehaviour
{
    public GameObject playerGameObject; // �v���C���[�I�u�W�F�N�g
    private PlayerController playerController; // �v���C���[�R���g���[���[

    public GameObject ClearUpWindow;
    public Text timeText; // ���Ԃ�\������e�L�X�g

    public GameObject setumeiWindow; //  RadarSetumeiWindow�Őݒ肳�ꂽUI
    public GameObject setumeiWindow2; // RadarSetumeiWindow�Őݒ肳�ꂽUI
    public GameObject setumeiWindow3; // RadarSetumeiWindow�Őݒ肳�ꂽUI
    public GameObject setumeiWindow4; // RadarSetumeiWindow�Őݒ肳�ꂽUI


    public AudioSource source1;
    public AudioClip clip1;

    public GameObject[] countDownObjects; // 10�b�O�̃I�u�W�F�N�g���i�[����z��

    private bool hasEnded = false; // �Q�[�����I���������ǂ����������t���O
    private StartCount startCount;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        startCount = FindObjectOfType<StartCount>();

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ClearUpWindow.SetActive(true);
            StartCoroutine(LoadSceneAfterDelay("Clear", 2f)); // 2�b��ɃS�[���V�[�������[�h

            source1.Play();

            hasEnded = true; // �Q�[�����I���������Ƃ������t���O��ݒ�
        }
    }

    private void Update()
    {
        // �^�C���A�b�v�E�B���h�E���\�����ꂽ�ꍇ�A���Ԃ��\���ɂ���
        if (ClearUpWindow.activeSelf)
        {
            // �v���C���[�̓������~����
            if (playerController != null)
            {
                playerController.StopMovement2();
            }

            // ���Ԃ��\���ɂ���
            if (timeText != null)
            {
                timeText.gameObject.SetActive(false);
            }


            // �S�[���ɐG�ꂽ�Ƃ���RadarSetumeiWindow��setumeiWindow���\���ɂ���
            if (setumeiWindow != null)
            {
                setumeiWindow.SetActive(false);
            }
            if (setumeiWindow2 != null)
            {
                setumeiWindow2.SetActive(false);
            }
            if (setumeiWindow3 != null)
            {
                setumeiWindow3.SetActive(false);
            }
            if (setumeiWindow4 != null)
            {
                setumeiWindow4.SetActive(false);
            }

            // �Q�[�����I�����Ă��Ȃ��ꍇ�̂�TimeUpWindow���\���ɂ���
            if (hasEnded)
            {
                startCount.TimeUpWindow.SetActive(false);
                startCount.source2.Stop();
                startCount.source1.Stop();
            }
           
            // �S�[��������countDownObjects�̃I�u�W�F�N�g���\���ɂ���
            if (countDownObjects != null)
            {
                foreach (GameObject obj in countDownObjects)
                {
                    obj.SetActive(false);
                }
            }
        }
    }

   
    IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay); // �w�肵���b���ҋ@
        SceneManager.LoadScene(sceneName); // �V�[�������[�h
    }
}