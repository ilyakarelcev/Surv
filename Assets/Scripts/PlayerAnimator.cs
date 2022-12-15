using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _playerHealth.OnDie += Die;
    }

    void Die() {
        _animator.SetTrigger("Die");
    }

}
