using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    protected Rigidbody2D Rigidbody2D;
    protected Vector2 Direction;

    protected virtual void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * _speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction.normalized;
    }
}
