using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource music; // music reference
    public Slider musicVolume; // slider reference

    private void Start()
    {
        musicVolume.value = PlayerPrefs.GetFloat("MusicVolume");

    }

    private void Update()
    {
        music.volume = musicVolume.value; // Audio source is = to the slider's value; that's how we can change the music's  volume from low to high 
    }

    public void VolumePrefs()
    {
        PlayerPrefs.SetFloat("MusicVolume",musicVolume.value); // set the value of the music that choose with the slider


    }
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume.value);
    }

}
