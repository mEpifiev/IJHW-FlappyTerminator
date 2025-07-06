using System;
using UnityEngine;

public class EnemyBullet : Bullet
{
    public event Action<EnemyBullet> Released;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            player.Die();

        Released?.Invoke(this);
    }
}
