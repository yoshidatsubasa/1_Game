//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Bullet : MonoBehaviour
//{
//    public GameObject bulletPrefab; // �e�̃v���n�u
//    public float bulletSpeed = 10f; // �e�̑��x
//    public float triggerDistance = 10f; // �v���C���[�����m���鋗��

//    private bool playerInRange = false; // �v���C���[���͈͓��ɂ��邩�ǂ����̃t���O

//    private void Update()
//    {
//        // �v���C���[��Transform���擾
//        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
//        if (playerObject != null)
//        {
//            Transform playerTransform = playerObject.transform;
//            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

//            // �v���C���[���w�肵�������ȓ��ɂ���ꍇ
//            if (distanceToPlayer <= triggerDistance)
//            {
//                if (!playerInRange)
//                {
//                    // �͈͓��Ƀv���C���[���������̂Œe�𔭎˂���
//                    ShootBullet();
//                    playerInRange = true; // �e�𔭎˂������Ƃ��L�^����
//                }
//            }
//            else
//            {
//                playerInRange = false; // �v���C���[���͈͊O�ɏo����t���O�����Z�b�g����
//            }
//        }
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            PlayerController playerController = other.GetComponent<PlayerController>();
//            if (playerController != null)
//            {
//                playerController
//            }

          
//        }
//    }

//    private void ShootBullet()
//    {
//        // �e�̔��ˏ���
//        GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
//        Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();

//        // �����ɒe�𔭎˂���x�N�g����ݒ�
//        Vector3 direction = Vector3.left;

//        // �e�ɑ��x��^����i�������ɔ�΂��j
//        bulletRigidbody.velocity = direction * bulletSpeed;

//        // ��莞�Ԍ�ɒe��j������
//        Destroy(newBullet, 5f);
//    }
//}