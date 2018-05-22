using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour {

    [SerializeField]
    Slider sfxVolumeSlider;

    [SerializeField]
    Slider musicVolumeSlider;

    [SerializeField]
    AudioMixerGroup masterSFXVolume;

    [SerializeField]
    float minimumSFXDB;

    [SerializeField]
    float maximumSFXDB;

    private float sfxVolume;

    [SerializeField]
    MusicManager musicManager; // Can I get this from the Director manager? Let's find out at some point!

    private float musicVolume;

    

    private void Start()
    {
        sfxVolumeSlider.maxValue = maximumSFXDB;
        sfxVolumeSlider.minValue = minimumSFXDB;
        sfxVolumeSlider.value = maximumSFXDB;
        sfxVolume = sfxVolumeSlider.value;

        musicVolumeSlider.maxValue = musicManager.MaxMusicVolume;
        musicVolumeSlider.minValue = 0;
        musicVolumeSlider.value = musicManager.MaxMusicVolume;
        musicVolume = musicVolumeSlider.value;        
    }

    private void Update()
    {
        if (this.isActiveAndEnabled)
        { 
            if (sfxVolume != sfxVolumeSlider.value)
            {
                sfxVolume = sfxVolumeSlider.value;
                masterSFXVolume.audioMixer.SetFloat("MasterSFXVolume", sfxVolume);    
            }

            else if (musicVolume != musicVolumeSlider.value)
            {
                musicVolume = musicVolumeSlider.value;
                musicManager.setMusicVolume(musicVolume);
            }
        }
    }

}
