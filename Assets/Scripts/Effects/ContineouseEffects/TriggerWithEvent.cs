using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWithEvent : MonoBehaviour
{

    public Action<Enemy> OnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        //if(other.GetComponent<Enemy>() is Enemy enemy)
        OnTrigger.Invoke(other.GetComponent<Enemy>());
    }

}
