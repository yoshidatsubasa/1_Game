using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banper : MonoBehaviour
{
    public float power = 10;
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody r = other.GetComponent<Rigidbody>();
        if(r !=null)
        {
            Vector3 angle = (other.transform.position - transform.position).normalized;
            r.velocity = angle * power;
        }
    }

    void Start()
    {

    }
    void Update()
    {
    }
}
