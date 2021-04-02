using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEventLogic : MonoBehaviour
{

    #region Fields
    [Header("Selectable")]
    [SerializeField] private bool _selectable = false;
    [SerializeField] private int _objectNumber;
    [SerializeField] private bool _deleteOnSelect = false;
    [SerializeField] private bool _soundOnSelect = false;
    [SerializeField] private string _soundToPlayOnSelect = string.Empty;
    private bool _selectAudioPlayed = false;



    [Header("Interact")]
    [SerializeField] private bool _soundOnInteract = false;
    [SerializeField] private string _soundToPlayOnInteract = string.Empty;
    private bool _interactAudioPlayed = false;


    [Header("Look")]
    [SerializeField] private bool _soundOnLook = false;
    [SerializeField] private string _soundToPlayOnLook = string.Empty;
    private bool _lookAudioPlayed = false;




    #endregion Fields

    #region Properties
    public bool Selectable => _selectable;
    public int ObjectNumber => _objectNumber;
    public bool DeleteOnSelect => _deleteOnSelect;
    public bool SoundOnSelect => _soundOnSelect;
    public string SoundToPlayOnSelect => _soundToPlayOnSelect;
    public bool SelectAudioPlayed
    {
        get
        {
            return _selectAudioPlayed;
        }
        set
        {
            _selectAudioPlayed = value;
        }

    }
    


    public bool SoundOnInteract => _soundOnInteract;
    public string SoundToPlayOnInteract => _soundToPlayOnInteract;
    public bool InteractAudioPlayed
    {
        get
        {
            return _interactAudioPlayed;
        }
        set
        {
            _interactAudioPlayed = value;
        }

    }


    public bool SoundOnLook => _soundOnLook;
    public string SoundToPlayOnLook => _soundToPlayOnLook;
    public bool LookAudioPlayed
    {
        get
        {
            return _lookAudioPlayed;
        }
        set
        {
            _lookAudioPlayed = value;
        }
        
    }


    #endregion Properties

}
