using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject _screenLocation = null;


    [Header("Computer Open/Close")]
    [SerializeField] private Camera _characterCam = null;
    [SerializeField] private CharacterMovement _characterMovement = null;

    [SerializeField] private Camera _computerCam = null;
    [SerializeField] private GameObject _hudComputer = null;
    [SerializeField] private SelectionManager _selectionManager = null;

    [Header("Computer Content")]
    [SerializeField] private GameObject _luckWindow = null;
    [SerializeField] private GameObject _searchWindow = null;
    [SerializeField] private AudioSource _searchMusic = null;

    [Header("InputField")]
    [SerializeField] private KeyCode _validationKey1 = KeyCode.KeypadEnter;
    [SerializeField] private KeyCode _validationKey2 = KeyCode.Return;
    [SerializeField] private TMP_InputField _inputField = null;

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


    private void Update()
    {

        if (_inputField != null)
        {
            _inputField.ActivateInputField(); //Reste sur le InputField 
        }

        if (Input.GetKeyDown(_validationKey1) || Input.GetKeyDown(_validationKey2))
        {
            Search();
        }
    }


    #region Buttons
    public void ExitComputer()
    {
        _computerCam.gameObject.SetActive(false);
        _hudComputer.SetActive(false);

        _characterMovement.MovementActive = true;
        Cursor.lockState = CursorLockMode.Locked;
        _characterCam.gameObject.SetActive(true);
        _selectionManager.ComputerActive = false;



    }

    public void ExitSearchPage()
    {
        _searchWindow.SetActive(false);
        AudioManager.Instance.Start3DSound("S_Sayonara", _screenLocation.transform);
        AudioManager.Instance.StopSound(ESoundType.REPETITIVE3D, "M_Japan");
    }

    public void ExitLuckPage()
    {
        _luckWindow.SetActive(false);
    }

    public void Search()
    {
        AudioManager.Instance.Start3DSound("M_Japan", _screenLocation.transform);
        AudioManager.Instance.Start3DSound("S_Konichiwa", _screenLocation.transform);
        _searchWindow.SetActive(true);
        _searchMusic.Play();

    }


    public void Luck()
    {
        _luckWindow.SetActive(true);
    }

   
    public void Buy()
    {
        _luckWindow.SetActive(false);
    }

    #endregion Buttons



    #endregion Methods


}
