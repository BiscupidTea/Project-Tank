using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private AudioClip NextSongToPlay;
    public void LoadLevel(string levelName)
    {
        SoundManager.Instance.PlayMusic(NextSongToPlay);
        SceneManager.LoadScene(levelName);
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(gameObject.scene.name);
    }
    public void ExitGame()
    {
        Application.Quit();
    } 

    public void SetNextSong(AudioClip audioClip)
    {
        NextSongToPlay = audioClip;
    }

}
