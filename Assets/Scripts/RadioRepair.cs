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
        AudioManager.instance.PlayOneShot(FMODEvents.instance.radioStatic, this.transform.position);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.fixingRadio, this.transform.position);
        Debug.Log("Fixing Radio");
        GameManager.Instance.CanFixRadio = false;
    }
}
