using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            string activeScene = SceneManager.GetActiveScene().name; // get the name of the scene we are currently in
            PlayerPrefs.SetString("LevelSaved", activeScene); // PlayerPrefs is a class that stores Player preferences between game sessions; "LevelSaved" is the name of the string that the scene is saved
                                                              // It can store string, float and integer values into the user’s platform registry. 
                                                              // Unity stores PlayerPrefs in a local registry, without encryption. Do not use PlayerPrefs data to store sensitive data.
                                                              // - Windows: HKCU\Software\ExampleCompanyName\ExampleProductName
                                                              // - Windows Universal Platform: %userprofile%\AppData\Local\Packages\[ProductPackageId]\LocalState\playerprefs.dat
            Debug.Log("Saved");
        }
    }





}
