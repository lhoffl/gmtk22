using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 0.05f;
    [SerializeField] bool _shouldRotate;

    // Update is called once per frame
    void Update()
    {
        if (_shouldRotate) transform.Rotate(0f, 0f,  Time.deltaTime * _rotationSpeed);
    }
}
