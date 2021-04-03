using cakeslice;
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
    [SerializeField] private Outline _outline = null;


    [SerializeField] private bool _hintActivated = false;





    [Header("Look")]
    [SerializeField] private bool _soundOnLook = false;
    [SerializeField] private string _soundToPlayOnLook = string.Empty;
    private bool _lookAudioPlayed = false;




    #endregion Fields

    #region Properties
    public bool Selectable
    {
        get
        {
            return _selectable;
        }
        set
        {
            _selectable = value;
        }
    }

    public Outline Outline
    {
        get
        {
            return _outline;
        }
        set
        {
            _outline = value;
        }
    }
    public int ObjectNumber
    {
        get
        {
            return _objectNumber;

        }
        set
        {
            _objectNumber = value;
        }
    }
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

    public bool HintActivated => _hintActivated;





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
