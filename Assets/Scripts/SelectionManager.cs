using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using cakeslice;

public class SelectionManager : MonoBehaviour
{

    #region Fields

    [Header("Main Logic")]
    [SerializeField] private int _phaseNumber = 1;

    [SerializeField] private string eventTag = "ObjectEvent";

    [SerializeField] private Material _highlightMaterial = null;
    private Material _defaultMaterial = null;

    Transform _selection = null;


    [Header("Computer")]
    [SerializeField] private Camera _characterCam = null;
    [SerializeField] private CharacterMovement _characterMovement = null;

    [SerializeField] private Camera _computerCam = null;
    [SerializeField] private GameObject _hudComputer = null;

    private bool _computerActive = false;

    [SerializeField] private GameObject _keyboardLocation = null;

    [SerializeField] private GameObject _computerLight = null;
    [SerializeField] private Outline _mouseOutline = null;
    [SerializeField] private Outline _screenOutline = null;
    [SerializeField] private Outline _keyboardOutline = null;
    [SerializeField] private ObjectEventLogic _mouseLogic = null;
    [SerializeField] private ObjectEventLogic _screenLogic = null;
    [SerializeField] private ObjectEventLogic _keyboardLogic = null;



    [Header("Door")]
    [SerializeField] private ObjectEventLogic _boxOfKey = null;
    [SerializeField] private Outline _doorOutline = null;
    [SerializeField] private GameObject _doorLocation = null;

    private bool _firstTime = true;

    private bool _gotKey = false;

    [Header("Carton")]
    [SerializeField] private bool _keyDiscovered = false;
    [SerializeField] private GameObject _firstKey = null;

    [Header("FirstKey")]
    [SerializeField] private GameObject _doorObject = null;
    [SerializeField] private GameObject _newDoorObject = null;

    [Header("ExitPhase1")]
    [SerializeField] private Animator _fadeAnim = null;
    [SerializeField] private Transform _spawnPosition = null;
    [SerializeField] private GameObject _characterPrefab = null;
    [SerializeField] private CharacterMovement _move = null;

    [SerializeField] private bool _inPhaseTransition = false;


    [Header("MovingKey")]
    [SerializeField] private ObjectEventLogic _carton = null;


    [SerializeField] private ObjectEventLogic _fauteuil = null;

    private bool _hasGlue = false;
    [SerializeField] private GameObject _gluePos = null;
    [SerializeField] private ObjectEventLogic _glueLogic = null;

    [SerializeField] private int _keyNumber = 0;

    [SerializeField] private GameObject[] _keys = null;
    [SerializeField] private int _dialogueNumber = 1;


    [Header("Window")]
    [SerializeField] private Outline _windowOutline = null;
    [SerializeField] private ObjectEventLogic _windowLogic = null;
    [SerializeField] private GameObject _windowSoundPos = null;

    [SerializeField] private GameObject _store1 = null;
    [SerializeField] private GameObject _store2 = null;
    [SerializeField] private GameObject _store3 = null;

    [SerializeField] private int _stepNumber = 1;





    [Header("Other")]
    [SerializeField] private GameObject[] _objectToReset = null;
    [SerializeField] private ExitTrigger _exitTrigger = null;
    private bool _pcFirstTime = true;
    private bool _hasJumped = false;

    #endregion Fields




    #region Properties
    public bool ComputerActive
    {
        get
        {
            return _computerActive;
        }
        set
        {
            _computerActive = value;
        }
    }

    #endregion Properties




    #region Methods
    void Update()
    {


       if(_selection != null)
       {
            ObjectEventLogic eventLogic2 = _selection.GetComponent<ObjectEventLogic>();
            eventLogic2.Outline.eraseRenderer = true;
       }

        if (_computerActive == false)
       {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Transform selection = hit.transform;

                //PREMIER CHECK SI IL Y A LE TAG OU NON (OPTIMISATION)
                if (selection.gameObject.tag == eventTag)
                {



                    //DEUXIEME CHECK SI L'OBJET TROUVER A UN OBJECTEVENTLOGIC

                    if (selection.GetComponent<ObjectEventLogic>() != null)
                    {

                        ObjectEventLogic eventLogic = selection.GetComponent<ObjectEventLogic>();


                        //REGARDE SI IL EST EN SELECTABLE 
                        if(eventLogic.Selectable == true)
                        {
                            if(eventLogic.HintActivated == true)
                            {

                                eventLogic.Outline.eraseRenderer = false;

                                /*   Renderer selectionRenderer = selection.GetComponent<Renderer>();
                                   _defaultMaterial = selectionRenderer.material;
                                   selectionRenderer.material = _highlightMaterial;*/

                                 _selection = selection;
                            }

                            if (Input.GetButtonDown("Fire1"))
                            {

                                switch(eventLogic.ObjectNumber)
                                {
                                    case 1:         //ORDI

                                        if(_phaseNumber == 3 || _phaseNumber == 4)
                                        {
                                            if(_pcFirstTime == true)
                                            {
                                                AudioManager.Instance.Start2DSound("D_HintLuck");
                                                _pcFirstTime = false;
                                            }
                                            

                                            _characterMovement.MovementActive = false;
                                            Cursor.lockState = CursorLockMode.None;
                                            _characterCam.gameObject.SetActive(false);
                                            _computerActive = true;
                                            AudioManager.Instance.Start3DSound("S_Press", _keyboardLocation.transform);
                                            _computerCam.gameObject.SetActive(true);

                                            StartCoroutine(ComputerStart());
                                        }
                                        else
                                        {
                                           AudioManager.Instance.Start2DSound("D_OrdiLock"); //VOIX
                                        }
                                        
                                        break;

                                    case 2:     //PORTE 
                                        if(_gotKey == false)
                                        {
                                           if( _firstTime == true)
                                           {
                                                AudioManager.Instance.Start2DSound("D_DoorLocked");
                                                _firstTime = false;
                                           }
                                            _boxOfKey.Selectable = true;
                                        }
                                        else
                                        {
                                            if(_phaseNumber == 2)
                                            {
                                                AudioManager.Instance.Start2DSound("D_Thanks");
                                            }
                                            _doorObject.SetActive(false);
                                            AudioManager.Instance.Start3DSound("S_DoorOpen", _doorLocation.transform);
                                            _newDoorObject.SetActive(true);
                                        }
                                        break;
                                    case 3: //CARTON
                                        if (_keyDiscovered == false)
                                        {
                                            _keyDiscovered = true;
                                            _firstKey.gameObject.SetActive(true);
                                        }

                                        break;

                                    case 4: //FIRST KEY
                                        AudioManager.Instance.Start2DSound("D_KeyFound");
                                        _gotKey = true;
                                        AudioManager.Instance.Start2DSound("S_KeyPickUp");
                                        _doorOutline.color = 1;

                                        break;
                                    case 5:      //EXIT PORTE
                                        if(_inPhaseTransition == false)
                                        {
                                            _move.MovementActive = false;
                                            AudioManager.Instance.Start2DSound("S_DoorOpen");
                                            _fadeAnim.SetTrigger("FadeOut");
                                            StartCoroutine(ExitPhase1());
                                            _phaseNumber++;
                                            _inPhaseTransition = true;
                                        }
                                        
                                        break;
                                    case 6:     //FENETRE
                                        if(_phaseNumber == 4)
                                        {
                                            StartCoroutine(WindowOpen());
                                        }
                                        else
                                        {
                                            AudioManager.Instance.Start2DSound("D_WindowLock"); //VOIX 
                                        }

                                        break;

                                    case 7: //EMPLACEMENT DE LA MOVING KEY
                                        _keys[0].SetActive(true);
                                        AudioManager.Instance.Start3DSound("S_Anime", _keys[0].transform);
                                       // _keys[1].GetComponent<ObjectEventLogic>().Selectable = true;


                                        break;

                                    case 8: //KEY QUI BOUGE
                                        if(_hasGlue == false)
                                        {
                                            _carton.Selectable = true;

                                            StartCoroutine(KeyDisappear());

                                        }
                                        else
                                        {
                                          
                                            StartCoroutine(GotKey());
                                            _doorOutline.color = 1;
                                        }
                                        break;
                                    case 9: //COLLE
                                        AudioManager.Instance.Start3DSound("S_Glue", _gluePos.transform);
                                        if(_hasGlue == false)
                                        {
                                            AudioManager.Instance.Start2DSound("D_Glue");
                                        }
                                        _hasGlue = true;
                                        break;
                                    case 10:
                                        _gotKey = true;
                                        AudioManager.Instance.Start2DSound("D_WinP3");
                                        AudioManager.Instance.Start2DSound("S_KeyPickUp");
                                        _doorOutline.color = 1;
                                        break;
                                }

                                if(eventLogic.DeleteOnSelect == true)
                                {
                                    //AudioManager.Instance.Start3DSound("S_Vanish", selection.gameObject.transform); //METTRE SUR LE FX
                                    selection.gameObject.SetActive(false);
                                }


                                if (eventLogic.SoundOnSelect == true && eventLogic.SelectAudioPlayed == false)    //JOUE LE SON
                                {
                                    eventLogic.SelectAudioPlayed = true;
                                    AudioManager.Instance.Start2DSound(eventLogic.SoundToPlayOnSelect);
                                }

                            }

                        }


                  


                        if (eventLogic.SoundOnLook == true && eventLogic.LookAudioPlayed == false)
                        {
                            eventLogic.LookAudioPlayed = true;
                            AudioManager.Instance.Start2DSound(eventLogic.SoundToPlayOnLook);
                        }



                    }
                    else
                    {
                        Debug.Log("PAS DE TAG");
                    }




                }
            }

       }

      
    }

    private void Restart()
    {
        _gotKey = false;

        for(int i = 0; i < _objectToReset.Length; i++)
        {
            _objectToReset[i].SetActive(true);
        }

        _boxOfKey.ObjectNumber = 0;
        _doorObject.SetActive(true);
        _newDoorObject.SetActive(false);
        _doorOutline.color = 0;

        switch (_phaseNumber)
        {
            case 2:
                AudioManager.Instance.Start2DSound("D_SpawnPhase2");
                _fauteuil.Selectable = true;
                _keys[0].SetActive(true);
                _keys[1].SetActive(true);
                _keys[2].SetActive(true);
                _keys[3].SetActive(true);
                break;

            case 3:
                AudioManager.Instance.Start2DSound("D_SpawnPhase3");
                _store1.SetActive(false);
                _store2.SetActive(true);

                _screenOutline.color = 1;
                _screenLogic.Selectable = true;
                _keyboardOutline.color = 1;
                _keyboardLogic.Selectable = true;
                _mouseLogic.Selectable = true;
                _mouseOutline.color = 1;
                break;

            case 4:
                UIManager.Instance.UIController._lastPhase = true;
                _store2.SetActive(false);
                _store3.SetActive(true);
                _windowOutline.color = 1;
                _windowLogic.Selectable = true;
                break;
        }

        _exitTrigger.Restart();

    }

    private IEnumerator ComputerStart()
    {
        yield return new WaitForSeconds(0.5f);
        _hudComputer.SetActive(true);
    }

    public IEnumerator ExitPhase1()
    {
        yield return new WaitForSeconds(1f);
        Restart();
        yield return new WaitForSeconds(0.2f);
        _characterPrefab.transform.position = _spawnPosition.transform.position;
        yield return new WaitForSeconds(0.2f);
        _fadeAnim.SetTrigger("FadeIn");
        _inPhaseTransition = false;
        _move.MovementActive = true;


    }

    private IEnumerator KeyDisappear()
    {
        _glueLogic.Selectable = true;

        int keyToRemove = _keyNumber;
        int keyToAdd;

        if (keyToRemove == 3)
        {
            keyToAdd = 0;
           _keyNumber = 0;
        }
        else
        {
            keyToAdd = _keyNumber + 1;
            _keyNumber++;
        }


        AudioManager.Instance.Start3DSound("S_KeyVanish", _keys[keyToRemove].transform);
        _keys[keyToRemove].GetComponent<ObjectEventLogic>().Selectable = false;

        yield return new WaitForSeconds(1.2f);
        _keys[keyToRemove].GetComponent<Renderer>().enabled = false;

        switch (_dialogueNumber)
        {
            case 1:
                AudioManager.Instance.Start2DSound("D_KeyGrab1");
                StartCoroutine(FishHint());
                break;
            case 2:
                AudioManager.Instance.Start2DSound("D_KeyGrab3");
                break;
            case 3:
                AudioManager.Instance.Start2DSound("D_KeyGrab4");
                break;
            default:
                AudioManager.Instance.Start2DSound("D_KeyGrabD");
                break;
        }

        yield return new WaitForSeconds(0.2f);
        _keys[keyToAdd].GetComponent<ObjectEventLogic>().Selectable = true;
        _keys[keyToAdd].GetComponent<Renderer>().enabled = true;
        //_keys[keyToAdd].SetActive(true);
        AudioManager.Instance.Start3DSound("S_KeyAppear", _keys[keyToAdd].transform);
        _dialogueNumber++;

    }

    private IEnumerator FishHint()
    {
        yield return new WaitForSeconds(8f);
        AudioManager.Instance.Start3DSound("D_Fish1", _carton.gameObject.transform);
    }

    private IEnumerator GotKey()
    {
        _keys[_keyNumber].GetComponent<ObjectEventLogic>().Selectable = false;
        AudioManager.Instance.Start3DSound("S_GotKey", _keys[_keyNumber].transform);
        yield return new WaitForSeconds(0.3f);
        _gotKey = true;
        AudioManager.Instance.Start2DSound("S_KeyPickUp");
        _keys[_keyNumber].SetActive(false);


    }

    private IEnumerator WindowOpen()
    {
        if (_hasJumped == false)
        {
              switch (_stepNumber)
               {

            

            case 1:
                AudioManager.Instance.Start3DSound("S_WindowOpen1", _windowSoundPos.transform);
            AudioManager.Instance.Start2DSound("RS_Quake");
            _characterCam.fieldOfView = 61;
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 62;
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 63;
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 64;
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 65;
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 66;
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 67;
            AudioManager.Instance.Start2DSound("D_Window1");
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 68;
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 69;
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 70;

            _stepNumber++;
            break;

            case 2:
                AudioManager.Instance.Start3DSound("S_WindowOpen2", _windowSoundPos.transform);
            _characterCam.fieldOfView = 71;
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 72;
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 73;
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 74;
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 75;
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 76;
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 77;
            AudioManager.Instance.Start2DSound("D_Window2");
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 78;
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 79;
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 80;

            _stepNumber++;
            break;

             case 3:
                 AudioManager.Instance.Start3DSound("S_WindowBreak", _windowSoundPos.transform);
            _characterCam.fieldOfView = 81;
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 82;
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 83;
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 84;
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 85;
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 86;
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 87;
            AudioManager.Instance.Start2DSound("D_Window3");
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 88;
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 89;
            yield return new WaitForSeconds(0.05f);
            _characterCam.fieldOfView = 90;

            _stepNumber++;
            break;

            case 4:
                _characterMovement.MovementActive = false;
                    _hasJumped = true;

                    _fadeAnim.SetTrigger("FadeOut");
                    AudioManager.Instance.Start2DSound("S_Jump");
                    yield return new WaitForSeconds(1f);
            AudioManager.Instance.StopSound(ESoundType.REPETITIVE2D, "RS_Quake");
            AudioManager.Instance.StopSound(ESoundType.REPETITIVE3D, "RS_Fan");
            AudioManager.Instance.StopSound(ESoundType.REPETITIVE2D, "RS_Ambiant");

            AudioManager.Instance.Start2DSound("S_Fall");
            yield return new WaitForSeconds(5f);

            AudioManager.Instance.PlayMusicWithFadeIn("RS_Death", 2f);
            yield return new WaitForSeconds(3f);
            AudioManager.Instance.PlayTheme();
            _fadeAnim.SetTrigger("Ending");
            yield return new WaitForSeconds(92f);
            AudioManager.Instance.Start2DSound("S_Sayonara");
            yield return new WaitForSeconds(2.5f);
            Application.Quit();
            break;
        }
    }

    }
    private IEnumerator TrueEnding()
    {
        _characterMovement.MovementActive = false;
        _hudComputer.SetActive(false);
        _fadeAnim.SetTrigger("FadeOut");
        AudioManager.Instance.StopSound(ESoundType.REPETITIVE2D, "RS_Quake");
        AudioManager.Instance.Start2DSound("RS_Quake");

        yield return new WaitForSeconds(3f);
        AudioManager.Instance.StopSound(ESoundType.REPETITIVE2D, "RS_Quake");
        AudioManager.Instance.StopSound(ESoundType.REPETITIVE3D, "RS_Fan");
        AudioManager.Instance.StopSound(ESoundType.REPETITIVE2D, "RS_Ambiant");
        yield return new WaitForSeconds(0.2f);
        AudioManager.Instance.Start2DSound("S_Breath");
        yield return new WaitForSeconds(1.1f);
        AudioManager.Instance.PlayTheme();
        AudioManager.Instance.PlayMusicWithFadeIn("RS_Death", 2f);
        _fadeAnim.SetTrigger("Ending");
        yield return new WaitForSeconds(92f);
        AudioManager.Instance.Start2DSound("S_Sayonara");
        yield return new WaitForSeconds(2.5f);
        Application.Quit();

    }


    public void Ending()
    {
        StartCoroutine(TrueEnding());
    }


    #endregion Methods

}
