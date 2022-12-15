using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAddCoins : MonoBehaviour
{

    [SerializeField] private Collector _coinCollector;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) {
            _coinCollector.CollectExperienceLoot();
        }
    }

}
