using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    private GameObject obj;
    public Warp transObj;
    private Vector3 transVec;

    //�ړ���Ԃ�\���t���O
    public bool moveStatus;

    public AudioSource warpSound; // ���[�v���̌��ʉ����Đ����邽�߂� AudioSource

    void Start()
    {
        // ���[�v���̌��ʉ����A�^�b�`���ꂽ�I�u�W�F�N�g���� AudioSource ���擾
        warpSound = GetComponent<AudioSource>();
        //�ړ���(�~��B)�̍��W���擾����
        transVec = transObj.transform.position;
        //�����ł͈ړ��\�Ȃ���True
        moveStatus = true;

    }


    //���̂Əd�Ȃ�u�ԌĂ΂��
    void OnTriggerEnter(Collider other)
    {
        obj = GameObject.Find(other.name);
        //�������ړ��\�ȂƂ��ړ�����B
        if (moveStatus)
        {
            //�ړ���͒���ړ��ł��Ȃ��悤�ɂ���
            transObj.moveStatus = false;
            obj.transform.position = transVec;

            // ���[�v���̌��ʉ����Đ�����
            if (warpSound != null)
            {
                warpSound.Play();
            }
        }
    }
    //���̂Ɨ��ꂽ����Ă΂��
    void OnTriggerExit(Collider other)
    {
        //�ړ��\�ɂ���B
        moveStatus = true;
    }

    private void Update()
    {

    }
}