using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackLoop : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Material _material;
    void Start()
    {
        _material = GetComponent<Renderer>().material;
    }
    void Update()
    {
        Vector2 offset = new Vector2(Time.time * _speed, 0);
        _material.mainTextureOffset = offset;
    }
}
