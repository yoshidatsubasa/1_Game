using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject exitObject;
    private void OnTriggerEnter(Collider other)
    {
        if(exitObject !=null)
        {
            other.transform.position = exitObject.transform.position;
        }
    }

    void Start()
    {

    }
    void Update()
    {
    }
}
