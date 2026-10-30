
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health;
    public float Health => _health;
    private Animator _animator;
    [SerializeField] private AnimationClip _hitClip;
    
    private NavMeshAgent _agent;
    private Coroutine _stunCoroutine;
    [SerializeField] private float _stunDuration = 0.3f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    public void ReceiveDamage(float damageAmount, Vector3 hitPosition)
    {
        
        _health -= damageAmount;

        
        
            
        if (_stunCoroutine != null)
            StopCoroutine(_stunCoroutine);

        _stunCoroutine = StartCoroutine(StunCoroutine(_hitClip));
        


        
        if (_health <= 0)
        {
            Destroy(gameObject);
        }


    }
    private IEnumerator StunCoroutine(AnimationClip clip)
    {
        _agent.isStopped = true;
        _agent.velocity = Vector3.zero;

        if (clip != null && _stunDuration > 0)
        {
            float defaulClipLength = clip.length;
            float speedMultiplier = defaulClipLength / _stunDuration;
            _animator.SetFloat("HitSpeedMultiplier", speedMultiplier);
        }
        

        _animator.SetTrigger("Hit");

        yield return new WaitForSeconds(_stunDuration);

        if (_agent != null)
        {
            _agent.isStopped = false;
        }
        
    }
    public void ReceiveHeal(float damageAmount, Vector3 hitPosition)
    {
        throw new System.NotImplementedException();
    }

    
}
