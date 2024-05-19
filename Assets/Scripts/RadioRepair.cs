using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioRepair : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if (GameManager.Instance.CanFixRadio)
            RepairRadio();
    }
    
    private void RepairRadio()
    {
        //Replace Radio Spitesheet
        //Display dialog
        //Itirate Sequence
        
        Debug.Log("Fixing Radio");
        GameManager.Instance.CanFixRadio = false;
    }
}
