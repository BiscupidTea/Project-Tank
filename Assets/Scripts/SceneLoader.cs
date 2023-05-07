using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToShootField()
    {
        SceneManager.LoadScene(2);
    }

    public void GoToTutorial()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToOprions()
    {
        
    }

    public void GoToCredits()
    {
        
    }
    public void GoToExit()
    {
        Application.Quit();
    }

}
