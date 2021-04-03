using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Vector3 _move = Vector3.zero;
    [SerializeField] private CharacterController _controller;

    [SerializeField] private float _speed = 12f;

    [SerializeField] private bool _movementActive = true;



    #region Properties
    public bool MovementActive
    {
        get
        {
            return _movementActive;
        }
        set
        {
            _movementActive = value;
        }

    }

    #endregion Properties

    void Start()
    {
        CharacterManager.Instance.CharacterController = this;
    }

    void Update()
    {

        if(_movementActive == true)
        {

            float x = InputManager.Instance.MoveDirX;
            float y = InputManager.Instance.MoveDirY;


            _move = transform.right * x + transform.forward * y;

            _controller.Move(_move * _speed * Time.deltaTime);
        }


    }
}
