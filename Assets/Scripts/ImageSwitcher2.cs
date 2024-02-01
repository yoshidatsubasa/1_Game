using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImageSwitcher2 : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("RBButton"))
        {
            SceneManager.LoadScene("PlayMove2");
        }

        if (Input.GetButtonDown("LBButton"))
        {

            SceneManager.LoadScene("Option");
        }
    }
}
