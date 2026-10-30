using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _playerTransform;

    [Header("Настройки")]
    [SerializeField] private float _pathUpdateInterval = 0.2f; // Интервал пересчета пути (для оптимизации)
    private float _timer;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        // Ищем игрока по тегу "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Игрок с тегом 'Player' не найден на сцене! Убедитесь, что повесили тег на объект игрока.");
        }
    }

    private void Update()
    {
        if (_playerTransform == null) return;

        // Пересчитываем путь к игроку по таймеру, чтобы не грузить процессор каждый кадр
        _timer += Time.deltaTime;
        if (_timer >= _pathUpdateInterval)
        {
            _agent.SetDestination(_playerTransform.position);
            _timer = 0f;
        }
    }
}