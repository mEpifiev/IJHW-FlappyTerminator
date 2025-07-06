using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(WeaponEnemy))]
[RequireComponent(typeof(BirdAnimator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody2D;
    private WeaponEnemy _weaponEnemy;
    private BirdAnimator _birdAnimator;

    public event Action<Enemy> Released;

    public WeaponEnemy WeaponEnemy => _weaponEnemy;
    public BirdAnimator BirdAnimator => _birdAnimator;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _weaponEnemy = GetComponent<WeaponEnemy>();
        _birdAnimator = GetComponent<BirdAnimator>();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = transform.right * _speed;
    }

    public void Release()
    {
        Released?.Invoke(this);
    }
}
