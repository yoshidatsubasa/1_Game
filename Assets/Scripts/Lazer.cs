using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Lazer : MonoBehaviour
{
    public GameObject player; // �v���C���[�̎Q��
    public ParticleSystem particleEffect; // �p�[�e�B�N���G�t�F�N�g�̎Q��

    //private bool hasPlayed = false; // �G�t�F�N�g���Đ����ꂽ���ǂ����������t��

    public int damageAmount = 1; // �p�[�e�B�N���G�t�F�N�g���^����_���[�W��

    private void Start()
    {
        // �v���C���[�̎Q�Ƃ��擾
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // �p�[�e�B�N���G�t�F�N�g�̏�����Ԃ͔�A�N�e�B�u�ɂ���
        if (particleEffect != null)
        {
            particleEffect.Stop();
        }
    }
   
    private void Update()
    {
        if (player.GetComponent<LazerErea>().invaded == true)
        {
            if (particleEffect != null)
            {
                particleEffect.Play();
            }
        }
      }
    private void OnParticleCollision(GameObject other)
    {
        // �Փ˂����I�u�W�F�N�g���v���C���[�ł��邩�ǂ������m�F����
        if (other.CompareTag("Player"))
        {
            // �v���C���[�Ƀ_���[�W��^����
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                // �v���C���[���p�[�e�B�N���G�t�F�N�g�͈͓̔��ɂ���ꍇ�̂݃_���[�W��^����
                if (particleEffect != null && particleEffect.IsAlive(true))
                {
                    playerController.TakeDamage(damageAmount);
                }
            }
        }
    }

}