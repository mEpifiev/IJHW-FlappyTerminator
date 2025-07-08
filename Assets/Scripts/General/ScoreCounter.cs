using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;

    private int _score;

    public event Action<int> Changed;

    public int Score => _score;

    private void OnEnable()
    {
        _enemySpawner.Released += Add;
    }

    private void OnDisable()
    {
        _enemySpawner.Released -= Add;
    }

    public void Add()
    {
        _score++;

        Changed?.Invoke(_score);
    }

    public void Reset()
    {
        _score = 0;

        Changed?.Invoke(_score);
    }
}
