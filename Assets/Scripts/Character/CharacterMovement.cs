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


            //_move = transform.right * x + transform.forward * y;
            Vector3 movementForward = transform.forward * Input.GetAxis("Vertical");
            Vector3 movementHorizontal = transform.right * Input.GetAxis("Horizontal");

            Vector3 move = (movementForward + movementHorizontal) * _speed;

            _controller.SimpleMove(move) ;
           // _controller.SimpleMove(movementHorizontal * _speed * Time.deltaTime);

            // _controller.SimpleMove(_move * _speed * Time.deltaTime);
        }


    }
}
