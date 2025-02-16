using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class Expedition : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator expedCutscene;
    [SerializeField] private Dialog_Instance dialog;

    private ExpeditionChecker expeditionChecker;
    private PlayerData playerData;
    private UnityAction _onCompleteAction;

    private void Start()
    {
        expeditionChecker = GameManager.Instance.FetchExpeditionChecker();
        playerData = GameManager.Instance.FetchPlayerData();
    }

    public void Interact()
    {
        Debug.Log("Go to Expedition");
        
        if (GameManager.Instance.CanExped()) GoToExpedition();
        else if (!GameManager.Instance.CanExped()) DialogManager.Instance.SetDialog(dialog);
    }
    
    private void GoToExpedition()
    {
        StartCoroutine("ExpedCutscene");
        ConsumeResources();
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

    private void ConsumeResources()
    {
        playerData.Oxygen = expeditionChecker.ConsumeOxygen(playerData.Oxygen);
        playerData.FoodSupply = expeditionChecker.ConsumeFood(playerData.FoodSupply);
    }
}
