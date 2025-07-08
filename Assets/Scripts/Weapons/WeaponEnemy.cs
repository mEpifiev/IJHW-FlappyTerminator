using System.Collections;
using UnityEngine;

public class WeaponEnemy : Weapon
{
    private Coroutine _coroutine;

    public override void Reset()
    {
        StopShoot();

        base.Reset();
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
        WaitForSeconds wait = new WaitForSeconds(Cooldown);

        while (enabled)
        {
            SpawnBullet();

            yield return wait;
        }
    }
}
