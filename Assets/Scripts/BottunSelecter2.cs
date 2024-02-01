using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BottunSelecter2 : MonoBehaviour
{
    public Button[] buttons; // �I���\�ȃ{�^���̔z��
    public GameObject selectionFrame; // �g�I�u�W�F�N�g

    private int currentButtonIndex = 0; // ���݂̑I������Ă���{�^���̃C���f�b�N�X

    bool isInputActive = false; // ���͏�Ԃ�ێ�����ϐ�
    float verticalInput = 0f; // ���͂�ێ�����ϐ�

    bool isBlinking = false; // �_�Œ����ǂ����������t���O
    Coroutine blinkCoroutine; // �_�ŃR���[�`���̎Q��
    bool firstButtonPress = true; // �ŏ��̃{�^�������t���O
    bool isDecisionPressed = false; // ����{�^���������ꂽ���ǂ����������t���O

    [SerializeField] AudioSource source1;
    [SerializeField] AudioSource source2;
    [SerializeField] AudioClip clip1;
    [SerializeField] AudioClip clip2;

    IEnumerator Start()
    {
        // �ŏ��̃{�^����I��������Ԃɂ���
        SelectButton(currentButtonIndex);
        yield return null;
        StartBlinking();
    }

    void Update()
    {
        if (isDecisionPressed)
        {
            // ����{�^���������ꂽ��͏\���L�[�̓��͂𖳎�
            return;
        }
        //-------------------------------------------------------------------------------------------------
        //���L�[Ver

        //float verticalInput2 = Input.GetAxis("Horizontal");

        //if (verticalInput2 != 0 && !isInputActive)
        //{
        //    isInputActive = true; // ���͏�Ԃ��X�V
        //    if (verticalInput2 > 0)
        //    {
        //        SelectPreviousButton();
        //    }
        //    else if (verticalInput2 < 0)
        //    {
        //        SelectNextButton();
        //    }
        //}
        //else if (verticalInput2 == 0)
        //{
        //    isInputActive = false; // ���͏�Ԃ����Z�b�g
        //}

        //if (Input.GetKey(KeyCode.Space)) // Xbox�R���g���[���[��A�{�^���������ꂽ��{�^�������s����
        //{
        //    if (firstButtonPress)
        //    {
        //        firstButtonPress = false; // �ŏ��̃{�^�������t���O������
        //    }
        //    PressSelectedButton();
        //    if (!isBlinking) // �_�Œ��łȂ��A���ŏ��̃{�^����������Ă��Ȃ��ꍇ�_�ŊJ�n
        //    {
        //        StartBlinking();
        //    }
        //}
        //-------------------------------------------------------------------------------------------------
        //�R���g���[���[Ver

        verticalInput = Input.GetAxis("Vertical2");

        if (verticalInput > 0 && !isInputActive) // ���͂������Ă��O����͂��Ȃ������ꍇ
        {
            SelectNextButton();
            isInputActive = true; // ���͏�Ԃ��X�V
        }
        else if (verticalInput < 0 && !isInputActive)
        {
            SelectPreviousButton();
            isInputActive = true;
        }
        else if (verticalInput == 0) // ���͂��Ȃ��ꍇ
        {
            isInputActive = false; // ���͏�Ԃ����Z�b�g
        }
        if (Input.GetButtonDown("AButton")) // Xbox�R���g���[���[��A�{�^���������ꂽ��{�^�������s����
        {
            if (firstButtonPress)
            {
                firstButtonPress = false; // �ŏ��̃{�^�������t���O������
            }
            PressSelectedButton();
            if (!isBlinking) // �_�Œ��łȂ��A���ŏ��̃{�^����������Ă��Ȃ��ꍇ�_�ŊJ�n
            {
                StartBlinking();
            }
        }
    }

    void SelectButton(int index)
    {
        // �I�����ꂽ�{�^���̈ʒu�ɘg���ړ�����
        Vector3 selectedPosition = buttons[index].transform.position;
        selectionFrame.transform.position = selectedPosition;
    }

    void SelectNextButton()
    {
        source1.PlayOneShot(clip1);
        // ���̃{�^����I��
        currentButtonIndex++;
        if (currentButtonIndex >= buttons.Length)
        {
            currentButtonIndex = 0;
        }
        SelectButton(currentButtonIndex);
        StopBlinking(); // �_�ł��~
    }

    void SelectPreviousButton()
    {
        source1.PlayOneShot(clip1);
        // �O�̃{�^����I��
        currentButtonIndex--;
        if (currentButtonIndex < 0)
        {
            currentButtonIndex = buttons.Length - 1;
        }
        SelectButton(currentButtonIndex);
        StopBlinking(); // �_�ł��~
    }

    void PressSelectedButton()
    {
        source2.PlayOneShot(clip2);
        // �I������Ă���{�^�������s����
        buttons[currentButtonIndex].onClick.Invoke();
        isDecisionPressed = true; // ����{�^���������ꂽ���Ƃ������t���O��ݒ�

    }


    IEnumerator BlinkCoroutine()
    {
        isBlinking = true; // �_�Œ��t���O��ݒ�

        while (firstButtonPress) // �ŏ��̃{�^�����������܂œ_�ł��Ȃ�
        {
            yield return null;
        }

        while (true)
        {
            selectionFrame.GetComponent<Image>().color = Color.red; // �ԂɕύX
            yield return new WaitForSeconds(0.1f); // 0.5�b�҂�

            selectionFrame.GetComponent<Image>().color = Color.clear; // ���ɕύX
            yield return new WaitForSeconds(0.1f); // 0.5�b�҂�
        }
    }

    void StartBlinking()
    {
        blinkCoroutine = StartCoroutine(BlinkCoroutine());
    }

    void StopBlinking()
    {
        if (isBlinking && blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine); // �_�ł��~
            isBlinking = false; // �_�Œ��t���O������
            selectionFrame.GetComponent<Image>().color = Color.white; // ���ɖ߂�
        }
    }
}