using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private Coroutine _coroutineAttack;
    [SerializeField] private float _attackCooldown = 1.0f;
    [SerializeField] private float _FramesOfAttack = 0.5f;
    [SerializeField] private float _attackDamage = 1f;
    private WaitForSeconds _cooldownTimer;
    private WaitForSeconds _inTheFramesOfAttackTimer;
    private Animator _animator;
    private void Awake()
    {
        _cooldownTimer = new WaitForSeconds(_attackCooldown);
        _inTheFramesOfAttackTimer = new WaitForSeconds(_FramesOfAttack);
        _animator = GetComponent<Animator>();
    }
    public void Attack(Transform target)
    {
        if (_coroutineAttack == null)
        {
            _coroutineAttack = StartCoroutine(AttackCoroutine(target));
        }
    }

    private IEnumerator AttackCoroutine(Transform target)
    {
        
        _animator.SetTrigger("Attack");
        yield return _inTheFramesOfAttackTimer;
        target.GetComponent<IDamageable>()?.ReceiveDamage(_attackDamage, target.position);
        yield return _cooldownTimer;
        _coroutineAttack = null;
    }
}
