using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDrop : MonoBehaviour
{
    public GameObject target;

    void Update()
    {
        Vector3 player = target.transform.position;
        float dis = Vector3.Distance(player, this.transform.position);

        if (dis < 9f)
        {
            SphereGravity();
        }
    }

    void SphereGravity()
    {
        GetComponent<Rigidbody>().useGravity = true;
    }
}
