using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected BulletPool BulletPool;
    [SerializeField] protected Bullet BulletPrefab;
    [SerializeField] protected Transform ShotPoint;
    [SerializeField] protected float Cooldown;

    public virtual void Reset()
    {
        foreach (Bullet bullet in BulletPool.AllObjects)
        {
            if (bullet.gameObject.activeSelf)
            {
                bullet.Released -= Release;
                BulletPool.ReleaseObject(bullet);
            }
        }

        BulletPool.Reset();
    }

    protected void SpawnBullet()
    {
        Bullet bullet = BulletPool.GetObject(BulletPrefab);

        bullet.Released += Release;

        bullet.gameObject.SetActive(true);

        bullet.transform.position = ShotPoint.position;
        bullet.SetDirection(transform.right);

    }

    protected void Release(Bullet bullet)
    {
        BulletPool.ReleaseObject(bullet);
        bullet.Released -= Release;
    }
}
