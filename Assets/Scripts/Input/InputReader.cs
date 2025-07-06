using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const KeyCode Jump = KeyCode.Space;

    public event Action Jumped;
    public event Action Shoted;

    private void Update()
    {
        if (Input.GetKeyDown(Jump))
            Jumped?.Invoke();

        if(Input.GetMouseButtonDown(1))
            Shoted?.Invoke();
    }
}
