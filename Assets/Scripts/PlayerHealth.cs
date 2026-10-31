using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public event Action<float, float> OnHealthChanged;
    [SerializeField] private float _currentHealth = 5;
    [SerializeField] private float _maxHealth = 5;
    public float Health => _currentHealth;

    private void Start()
    {

    }

    public void ReceiveDamage(float damageAmount, Vector3 hitPosition)
    {
        
        _currentHealth -= damageAmount;

        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ReceiveHeal(float damageAmount, Vector3 hitPosition)
    {
        throw new System.NotImplementedException();
    }

}
