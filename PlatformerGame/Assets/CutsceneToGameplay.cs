using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneToGameplay : MonoBehaviour
{
    public float changeTime;
    public string sceneName;

    private void Update()
    {
        changeTime = changeTime - Time.deltaTime; // only to happen when changeTime is 0; a.k.a the cutscene is over

        if (changeTime <= 0)
        {
            SceneManager.LoadScene(sceneName);
        }
        
    }

}
