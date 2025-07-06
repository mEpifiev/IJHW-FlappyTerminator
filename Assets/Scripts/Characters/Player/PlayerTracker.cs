using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private float _xOffset;

    private void Update()
    {
        Vector2 position = transform.position;
        position.x = _target.transform.position.x + _xOffset;
        transform.position = position;
    }
}