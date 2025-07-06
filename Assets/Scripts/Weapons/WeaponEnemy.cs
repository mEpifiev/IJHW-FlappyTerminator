using System.Collections;
using UnityEngine;

public class WeaponEnemy : GenericPool<EnemyBullet>
{
    [SerializeField] private EnemyBullet _enemyBullet;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _delay;

    private Coroutine _coroutine;

    public void Reset()
    {
        StopShoot();

        foreach (EnemyBullet bullet in AllObjects)
        {
            if(bullet.gameObject.activeSelf)
            {
                bullet.Released -= Release;

                ReleaseObject(bullet);
            }
        }

        ReleaseAllObjects();
    }

    public void StartShoot()
    {
        if (_coroutine != null)
            return;

        _coroutine = StartCoroutine(Shoot());
    }

    public void StopShoot()
    {
        if (_coroutine == null)
            return;

        StopCoroutine(_coroutine);
        _coroutine = null;

    }

    private IEnumerator Shoot()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            SpawnBullet();

            yield return wait;
        }
    }

    private void SpawnBullet()
    {
        EnemyBullet enemyBullet = GetObject(_enemyBullet);

        enemyBullet.Released += Release;

        enemyBullet.gameObject.SetActive(true);

        enemyBullet.transform.position = _shootPoint.position;
        enemyBullet.SetDirection(transform.right);
    }

    private void Release(EnemyBullet enemyBullet)
    {
        ReleaseObject(enemyBullet);

        enemyBullet.Released -= Release;       
    }
}
