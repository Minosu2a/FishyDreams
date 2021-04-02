using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    #region Fields

    [Header("Computer")]
    [SerializeField] private Camera _characterCam = null;
    [SerializeField] private CharacterMovement _characterMovement = null;

    [SerializeField] private Camera _computerCam = null;
    [SerializeField] private GameObject _hudComputer = null;
    [SerializeField] private SelectionManager _selectionManager = null;

    #endregion Fields
    #region Property
    #endregion Property


    #region Methods
    private void Awake()
    {
        UIManager.Instance.UIController = this;    
    }

    public void TooglePause()
    {
        GameManager.Instance.TogglePause();
    }


    public void ExitComputer()
    {
        _computerCam.gameObject.SetActive(false);
        _hudComputer.SetActive(false);

        _characterMovement.MovementActive = true;
        Cursor.lockState = CursorLockMode.Locked;
        _characterCam.gameObject.SetActive(true);
        _selectionManager.ComputerActive = false;



    }

    public void ExitPage()
    {

    }


    #endregion Methods


}
