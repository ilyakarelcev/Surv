using UnityEngine;

public class PlayerHitParticles : MonoBehaviour
{

    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private ParticleSystem _particleSystem;

    //private float _healthBefore = Mathf.Infinity;

    private void Start()
    {
        _playerHealth.OnChangeHealth += CreateBlood;
    }

    private void CreateBlood(float currentHealth, float maxHealth, bool isDamage)
    {
        if (isDamage)
        {
            transform.localEulerAngles = new Vector3(0, Random.Range(0, 360f), 0);
            _particleSystem.Play();
        }
    }

}
