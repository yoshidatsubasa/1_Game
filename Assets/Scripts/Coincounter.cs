using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Coincounter : MonoBehaviour
{
    public Coincounter playerController;

    public Text textComponent;

    public static int coinCount;

    float lifeTime = 0.5f;  // �l����̐������� 

    float speed = 100f;

    bool isGet;             // �l���ς݃t���O

   
    public AudioSource coinSound;  // �R�C�����擾���ꂽ���ɍĐ����鉹��


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player" && !isGet)
        {
            playerController.AddCoinCount();
            //Destroy(gameObject);
            

            isGet = true;
            transform.position += Vector3.up * 0.5f;

            //�T�E���h���Đ�
            if (coinSound != null)
            {
                coinSound.Play();
            }
        }
    }

    public void AddCoinCount()
    {
        coinCount = coinCount + 1;
        Debug.Log("CoinCount;" + coinCount);

        textComponent.text = "x" + coinCount;
    }

    public static int getscore()
    {
        return coinCount;
    }
   

    private void Start()
    {
        GameObject managerObject = GameObject.Find("Coin");

        playerController = managerObject.GetComponent<Coincounter>();

        // AudioSource�R���|�[�l���g���擾����
       coinSound = GetComponent<AudioSource>();

    }

    private void Update()
    {
        // �l����
        if (isGet)
        {

            // �f������]
            transform.Rotate(Vector3.up * speed * 10f * Time.deltaTime, Space.World);

            // �������Ԃ����炷
            lifeTime -= Time.deltaTime;

            // �������Ԃ�0�ȉ��ɂȂ��������
            if (lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }
        // �l���O
        else
        {
            // ��������]
            transform.Rotate(Vector3.up * speed * Time.deltaTime, Space.World);
        }

    }

   
}