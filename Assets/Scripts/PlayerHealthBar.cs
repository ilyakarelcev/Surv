using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{

    [SerializeField] private Image _scale;
    [SerializeField] PlayerHealth _playerHealth;

    private void Awake()
    {
        _playerHealth.OnChangeHealth += SetHealth;
    }

    public void SetHealth(float health, float maxHealth, bool isDamage) {
        _scale.fillAmount = health / maxHealth;
    }

}
