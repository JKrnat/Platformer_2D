using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //public AudioSource music;

    private void Start()
    {
        //music.volume = PlayerPrefs.GetFloat("MusicVolume"); // gets the volume of audio that we set with the slider Settings with AudioManager script
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("LevelSaved"))
        {
            string levelToLoad = PlayerPrefs.GetString("LevelSaved"); // fills the local variable "levelToLoad" with LevelSaved
            SceneManager.LoadScene(levelToLoad); // loads the scene using the local variable with the scene
            
        }

        else
        {
            NewGame();
        }
    }
    
    public void NewGame()
    {
        SceneManager.LoadScene("Cutscene"); 
    }

    /*public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // load the next level in the queue (in the index from Build in Unity; Files > Build Settings > order the scenes in the index)
    }*/

    public void QuitGame()
    {
        Application.Quit();
    }    
}
