using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInput : MonoBehaviour
{
    private PlayerInputActions input;

    private Vector2 _navi;

    public Vector2 Navi
    {
        get { return _navi; }
        private set { _navi = value; }
    }

    private Vector2 _nup;

    public Vector2 nUp
    {
        get { return _nup; }
        private set { _nup = value; }
    }

    private Vector2 _ndown;

    public Vector2 nDown
    {
        get { return _ndown; }
        private set { _ndown = value; }
    }

    private bool _nselect;

    public bool nSelect
    {
        get { return _nselect; }
        private set { _nselect = value; }
    }

    private bool _nback;

    public bool nBack
    {
        get { return _nback; }
        private set { _nback = value; }
    }

    private bool _nmenu;

    public bool nMenu
    {
        get { return _nmenu; }
        private set { _nmenu = value; }
    }

    private void OnEnable()
    {
        input = new PlayerInputActions();

        input.MenuMap.Nav.performed += ctx => Navi = ctx.ReadValue<Vector2>();
        input.MenuMap.NavUp.performed += ctx => nUp = ctx.ReadValue<Vector2>();
        input.MenuMap.NavDown.performed += ctx => nDown = ctx.ReadValue<Vector2>();

        input.MenuMap.Select.started += ctx => nSelect = true;
        input.MenuMap.Select.canceled += ctx => nSelect = false;
        input.MenuMap.Back.started += ctx => nBack = true;
        input.MenuMap.Back.canceled += ctx => nBack = false;
        input.MenuMap.NavMenu.started += ctx => nMenu = true;
        input.MenuMap.NavMenu.canceled += ctx => nMenu = false;

        input.Enable();
    }
}