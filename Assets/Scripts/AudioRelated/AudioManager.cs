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
