using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{

    [SerializeField] private Outline _outline = null;
    [SerializeField] private ObjectEventLogic _logic = null;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _outline.eraseRenderer = false;
            _logic.Selectable = true;
        }
    }


    public void Restart()
    {
        _outline.eraseRenderer = true;
        _logic.Selectable = false;
    }
}
