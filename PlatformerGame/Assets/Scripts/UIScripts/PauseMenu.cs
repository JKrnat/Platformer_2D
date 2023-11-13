using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool PlayerIsDead = false;    
  
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject deathMenuUI;
    [SerializeField] private PlayerCont playerCont; // reference to the PlayerCont.cs script so we are able to use bool isDying


    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape) && playerCont.isDying == false)  // isDying = bool for death because we don't want to be able to pause/resume the game while the IEnumerator for RestartLevel() is iterating
        {
            if (GameIsPaused)
            {
                Resume();

            }
            else
            {
                Pause();

            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if(playerCont.isDying == true)
            {
                RestartGame();
            }
           
        }
        
            


    }

    
    


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        Time.timeScale = 1f; // unfreeze game
        GameIsPaused = false;

    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // freeze game
        GameIsPaused = true;
        
    }

    public void StartScreen()
    {
        Time.timeScale = 1f; // we resume time because we don't want the game to be frozen in menu
        SceneManager.LoadScene("StartScreen");
    }   
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
