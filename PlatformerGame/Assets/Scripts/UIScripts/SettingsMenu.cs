using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

   
    
    private float volume;

    private void Start()
    {
        

    }

    private void Update()
    {
        

    }

    /*public void SaveData()
    {
        PlayerPrefs.SetFloat("volume", MusicVolume);
  


    }

    public void SetVolume (float volume) // takes a value from the slider type float
    {
      audioMixer.SetFloat("volume", volume) ; // we want to set the float "volume" equal to volume from audio mixer
       

    }
    */

    

    public void SetQuality (int qualityIndex) // qualityIndex is an integer type variable because we take the Index of the quality in Unity (0,1,2 etc)
    {
        QualitySettings.SetQualityLevel (qualityIndex); // we change the quality based on the qualityIndex
        PlayerPrefs.SetInt("quality", qualityIndex);  // save the quality in memory
        
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        
    }



}
