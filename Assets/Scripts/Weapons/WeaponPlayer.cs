using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class WeaponPlayer : GenericPool<PlayerBullet>
{
    [SerializeField] private PlayerBullet playerBullet;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private float _cooldown;

    private InputReader _inputReader;
    private float _lastShootTime;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _inputReader.Shoted += TryShoot;
    }

    private void OnDisable()
    {
        _inputReader.Shoted -= TryShoot;
    }

    public void Reset()
    {
        _lastShootTime = 0f;

        foreach (PlayerBullet bullet in AllObjects)
        {
            if (bullet.gameObject.activeSelf)
            {
                bullet.EnemyHited -= _scoreCounter.Add;
                bullet.Released -= Release;

                ReleaseObject(bullet);
            }
        }

        ReleaseAllObjects();
    }

    private void TryShoot()
    {
        if (Time.time < _lastShootTime + _cooldown)
            return;

        _lastShootTime = Time.time;
        Shoot();
    }

    private void Shoot()
    {
        PlayerBullet bullet = GetObject(playerBullet);

        bullet.Released += Release;
        bullet.EnemyHited += _scoreCounter.Add;

        bullet.gameObject.SetActive(true);

        bullet.transform.position = _shotPoint.position;
        bullet.SetDirection(transform.right); 
    }

    private void Release(PlayerBullet bullet)
    {
        ReleaseObject(bullet);

        bullet.EnemyHited -= _scoreCounter.Add;
        bullet.Released -= Release;
    }
}
