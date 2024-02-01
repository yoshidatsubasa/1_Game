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
            // LBButton‚ª‰Ÿ‚³‚ê‚½‚çPlayMoveƒV[ƒ“‚ÉˆÚ“®‚·‚é
            SceneManager.LoadScene("PlayMove");
        }
    }
}
