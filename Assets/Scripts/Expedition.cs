using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Expedition : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator expedCutscene;
    
    public void Interact()
    {
        Debug.Log("Go to Expedition");
        if (!GameManager.Instance.CanExped()) return;
            GoToExpedition();
    }
    
    private void GoToExpedition()
    {
        StartCoroutine("ExpedCutscene");
        //consume resources
    }

    IEnumerator ExpedCutscene()
    {
        GameManager.Instance.playerCanMove = false;
        EventManager.ON_DOOR_OPEN?.Invoke();
        yield return new WaitForSeconds(2);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.dirtExpedition, transform.position);
        expedCutscene.SetTrigger("StartExped");
        yield return new WaitForSeconds(2);
        FinishExpedition();
    }

    private void FinishExpedition()
    {
        EventManager.ON_DOOR_CLOSE?.Invoke();
        GameManager.Instance.CanFixRadio = true;
        GameManager.Instance.playerCanMove = true;
        //Set tooltip for radio fix
    }
}
