using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const KeyCode Jump = KeyCode.Space;

    public event Action Jumped;

    private void Update()
    {
        if (Input.GetKeyDown(Jump))
            Jumped?.Invoke();
    }
}
