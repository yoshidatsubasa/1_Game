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

    int coinCount;

    [SerializeField]
    GameObject boostUI;

    void Start()
    {
        // CharacterController‚ðŽæ“¾
        controller = GetComponent<CharacterController>();

        Screen.SetResolution(1920, 1080, false);
        Application.targetFrameRate = 60;

        slider.value = 5;

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
        power = 15;
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
        if (collision.gameObject.name == "Red")
        {
            slider.value--;
        }
        

    }


   

}
