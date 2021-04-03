using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject _screenLocation = null;
    [SerializeField] private GameObject _mouseLocation = null;
    [SerializeField] private GameObject _keyboardLocation = null;




    [Header("Computer Open/Close")]
    [SerializeField] private Camera _characterCam = null;
    [SerializeField] private CharacterMovement _characterMovement = null;

    [SerializeField] private GameObject _computerCam = null;
    [SerializeField] private GameObject _hudComputer = null;
    [SerializeField] private SelectionManager _selectionManager = null;

    [Header("Computer Content")]
    [SerializeField] private GameObject _luckWindow = null;
    [SerializeField] private GameObject _searchWindow = null;

    private bool _keyBought = false;

    [Header("InputField")]
    [SerializeField] private KeyCode _validationKey1 = KeyCode.KeypadEnter;
    [SerializeField] private KeyCode _validationKey2 = KeyCode.Return;
    [SerializeField] private TMP_InputField _inputField = null;
    [Header("Buying")]
    [SerializeField] private GameObject _keyPosition = null;


    [Header("Other")]

    [SerializeField] private GameObject _computerPos = null;


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

    private void Start()
    {
        AudioManager.Instance.Start3DSound("RS_Fan", _computerPos.transform);
        AudioManager.Instance.Start2DSound("RS_Ambiant");
    }

    private void Update()
    {

        if (_inputField != null)
        {
            _inputField.ActivateInputField(); //Reste sur le InputField 
        }

        if (Input.GetKeyDown(_validationKey1) || Input.GetKeyDown(_validationKey2))
        {
            AudioManager.Instance.Start3DSound("S_Press", _keyboardLocation.transform);
            SearchOpen();
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
        AudioManager.Instance.Start3DSound("S_Click", _mouseLocation.transform);



    }

    public void ExitSearchPage()
    {
        AudioManager.Instance.Start3DSound("S_Click", _mouseLocation.transform);
        _searchWindow.SetActive(false);
        AudioManager.Instance.Start3DSound("S_Sayonara", _screenLocation.transform);
        AudioManager.Instance.StopSound(ESoundType.REPETITIVE3D, "M_Japan");
    }

    public void ExitLuckPage()
    {
        AudioManager.Instance.Start3DSound("S_Click", _mouseLocation.transform);
        _luckWindow.SetActive(false);
    }

    public void SearchButton()
    {
        AudioManager.Instance.Start3DSound("S_Click", _mouseLocation.transform);
        SearchOpen();
    }

    public void SearchOpen()
    {

        AudioManager.Instance.Start3DSound("M_Japan", _screenLocation.transform);
        AudioManager.Instance.Start3DSound("S_Konichiwa", _screenLocation.transform);
        _searchWindow.SetActive(true);
    }

    public void KawaiSound()
    {
        AudioManager.Instance.Start3DSound("S_Anime", _screenLocation.transform);
    }

    public void Luck()
    {
       
        AudioManager.Instance.Start3DSound("S_Click", _mouseLocation.transform);

        if(_keyBought == false)
        {
            _luckWindow.SetActive(true);
        }
        else
        {
            AudioManager.Instance.Start3DSound("M_Japan", _screenLocation.transform);
            AudioManager.Instance.Start3DSound("S_Konichiwa", _screenLocation.transform);
            _searchWindow.SetActive(true);
        }
    }

   
    public void Buy()
    {
        _keyBought = true;
  

        AudioManager.Instance.Start3DSound("S_Buy", _screenLocation.transform);

        _keyPosition.SetActive(true);

        StartCoroutine(BuyKey());
        AudioManager.Instance.Start3DSound("S_Click", _mouseLocation.transform);

        _luckWindow.SetActive(false);
    }


    public IEnumerator BuyKey()
    {
        yield return new WaitForSeconds(0.5f);
        AudioManager.Instance.Start3DSound("S_KeyAppear", _keyPosition.transform);

        yield return new WaitForSeconds(0.8f);
        AudioManager.Instance.Start2DSound("D_Delivery");


    }

    #endregion Buttons



    #endregion Methods


}
