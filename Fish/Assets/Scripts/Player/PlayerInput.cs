using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Vector2 _movement;
    public Vector2 Movement { get { return _movement; } private set { _movement = value; } }

    private bool _dashing;
    public bool Dashing { get { return _dashing;} private set { _dashing = value;} }

    private bool _shrinkFish;
    public bool ShrinkFish { get { return _shrinkFish; } private set { _shrinkFish = value; } }

    PlayerInputActions input;

    private void OnEnable() {
        input = new PlayerInputActions();

        input.ActionMap.Movement.performed += ctx => Movement = ctx.ReadValue<Vector2>();

        input.ActionMap.Dash.started += ctx => Dashing = true;
        input.ActionMap.Dash.canceled += ctx => Dashing = false;

        input.ActionMap.ShrinkFish.performed += ctx => ShrinkFish = true;
        input.ActionMap.ShrinkFish.canceled += ctx => ShrinkFish = false;

        input.ActionMap.CloseGame.performed += ctx => Application.Quit();

        

        input.Enable();
    }
}
