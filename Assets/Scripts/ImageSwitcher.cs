using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImageSwitcher : MonoBehaviour
{
    void Update()
    {
       
        if (Input.GetButtonDown("LBButton"))
        {
            // LBButtonが押されたらPlayMoveシーンに移動する
            SceneManager.LoadScene("PlayMove");
        }
    }
}
