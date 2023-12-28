using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float textMove;

    void Start()
    {
        textMove = this.transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, textMove + Mathf.PingPong(Time.time / 3, 0.3f), transform.position.z);
    }
}
