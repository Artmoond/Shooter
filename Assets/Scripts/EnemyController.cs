using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _playerTransform;
    private EnemyCombat _combat;

    [Header("Переменные пути")]
    [SerializeField] private float _pathUpdateInterval = 0.2f;
    [Header("Переменные боя")]
    [SerializeField] private float _attackRange = 2.0f;
    

    private float _lastAttackTime;
    private Coroutine _coroutinePathUpdate;
    

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _combat = GetComponent<EnemyCombat>();
    }

    private void Start()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _playerTransform = player.transform;
            _coroutinePathUpdate = StartCoroutine(pathUpdateCoroutine());
        }
        else
        {
            Debug.LogError("Нет объекта с тегом Player");
        }
    }


    private IEnumerator pathUpdateCoroutine()
    {
        WaitForSeconds waitInterval = new WaitForSeconds(_pathUpdateInterval);
        while (true)
        {
            if (_playerTransform != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);

                if (distanceToPlayer > _attackRange)
                {
                    _agent.isStopped = false;
                    _agent.SetDestination(_playerTransform.position);
                }
                else
                {
                    _agent.isStopped = true;
                    _agent.velocity = Vector3.zero;
                    
                    _combat.Attack(_playerTransform);
                }
            }
            yield return waitInterval;
        }
    }
}