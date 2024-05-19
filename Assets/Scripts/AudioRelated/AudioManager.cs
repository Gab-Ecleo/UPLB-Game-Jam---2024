using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;

    private EventInstance musicEventInstance;

    private EventInstance ambienceEventInstance;

    private Scene scene;
    public string sceneName;

    [Header("Master Volume")]
    [Range(0, 1)]
    public float masterVolume = 1;

    [Header("BGM Volume")]
    [Range(0, 1)]
    public float volumeBGM = 1;

    [Header("SFX Volume")]
    [Range(0, 1)]
    public float volumeSFX = 1;

    [Header("Ambiance Volume")]
    [Range(0, 1)]
    public float volumeAmbiance = 1;

    private Bus masterBus;
    private Bus BGMBus;
    private Bus SFXBus;
    private Bus ambianceBus;

    public static AudioManager instance { get; private set;}

    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

        if (instance != null)
        {
            Debug.LogError("More than one AUDIO MANAGER found in the scene!");
        }

        instance = this;

        eventInstances = new List<EventInstance>();

        masterBus = RuntimeManager.GetBus("bus:/");
        BGMBus = RuntimeManager.GetBus("bus:/Music");
        ambianceBus = RuntimeManager.GetBus("bus:/Ambiance");
        SFXBus = RuntimeManager.GetBus("bus:/SFX");

    }

    private void Start()
    {
        //InitializeMusic(FMODEvents.instance.mainMenuMusic);
        //InitializeAmbiance(FMODEvents.instance.spaceAmbience);

        Time.timeScale = 1;

        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;

        if (sceneName == "MainMenu")
        {
            StopAmbience();
            StopMusic();
            InitializeMusic(FMODEvents.instance.mainMenuMusic);
        }else if (sceneName == "GameScene")
        {
            StopAmbience();
            StopMusic();
            InitializeAmbiance(FMODEvents.instance.spaceAmbience);
            InitializeMusic(FMODEvents.instance.domeBGM);
        }else if (sceneName == "Credits")
        {
            StopAmbience();
            StopMusic();
            InitializeMusic(FMODEvents.instance.fullTheme);
        }

    }


    //To use this method:
    //AudioManager.instance.PlayOneShot(FMODEvents.instance.EventName, this.transform.position);
    //EventName found in FMODEvents Script
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public void MasterVolume(float volume)
    {
        masterBus.setVolume(volume);
        masterVolume = volume;
    }

    public void MusicVolume(float volume)
    {
        BGMBus.setVolume(volume);
        volumeBGM = volume;
    }

    public void SFXVolume(float volume)
    {
        SFXBus.setVolume(volume);
        volumeSFX = volume;
    }

    public void AmbianceVolume(float volume)
    {
        ambianceBus.setVolume(volume);
        volumeAmbiance = volume;
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public void InitializeAmbiance(EventReference ambienceEventReference)
    {
        ambienceEventInstance = CreateEventInstance(ambienceEventReference);
        ambienceEventInstance.start();
    }

    public void SetAmbianceParameter(string parameterName, float parameterValue)
    {
        ambienceEventInstance.setParameterByName(parameterName, parameterValue);
    }

    public void StopAmbience()
    {
        ambienceEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateEventInstance(musicEventReference);
        musicEventInstance.start();
    }

    public void StopMusic()
    {
        musicEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    private void CleanUp()
    {
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }

}
