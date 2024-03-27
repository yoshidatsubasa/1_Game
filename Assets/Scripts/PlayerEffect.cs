using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerEffect : MonoBehaviour
{
    private Renderer[] renderers;
    private Color[] originalColors;
    private bool isFlickering = false;

    public Color flickerColor = Color.red; // �_�Ŏ��̐F

    private void Start()
    {
        renderers = GetComponentsInChildren<Renderer>(); // ���f���̂��ׂĂ�Renderer�R���|�[�l���g���擾
        originalColors = new Color[renderers.Length]; // �eRenderer�̏����F��ۑ�
        for (int i = 0; i < renderers.Length; i++)
        {
            originalColors[i] = renderers[i].material.color;
        }
    }

    public void StartFlicker()
    {
        if (!isFlickering)
        {
            isFlickering = true;
            StartCoroutine(FlickerPlayer());
        }
    }

    private IEnumerator FlickerPlayer()
    {
        int flickerCount = 10; // �_�ł����
        float flickerDuration = 0.05f; // �_�ł̊Ԋu

        for (int i = 0; i < flickerCount * 2; i++)
        {
            // ���ׂĂ�Renderer�̐F��Ԃ�����
            foreach (Renderer renderer in renderers)
            {
                renderer.material.color = flickerColor;
            }
            yield return new WaitForSeconds(flickerDuration);

            // ���̐F�ɖ߂�
            for (int j = 0; j < renderers.Length; j++)
            {
                renderers[j].material.color = originalColors[j];
            }
            yield return new WaitForSeconds(flickerDuration);
        }

        // �ŏI�I�Ɍ��̐F�ɖ߂�
        for (int j = 0; j < renderers.Length; j++)
        {
            renderers[j].material.color = originalColors[j];
        }
        isFlickering = false;
    }
}