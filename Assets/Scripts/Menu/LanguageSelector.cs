using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanguageSelector : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.HasKey("Language"))
        {
            SceneManager.LoadScene(2);
        }
    }
    public void SpanishLanguageSelected()
    {
        PlayerPrefs.SetInt("Language", 1);
        SceneManager.LoadScene(2);
    }

    public void EnglishLanguageSelected()
    {
        PlayerPrefs.SetInt("Language", 2);
        SceneManager.LoadScene(2);
    }

    public void ItalianoLanguageSelected()
    {
        PlayerPrefs.SetInt("Language", 3);
        SceneManager.LoadScene(2);
    }
}
