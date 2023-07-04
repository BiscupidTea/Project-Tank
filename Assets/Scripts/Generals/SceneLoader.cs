using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]private SoundManager soundManager;
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(gameObject.scene.name);
    }
    public void ExitGame()
    {
        Application.Quit();
    } 
}
