using System;
using UnityEngine;

[RequireComponent(typeof(BirdJumper))]
[RequireComponent(typeof(PlayerCollisionHandler))]
[RequireComponent(typeof(BirdAnimator))]
public class Player : MonoBehaviour
{
    private BirdJumper _jumper;
    private PlayerCollisionHandler _collisionHandler;
    private BirdAnimator _animator;

    public event Action GameOver;

    private void OnEnable()
    {
        _collisionHandler.CollisionDetected += OnCollisionDetected;
    }

    private void OnDisable()
    {
        _collisionHandler.CollisionDetected -= OnCollisionDetected;
    }

    private void Awake()
    {
        _jumper = GetComponent<BirdJumper>();
        _collisionHandler = GetComponent<PlayerCollisionHandler>();
        _animator = GetComponent<BirdAnimator>();
    }

    public void Reset()
    {
        _jumper.Reset();
        _animator.PlayFlyAnimation();
    }

    private void OnCollisionDetected(IInteractable interactable)
    {
        if (interactable is Ground)
            Die();
    }

    private void Die()
    {
        _animator.StopFlyAnimation();
        GameOver?.Invoke();
    }
}