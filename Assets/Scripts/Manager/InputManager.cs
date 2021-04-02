using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{


    #region Fields
    private float _moveDirX = 0f;
    private float _moveDirY = 0f;



    #endregion Fields

    #region Properties
    public float MoveDirX => _moveDirX;
    public float MoveDirY => _moveDirY;

    #endregion Properties

    #region Events



    #endregion Events

    #region Methods
    public void Initialize()
    {

    }


    protected override void Update()
    {

    

        if(Input.GetButtonUp("Fire3"))
        {
        }

        _moveDirX = Input.GetAxis("Horizontal");
        _moveDirY = Input.GetAxis("Vertical");
    }

    #endregion Methods



}
