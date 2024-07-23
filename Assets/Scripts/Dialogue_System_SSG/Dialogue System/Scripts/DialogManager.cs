using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    #region IGNORE THESE, DO NOT USE THESE METHODS, DO NOT EDIT THESE METHODS
    [SerializeField] private Dialog_DialogBox dialogBox;
    //[SerializeField] private Dialog_CharacterImage leftCharacterImage;
    //[SerializeField] private Dialog_CharacterImage rightCharacterImage;
    
    private Animator animator;
    private Dialog_Instance instance;
    private UnityEvent onFinish;
    private int index;
    private bool allowNext;
    private bool allowAction;
    private bool dialogActive;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        
        
        animator = GetComponent<Animator>();
        onFinish = new UnityEvent();
        allowNext = false;
        allowAction = false;
        dialogActive = false;
    }
    #endregion

    public void SetDialog(Dialog_Instance dialog)
    {
        if (dialogActive) return;
        allowAction = false;
        OpenDialog();
        instance = dialog;
        index = 0;
        UpdateDialogBox();
    }

    public void SetDialog(Dialog_Instance dialog, UnityAction action)
    {
        if (dialogActive) return;
        allowAction = true;
        OpenDialog();
        onFinish.AddListener(action);
        instance = dialog;
        index = 0;
        UpdateDialogBox();
    }

    #region IGNORE THESE, DO NOT USE THESE METHODS, DO NOT EDIT THESE METHODS
    public void NextPrompt()
    {
        if (!allowNext) return;
        allowNext = false;
        StartCoroutine(AllowNextPrompt(0.3f));

        if (!dialogBox.IsComplete())
        {
            dialogBox.SkipDialog();
            return;
        }

        index++;
        if (index >= instance.conversation.Length)
        {
            CloseDialog();
            return;
        }
        UpdateDialogBox();
    }

    private void OpenDialog()
    {
        animator.SetTrigger("show");
        StartCoroutine(AllowNextPrompt(1));
        dialogActive = true;
    }

    private void CloseDialog()
    {
        if (allowAction)
        {
            onFinish.Invoke();
            onFinish.RemoveAllListeners();
        }
        animator.SetTrigger("hide");
        //leftCharacterImage.ShrinkCharacter();
        //rightCharacterImage.ShrinkCharacter();
        dialogActive = false;
        allowNext = false;
    }

    private void UpdateDialogBox()
    {
        //current index settings
        bool isSameCharacters = instance.conversation[index].isSameCharacters;
        bool isLeftTalking = instance.conversation[index].isLeftTalking;
        string dialog = instance.conversation[index].dialog;
        
        //for setting current prompt
        dialogBox.SetDialogText(dialog);
        if (isLeftTalking) ActivateLeftCharacter();
        else ActivateRightCharacter();

        //for setting characters
        int promptIndex = isSameCharacters ? 0 : index;
        DialogPrompt characterSettingsPrompt = instance.conversation[promptIndex];
        //leftCharacterImage.SetCharacterImage(characterSettingsPrompt.character1Settings.sprite);
        //rightCharacterImage.SetCharacterImage(characterSettingsPrompt.character2Settings.sprite);
        dialogBox.SetLeftCharacterName(characterSettingsPrompt.character1Settings.name, isLeftTalking);
        dialogBox.SetRightCharacterName(characterSettingsPrompt.character2Settings.name, !isLeftTalking);
    }

    private void ActivateLeftCharacter()
    {
        //leftCharacterImage.EnlargeCharacter();
       // rightCharacterImage.ShrinkCharacter();
    }

    private void ActivateRightCharacter()
    {
        //rightCharacterImage.EnlargeCharacter();
        //leftCharacterImage.ShrinkCharacter();
    }

    private IEnumerator AllowNextPrompt(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (!dialogActive) yield break;

        allowNext = true;
    }
    #endregion
}
