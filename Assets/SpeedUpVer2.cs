using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpVer2 : MonoBehaviour
{
    public float power = 0;
    new public Rigidbody rigidbody;

    private CharacterController controller;
    private Vector3 moveDirection;

    int coinCount;

    

    [SerializeField]
    GameObject boostUI;

    void Start()
    {
        // CharacterController‚ðŽæ“¾
        controller = GetComponent<CharacterController>();


        Screen.SetResolution(1920, 1080, false);
        Application.targetFrameRate = 60;

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


        if (Input.GetKey(KeyCode.DownArrow))
        {
            rigidbody.AddForce(new Vector3(-1, 0, 0) * power);
        }

        coinCount = Coincounter.getscore();

        if (coinCount >= 20)
        {
            StartCoroutine("SpeedUp2");
            boostUI.SetActive(true);
        }


    }
    public IEnumerator SpeedUp2()
    {
        power = 15;
        yield return new WaitForSeconds(3.0f);
        power = 0;
        boostUI.SetActive(false);
    }

}
