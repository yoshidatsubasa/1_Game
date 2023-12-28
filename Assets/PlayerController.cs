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

    private bool isDelayedDecreasing = false; // 遅延減少中かどうかを示すフラグ

    int coinCount;

    [SerializeField]
    GameObject boostUI;


    void Start()
    {
        // CharacterControllerを取得
        controller = GetComponent<CharacterController>();

        Screen.SetResolution(1920, 1080, false);
        Application.targetFrameRate = 60;

        slider.value = 5;
        slider2.value = 5;


        //マウスでクリックできないようにする
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
            int damage = 1;                           //一回のダメージ
            slider.value-=damage;　　　　　　　　　　 //即座に減少
            StartCoroutine(DelayedSliderDecrease(damage));  //遅れて減少 

            PlayerEffect flickerScript = GetComponent<PlayerEffect>();
            if (flickerScript != null)
            {
                flickerScript.StartFlicker();
            }
        }
        

    }

    private System.Collections.IEnumerator DelayedSliderDecrease(int damage)
    {
        float decreaseDuration = 1.0f; // 減少時間（秒）
        float startTime = Time.time;
        float startValue = slider2.value;

        isDelayedDecreasing = true; // 遅延減少中に設定

        while (Time.time - startTime < decreaseDuration && damage > 0)
        {
            float currentTime = Time.time - startTime;
            float decreaseAmount = Mathf.Lerp(startValue, startValue - damage, currentTime / decreaseDuration);
            slider2.value = Mathf.Max(0, decreaseAmount); // スライダーの値を0未満にならないように調整
            yield return null;
        }

        slider2.value = slider.value; // slider2の値を即座に減少したスライダーの値に合わせる
        isDelayedDecreasing = false; // 遅延減少終了
    }


}
