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


    public void GoToTutorial()
    {
        SceneManager.LoadScene(1);
    }

    public void Level1()
    {
        SceneManager.LoadScene(2);
    }

    public void Level2()
    {
        SceneManager.LoadScene(3);
    }

    public void LevelFinalBoss() 
    {
        SceneManager.LoadScene(4);
    }
    public void GoToExit()
    {
        Application.Quit();
    }

    

}
