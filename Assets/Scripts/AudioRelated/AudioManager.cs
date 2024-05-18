using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;

    private EventInstance musicEventInstance;

    private EventInstance ambienceEventInstance;

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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        eventInstances = new List<EventInstance>();

        masterBus = RuntimeManager.GetBus("bus:/");
        BGMBus = RuntimeManager.GetBus("bus:/Music");
        SFXBus = RuntimeManager.GetBus("bus:/Ambiance");
        ambianceBus = RuntimeManager.GetBus("bus:/SFX");

    }

    private void Start()
    {
        InitializeMusic(FMODEvents.instance.domeBGM);
        InitializeAmbiance(FMODEvents.instance.spaceAmbience);
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
