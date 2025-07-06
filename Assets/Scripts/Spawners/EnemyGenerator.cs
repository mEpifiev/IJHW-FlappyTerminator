using System.Collections;
using UnityEngine;

public class EnemyGenerator : GenericPool<Enemy>
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _delay;
    [SerializeField] private float _lowerLimitY;
    [SerializeField] private float _upperLimitY;

    private Coroutine _coroutine;

    public void Reset()
    {
        StopSpawn();

        foreach (Enemy enemy in AllObjects)
        {
            enemy.Release();
            enemy.Released -= Release;
        }

        ReleaseAllObjects();

        _coroutine = StartCoroutine(Generate());
    }

    private void StopSpawn()
    {
        if (_coroutine == null)
            return;

        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    private IEnumerator Generate()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            Spawn();
            yield return wait;
        }
    }

    private void Spawn()
    {
        float spawnPositionY = Random.Range(_lowerLimitY, _upperLimitY);
        Vector3 spawnPoint = new Vector3(transform.position.x, spawnPositionY, transform.position.z);

        Enemy enemy = GetObject(_enemy);

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

        ReleaseObject(enemy);

        enemy.Released -= Release;
    }
}
