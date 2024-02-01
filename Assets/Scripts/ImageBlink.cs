using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageBlink : MonoBehaviour
{
    public Image imageToBlink; // �_�ł��������C���[�W
    public float blinkInterval = 0.5f; // �_�ł̊Ԋu�i�b�j

    private void Start()
    {
        // �R���[�`�����J�n����
        StartCoroutine(BlinkImage());
    }
    void Update()
    {
    }
        IEnumerator BlinkImage()
    {
        while (true)
        {
            // �C���[�W�̓����x��؂�ւ���i0����1�܂Łj
            imageToBlink.CrossFadeAlpha(0f, blinkInterval, false);
            yield return new WaitForSeconds(blinkInterval);
            imageToBlink.CrossFadeAlpha(1f, blinkInterval, false);
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
