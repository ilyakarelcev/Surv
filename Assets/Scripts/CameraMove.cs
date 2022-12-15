using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraMove : MonoBehaviour
{

    [SerializeField] private Transform _target;
    void Update()
    {
        transform.position = _target.position;
    }

}
