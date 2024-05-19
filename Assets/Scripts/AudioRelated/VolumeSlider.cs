using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider masterSlider, musicSlider, ambianceSlider, SFXSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Master_Vol"))
        {
            loadVolume();
        }
        else
        {
            MasterVolumeController();
            MusicVolumeController();
            AmbianceVolumeController();
            SFXVolumeController();
        }
    }

    public void MasterVolumeController()
    {
        float masVol = masterSlider.value;
        AudioManager.instance.MasterVolume(masVol);
        PlayerPrefs.SetFloat("Master_Vol", masVol);
    }

    public void MusicVolumeController()
    {
        float BGMVol = musicSlider.value;
        AudioManager.instance.MusicVolume(BGMVol);
        PlayerPrefs.SetFloat("BGM_Vol", BGMVol);
    }

    public void AmbianceVolumeController()
    {
        float ambiVol = ambianceSlider.value;
        AudioManager.instance.AmbianceVolume(ambiVol);
        PlayerPrefs.SetFloat("Ambi_Vol", ambiVol);
    }

    public void SFXVolumeController()
    {
        float SFXVol = SFXSlider.value;
        AudioManager.instance.SFXVolume(SFXVol);
        PlayerPrefs.SetFloat("SFX_Vol", SFXVol);
    }

    public void loadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("Master_Vol");
        musicSlider.value = PlayerPrefs.GetFloat("BGM_Vol");
        ambianceSlider.value = PlayerPrefs.GetFloat("Ambi_Vol");
        SFXSlider.value = PlayerPrefs.GetFloat("SFX_Vol");

        MasterVolumeController();
        MusicVolumeController();
        AmbianceVolumeController();
        SFXVolumeController();

    }

}
