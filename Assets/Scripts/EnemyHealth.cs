using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health;
    public float Health => _health;

    private void Start()
    {

    }

    public void ReceiveDamage(float damageAmount, Vector3 hitPosition)
    {
        Debug.Log($"ReceiveDamage.Current_HP: {_health}");
        _health -= damageAmount;
        Debug.Log($"ReceiveDamage.After taking Damage: {_health}");
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ReceiveHeal(float damageAmount, Vector3 hitPosition)
    {
        throw new System.NotImplementedException();
    }
}
