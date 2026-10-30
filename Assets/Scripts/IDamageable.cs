using UnityEngine;

public interface IDamageable
{
    float Health { get; }
    void ReceiveDamage(float damageAmount, Vector3 hitPosition);

    void ReceiveHeal(float damageAmount, Vector3 hitPosition);
}

