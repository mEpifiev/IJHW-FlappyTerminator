using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class WeaponPlayer : Weapon
{
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

    public override void Reset()
    {
        _lastShootTime = 0f;

        base.Reset();
    }

    private void TryShoot()
    {
        if (Time.time < _lastShootTime + Cooldown)
            return;

        _lastShootTime = Time.time;

        SpawnBullet();
    }
}
