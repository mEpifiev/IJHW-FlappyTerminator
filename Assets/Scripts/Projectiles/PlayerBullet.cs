using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBullet : Bullet
{
    public event Action<PlayerBullet> Released;
    public event Action EnemyHited;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.Release();
            EnemyHited?.Invoke();
        }

        Released?.Invoke(this);
    }
}
