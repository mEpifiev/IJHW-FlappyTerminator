using UnityEngine;

public class ObjectRemover : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
            enemy.Release();
    }
}
