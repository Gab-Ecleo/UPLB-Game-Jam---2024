using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Ambience Space")]
    [field: SerializeField] public EventReference spaceAmbience { get; private set; }

    [field: Header("Harvest SFX")]
    [field: SerializeField] public EventReference plantHarvest { get; private set; }

    [field: Header("Footsteps SFX")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }

    [field: Header("Dome Music")]
    [field: SerializeField] public EventReference domeBGM { get; private set; }

    [field: Header("MainMenu Music")]
    [field: SerializeField] public EventReference mainMenuMusic { get; private set; }

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
