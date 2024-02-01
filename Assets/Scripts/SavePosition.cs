using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePosition : MonoBehaviour
{
    // ���ԃt���b�O�̈ʒu
    public Transform checkpoint;

    // �v���C���[�̏����ʒu
    private Vector3 initialPosition;

    // �V�[�����J�ڂ��Ă��I�u�W�F�N�g���j������Ȃ��悤�ɂ��邽�߂̃t���O
    private static bool created = false;

    void Start()
    {
        // �V�[�����J�ڂ��Ă��I�u�W�F�N�g���j������Ȃ��悤�ɂ���
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }

        // �v���C���[�̏����ʒu��ۑ�
        initialPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        // ���ԃt���b�O�ɐG�ꂽ�ꍇ
        if (other.CompareTag("Checkpoint"))
        {
            Debug.Log("Player touched the checkpoint!");
            // �v���C���[�̌��݂̈ʒu����y���W-20�𒴂����ꍇ
            if (transform.position.y < checkpoint.position.y - 20f)
            {
                // �v���C���[�𒆊ԃt���b�O�̈ʒu�ɖ߂�
                ResetToCheckpoint();
            }

            // ���ԃt���b�O���A�N�e�B�u�ɂ���
            other.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // �v���C���[�����̍�����艺�ɗ�������
        if (transform.position.y < -20f)
        {
            // �v���C���[�𒆊ԃt���b�O�̈ʒu�ɖ߂�
            ResetToCheckpoint();
        }
    }

    // �v���C���[�𒆊ԃt���b�O�̈ʒu�ɖ߂����\�b�h
    private void ResetToCheckpoint()
    {
        transform.position = new Vector3(checkpoint.position.x, checkpoint.position.y, checkpoint.position.z);
    }

    // �V�[���J�ڑO�ɒ��ԃt���b�O�̈ʒu��ۑ�
    public static void SaveCheckpointPosition(Vector3 position)
    {
        checkpointPosition = position;
    }

    // ���ԃt���b�O�̈ʒu���擾���邽�߂̃v���p�e�B
    public static Vector3 CheckpointPosition
    {
        get { return checkpointPosition; }
    }

    // ���ԃt���b�O�̈ʒu
    private static Vector3 checkpointPosition;
}