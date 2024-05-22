using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    //Ambiance
    [field: Header("Ambience Space")]
    [field: SerializeField] public EventReference spaceAmbience { get; private set; }

    [field: Header("Morse Code")]
    [field: SerializeField] public EventReference morseCode { get; private set; }

    //SFX
    [field: Header("Harvest SFX")]
    [field: SerializeField] public EventReference plantHarvest { get; private set; }

    [field: Header("Footsteps SFX")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }

    [field: Header("Cranking SFX")]
    [field: SerializeField] public EventReference crankingLever { get; private set; }

    [field: Header("Radio SFX")]
    [field: SerializeField] public EventReference radioStatic { get; private set; }

    [field: Header("Fixing SFX")]
    [field: SerializeField] public EventReference fixingRadio { get; private set; }

    [field: Header("Growing SFX")]
    [field: SerializeField] public EventReference plantGrow { get; private set; }

    [field: Header("PlantRaise SFX")]
    [field: SerializeField] public EventReference plantRaise { get; private set; }

    [field: Header("Button SFX")]
    [field: SerializeField] public EventReference buttonPress { get; private set; }

    [field: Header("Door Close SFX")]
    [field: SerializeField] public EventReference doorClose { get; private set; }

    [field: Header("Door Open SFX")]
    [field: SerializeField] public EventReference doorOpen { get; private set; }

    [field: Header("Expedition SFX")]
    [field: SerializeField] public EventReference dirtExpedition { get; private set; }


    //Music
    [field: Header("Dome Music")]
    [field: SerializeField] public EventReference domeBGM { get; private set; }

    [field: Header("MainMenu Music")]
    [field: SerializeField] public EventReference mainMenuMusic { get; private set; }

    [field: Header("Lose Music")]
    [field: SerializeField] public EventReference loseMusic { get; private set; }

    [field: Header("Full Theme")]
    [field: SerializeField] public EventReference fullTheme { get; private set; }

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the Scene");
        }
        instance = this;
    }

}
