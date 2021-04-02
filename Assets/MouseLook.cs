using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    #region Fields
    private float _mouseX = 0;
    private float _mouseY = 0;

    private float _xRotation = 0f;

    [SerializeField] private float _mouseSensitivity = 100f;
    [SerializeField] private Transform _playerBody;


    #endregion Fields


    #region Property
    #endregion Property


    #region Methods
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        _mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        _mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        _xRotation -= _mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRotation,0f,0f);
        _playerBody.Rotate(Vector3.up * _mouseX);

    }
    #endregion Methods

}
