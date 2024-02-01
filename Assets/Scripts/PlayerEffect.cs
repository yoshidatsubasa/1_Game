using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    private Renderer playerRenderer;
    private Color originalColor;
    private bool isFlickering = false;

    public Color flickerColor = Color.red; // �_�Ŏ��̐F

    private void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        originalColor = playerRenderer.material.color;
    }

    public void StartFlicker()
    {
        if (!isFlickering)
        {
            isFlickering = true;
            StartCoroutine(FlickerPlayer());
        }
    }

    private System.Collections.IEnumerator FlickerPlayer()
    {
        int flickerCount = 10; // �_�ł����
        float flickerDuration = 0.05f; // �_�ł̊Ԋu

        for (int i = 0; i < flickerCount * 2; i++)
        {
            playerRenderer.material.color = flickerColor; // �Ԃ�����
            yield return new WaitForSeconds(flickerDuration);
            playerRenderer.material.color = originalColor; // ���̐F�ɖ߂�
            yield return new WaitForSeconds(flickerDuration);
        }

        playerRenderer.material.color = originalColor; // �ŏI�I�Ɍ��̐F�ɖ߂�
        isFlickering = false;
    }
}