using System;
using UnityEngine;

[RequireComponent(typeof(PlayerJumper))]
[RequireComponent(typeof(PlayerCollisionHandler))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(BirdAnimator))]
public class Player : MonoBehaviour
{
    private PlayerJumper _jumper;
    private PlayerCollisionHandler _collisionHandler;
    private InputReader _inputReader;
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
        _jumper = GetComponent<PlayerJumper>();
        _collisionHandler = GetComponent<PlayerCollisionHandler>();
        _inputReader = GetComponent<InputReader>();
        _animator = GetComponent<BirdAnimator>();
    }

    public void Reset()
    {
        _inputReader.enabled = true;
        _jumper.Reset();
        _animator.PlayFlyAnimation();
    }

    public void Die()
    {
        _inputReader.enabled = false;
        _animator.StopFlyAnimation();
        GameOver?.Invoke();
    }

    private void OnCollisionDetected(IInteractable interactable)
    {
        if (interactable is Ground)
            Die();
    }
}