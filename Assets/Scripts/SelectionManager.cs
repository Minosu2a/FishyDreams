using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{

    #region Fields

    [Header("Main Logic")]
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
            Renderer selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = _defaultMaterial;
            _selection = null;
       }

       if(_computerActive == false)
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

                            if (Input.GetButtonDown("Fire1"))
                            {

                                switch(eventLogic.ObjectNumber)
                                {
                                    case 1:
                                        _characterMovement.MovementActive = false;
                                        Cursor.lockState = CursorLockMode.None;
                                        _characterCam.gameObject.SetActive(false);
                                        _computerActive = true;

                                        _computerCam.gameObject.SetActive(true);
                                        _hudComputer.SetActive(true);
                                        break;
                                }

                                if (eventLogic.SoundOnSelect == true && eventLogic.SelectAudioPlayed == false)    //JOUE LE SON
                                {
                                    eventLogic.SelectAudioPlayed = true;
                                    AudioManager.Instance.Start2DSound(eventLogic.SoundToPlayOnSelect);
                                }

                            }

                             Renderer selectionRenderer = selection.GetComponent<Renderer>();
                             _defaultMaterial = selectionRenderer.material;
                             selectionRenderer.material = _highlightMaterial;

                            _selection = selection;
                        }


                        if (eventLogic.SoundOnInteract == true && eventLogic.InteractAudioPlayed == false)
                        {
                            eventLogic.InteractAudioPlayed = true;
                            AudioManager.Instance.Start2DSound(eventLogic.SoundToPlayOnInteract);
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
    #endregion Methods

}
