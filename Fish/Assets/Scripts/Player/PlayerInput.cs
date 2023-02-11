using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Vector2 _movement;
    public Vector2 Movement { get { return _movement; } private set { _movement = value; } }

    PlayerInputActions input;

    private void OnEnable() {
        input = new PlayerInputActions();

        input.ActionMap.Movement.performed += ctx => Movement = ctx.ReadValue<Vector2>();

        input.Enable();
    }
}
