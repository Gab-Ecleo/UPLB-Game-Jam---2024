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
    [SerializeField] private int RadioState = 1; //1 - 2 Broken, 3 - 4 Semi-Fixed, 5-6 Fixed;

    [SerializeField] private TMP_Text text;

    private void Start()
    {
        RadioState = 0;
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
            //Play Final Cutscene
            SceneManagerScript.Instance.LoadScene("EndingCutScene");
        }
        else if (RadioState < 6)
        {
            Debug.Log("Playing Radio Dialogue");
            //play dialgoue based on radio state
        }
        
        text.text = RadioFixProgress;
        GameManager.Instance.CanFixRadio = false;
    }

    private void SwapSprite()
    {
        switch (RadioState)
        {
            case 1:
                RadioFixProgress = "16.67";
                break;
            case 2:
                _spriteRenderer.sprite = brokenRadio;
                RadioFixProgress = "32.27";
                break;
            case 3:
                RadioFixProgress = "49.94";
                break;
            case 4:
                _spriteRenderer.sprite = patchedRadio;
                RadioFixProgress = "60.61";
                break;
            case 5:
                RadioFixProgress = "83.28";
                break;
            case 6:
                _spriteRenderer.sprite = fixedRadio;
                RadioFixProgress = "100";
                break;
        }
    } 
}