using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fps : MonoBehaviour
{

    [SerializeField] private int _targetFrameRate;

    void Start()
    {
        Application.targetFrameRate = _targetFrameRate;
    }

}
