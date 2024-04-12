using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerErea : MonoBehaviour
{
    public bool invaded;
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Lazercollider")
        {
            invaded = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Lazercollider")
        {
            invaded = false;
        }
    }
}
