using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{

    public float power = 0;
    new public Rigidbody rigidbody;

    private bool jampFlag = false;

    public float jumppower = 5;

    private CharacterController controller;
    private Vector3 moveDirection;

    public Slider slider;
    public Slider slider2;

    private bool isDelayedDecreasing = false; // �x�����������ǂ����������t���O

    int coinCount;

    [SerializeField]
    GameObject boostUI;


    void Start()
    {
        // CharacterController���擾
        controller = GetComponent<CharacterController>();

        Screen.SetResolution(1920, 1080, false);
        Application.targetFrameRate = 60;

        slider.value = 5;
        slider2.value = 5;


        //�}�E�X�ŃN���b�N�ł��Ȃ��悤�ɂ���
        slider.interactable = false;
        slider2.interactable = false;
    }

   
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody.AddForce(new Vector3(-1, 0, 0) * power);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody.AddForce(new Vector3(1, 0, 0) * power);
        }

        if (Input.GetKey(KeyCode.UpArrow)&& ! jampFlag)
        {
            rigidbody.velocity = Vector3.up * jumppower;
            jampFlag = true;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            rigidbody.AddForce(new Vector3(-1, 0, 0) * power);
        }

        coinCount = Coincounter.getscore();

        if(coinCount>=10)
        {
            StartCoroutine("SpeedUp");
            boostUI.SetActive(true);
        }
        if(slider.value<=0)
        {
            SceneManager.LoadScene("Result");
        }

       

    }

    public IEnumerator SpeedUp()
    {

        power = 10;
        yield return new WaitForSeconds(3.0f);
        power = 5;
        boostUI.SetActive(false);
    }

   

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Stage Load 1"))
        {
            jampFlag = false;
        }

        if (collision.gameObject.CompareTag("Stage Load 2"))
        {
            jampFlag = false;
        }

        if (collision.gameObject.CompareTag("Stage Load 3"))
        {
            jampFlag = false;
        }

        if (collision.gameObject.CompareTag("Cube"))
        {
            jampFlag = false;
        }

        if (collision.gameObject.CompareTag("Cube 1"))
        {
            jampFlag = false;
        }

        if (collision.gameObject.CompareTag("Cube 2"))
        {
            jampFlag = false;
        }

        if (collision.gameObject.CompareTag("Cube 3"))
        {
            jampFlag = false;
        }
        if (collision.gameObject.name == "Red" && !isDelayedDecreasing)
        {
            int damage = 1;                           //���̃_���[�W
            slider.value-=damage;�@�@�@�@�@�@�@�@�@�@ //�����Ɍ���
            StartCoroutine(DelayedSliderDecrease(damage));  //�x��Č��� 

            PlayerEffect flickerScript = GetComponent<PlayerEffect>();
            if (flickerScript != null)
            {
                flickerScript.StartFlicker();
            }
        }
        

    }

    private System.Collections.IEnumerator DelayedSliderDecrease(int damage)
    {
        float decreaseDuration = 1.0f; // �������ԁi�b�j
        float startTime = Time.time;
        float startValue = slider2.value;

        isDelayedDecreasing = true; // �x���������ɐݒ�

        while (Time.time - startTime < decreaseDuration && damage > 0)
        {
            float currentTime = Time.time - startTime;
            float decreaseAmount = Mathf.Lerp(startValue, startValue - damage, currentTime / decreaseDuration);
            slider2.value = Mathf.Max(0, decreaseAmount); // �X���C�_�[�̒l��0�����ɂȂ�Ȃ��悤�ɒ���
            yield return null;
        }

        slider2.value = slider.value; // slider2�̒l�𑦍��Ɍ��������X���C�_�[�̒l�ɍ��킹��
        isDelayedDecreasing = false; // �x�������I��
    }


}
