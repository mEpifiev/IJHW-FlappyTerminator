using System;
using UnityEngine;

[RequireComponent(typeof(Player))]                
public class PlayerCollisionHandler : MonoBehaviour
{
    public event Action<IInteractable> CollisionDetected;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out IInteractable interactable))
            CollisionDetected?.Invoke(interactable);
    }
}
