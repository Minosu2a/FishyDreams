using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    #region Fields
    private CharacterStateController _characterStateController = null;
    #endregion Fields


    #region Property
    public CharacterStateController CharacterStateController
    {

        get
        {
            return _characterStateController;
        }
        set
        {
            if (_characterStateController == null)
            {
                _characterStateController = value;
            }
        }

    }
    #endregion Property


    #region Methods
   public void Initialize()
   {

   }

    #endregion Methods

}
