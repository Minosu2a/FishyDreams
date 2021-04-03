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

    [Header("Door")]
    [SerializeField] private ObjectEventLogic _boxOfKey = null;
    [SerializeField] private Outline _doorOutline = null;
    [SerializeField] private GameObject _doorLocation = null;

    private bool _gotKey = false;

    [Header("Carton")]
    [SerializeField] private GameObject _firstKey = null;

    [Header("Key")]
    [SerializeField] private GameObject _doorObject = null;
    [SerializeField] private GameObject _newDoorObject = null;

    [Header("ExitPhase1")]
    [SerializeField] private Animator _fadeAnim = null;
    [SerializeField] private Transform _spawnPosition = null;
    [SerializeField] private GameObject _characterPrefab = null;


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
                                          //  AudioManager.Instance.Start2DSound("");
                                        }
                                        
                                        break;

                                    case 2:
                                        if(_gotKey == false)
                                        {
                                            _boxOfKey.Selectable = true;
                                        }
                                        else
                                        {
                                            _doorObject.SetActive(false);
                                            AudioManager.Instance.Start3DSound("S_DoorOpen", _doorLocation.transform);
                                            _newDoorObject.SetActive(true);
                                        }
                                        break;
                                    case 3:
                                        _firstKey.gameObject.SetActive(true);
                                        
                                        break;

                                    case 4:
                                        _gotKey = true;
                                        AudioManager.Instance.Start2DSound("S_KeyPickUp");
                                        _doorOutline.color = 1;

                                        break;
                                    case 5:      //EXIT
                                        AudioManager.Instance.Start2DSound("S_DoorOpen");
                                        _fadeAnim.SetTrigger("FadeOut");
                                        StartCoroutine(ExitPhase1());
                                        _phaseNumber++;
                                        break;
                                    case 6:     //FENETRE
                                        if(_phaseNumber == 4)
                                        {

                                        }
                                        else
                                        {

                                        }

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

    private IEnumerator ComputerStart()
    {
        yield return new WaitForSeconds(0.5f);
        _hudComputer.SetActive(true);
    }

    private IEnumerator ExitPhase1()
    {
        yield return new WaitForSeconds(1.2f);
        _characterPrefab.transform.position = _spawnPosition.transform.position;
        yield return new WaitForSeconds(0.2f);
        _fadeAnim.SetTrigger("FadeIn");

    }

    #endregion Methods

}
