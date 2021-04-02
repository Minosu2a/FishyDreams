using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    #region Fields
    private CharacterMovement _characterController = null;
    #endregion Fields


    #region Property
    public CharacterMovement CharacterController
    {

        get
        {
            return _characterController;
        }
        set
        {
            if (_characterController == null)
            {
                _characterController = value;
            }
        }

    }
    #endregion Property


    #region Methods
   public void Initialize()
   {
        // AudioManager.Instance.Start3DSound("R", CharacterController.transform);
   }

    #endregion Methods

}
