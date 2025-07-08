using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _delay;
    [SerializeField] private float _lowerLimitY;
    [SerializeField] private float _upperLimitY;

    private Coroutine _coroutine;

    public event Action Released;

    public void Reset()
    {
        StopSpawn();

        foreach (Enemy enemy in _enemyPool.AllObjects)
        {
            enemy.Release();
            enemy.Released -= Release;
        }

        _enemyPool.Reset();

        _coroutine = StartCoroutine(Spawn());
    }

    private void StopSpawn()
    {
        if (_coroutine == null)
            return;

        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            Create();

            yield return wait;
        }
    }

    private void Create()
    {
        float spawnPositionY = Random.Range(_lowerLimitY, _upperLimitY);
        Vector3 spawnPoint = new Vector3(transform.position.x, spawnPositionY, transform.position.z);

        Enemy enemy = _enemyPool.GetObject(_enemy);

        enemy.Released += Release;

        enemy.gameObject.SetActive(true);
        enemy.transform.position = spawnPoint;

        enemy.WeaponEnemy.StartShoot();
        enemy.BirdAnimator.PlayFlyAnimation();
    }

    private void Release(Enemy enemy)
    {
        enemy.WeaponEnemy.StopShoot();
        enemy.BirdAnimator.StopFlyAnimation();

        _enemyPool.ReleaseObject(enemy);

        enemy.Released -= Release;

        Released?.Invoke();
    }
}
