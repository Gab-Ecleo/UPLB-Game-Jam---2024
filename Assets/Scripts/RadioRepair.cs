using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RadioRepair : MonoBehaviour, IInteractable
{
    [Header("Sprites")]
    [SerializeField] private Sprite brokenRadio;
    [SerializeField] private Sprite patchedRadio;
    [SerializeField] private Sprite fixedRadio;

    private SpriteRenderer _spriteRenderer;
    private string RadioFixProgress;
    public int RadioState = 1; //1 - 2 Broken, 3 - 4 Semi-Fixed, 5-6 Fixed;

    [Header("UI Assets")]
    [SerializeField] private Dialog_PlayerDetection radioDialogue;
    [SerializeField] private TMP_Text radioProgressText;
    
    [SerializeField] private Dialog_Instance[] dialogueBank;
    

    private void Start()
    {
        RadioState = 0;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        radioDialogue.dialog = dialogueBank[0];
    }

    public void Interact()
    {
        if (GameManager.Instance.CanFixRadio)
            RepairRadio();
    }
    
    private void RepairRadio()
    {
        //Display dialog
        AudioManager.instance.PlayOneShot(FMODEvents.instance.radioStatic, transform.position);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.fixingRadio, transform.position);
        Debug.Log("Fixing Radio");

        RadioState++;
        SwapSprite();
        
        //Display dialog
        if (RadioState > 5)
        {
            Debug.Log("Game End");
            SceneManagerScript.Instance.LoadScene("EndingCutScene");
        }
        else if (RadioState < 6)
        {
            Debug.Log("Playing Radio Dialogue");
        }
        
        radioProgressText.text = RadioFixProgress;
        GameManager.Instance.CanFixRadio = false;
    }

    private void SwapSprite()
    {
        switch (RadioState)
        {
            case 1:
                RadioFixProgress = "16.67";
                radioDialogue.dialog = dialogueBank[1];
                break;
            case 2:
                _spriteRenderer.sprite = brokenRadio;
                RadioFixProgress = "32.27";
                radioDialogue.dialog = dialogueBank[2];
                break;
            case 3:
                RadioFixProgress = "49.94";
                radioDialogue.dialog = dialogueBank[3];
                break;
            case 4:
                _spriteRenderer.sprite = patchedRadio;
                RadioFixProgress = "60.61";
                radioDialogue.dialog = dialogueBank[4];
                break;
            case 5:
                RadioFixProgress = "83.28";
                radioDialogue.dialog = dialogueBank[5];
                break;
            case 6:
                _spriteRenderer.sprite = fixedRadio;
                RadioFixProgress = "100";
                break;
        }
    } 
}