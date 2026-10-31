using NUnit.Framework;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    private EnemyHealth health;
    private Queue<GameObject> _army = new Queue<GameObject>();
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private ScoreUI _score;
    private Coroutine _spawnCoroutine;
    private Coroutine _increaseCoroutine;
    private float _timeToSpawn = 10f;
    private float _timeToIncrease = 5f;
    private WaitForSeconds _timerIncrease;

    private void Awake()
    {
        _timerIncrease = new WaitForSeconds(_timeToIncrease);
        InitializePool(20);


    }
    private void Start()
    {
        _spawnCoroutine = StartCoroutine(spawnCoroutine());
        _increaseCoroutine = StartCoroutine(increaseCoroutine());
    }

    private IEnumerator spawnCoroutine()
    {
        while (true)
        {
            

            int spawnPoint = Random.Range(0, 4);

            Vector3 spawnPosition = _spawnPoints[spawnPoint].position;
            GetFromPool(spawnPosition);


            yield return new WaitForSeconds(_timeToSpawn);
        }
    }
    private IEnumerator increaseCoroutine()
    {
        while (_timeToSpawn > 1)
        {
            
            _timeToSpawn -= 1;
            
            yield return _timerIncrease;
        }
        
        
        
    }
    private void InitializePool(int n)
    {
        for(int i = 0; i < n; i ++)
        {
            GameObject obj = Instantiate(_enemyPrefab);
            obj.SetActive(false);
            obj.GetComponent<EnemyHealth>().Death += ReturnToPool;
            _army.Enqueue(obj);
        }
    }
    private void ReturnToPool(GameObject gameObject) 
    {
        gameObject.SetActive(false);
        _army.Enqueue(gameObject);

        if (_score != null)
        {
            _score.AddScore(1);
        }
    }
    private void GetFromPool(Vector3 spawnPosition)
    {
        if (_army.Count != 0)
        {
            GameObject obj = _army.Dequeue();
            obj.SetActive(true);
            obj.GetComponent<EnemyHealth>()._health = obj.GetComponent<EnemyHealth>().maxHP;
            obj.transform.position = spawnPosition;
        }
        
    }
}
