using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Menu Canvas switcher
/// </summary>
public class UiChangeButton : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private CanvasGroup canvasMenu;
    [SerializeField] private CanvasGroup canvasCredits;
    [SerializeField] private CanvasGroup canvasOptions;
    [SerializeField] private CanvasGroup currentCanvas;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private SoundManager soundManager;

    private void Awake()
    {
        canvasMenu.alpha = 0;
        canvasCredits.alpha = 0;
        canvasOptions.alpha = 0;
        currentCanvas.alpha = 1;

        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", musicSlider.maxValue / 2);
        }
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        soundManager.ChangeVolumeMusicSlider(musicSlider.value);

        if (!PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.SetFloat("SFXVolume", SFXSlider.maxValue / 2);
        }
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        soundManager.ChangeVolumeSFXSlider(SFXSlider.value);
        PlayerPrefs.Save();
    }
    public void SwitchCanvas(CanvasGroup canvasEnable, CanvasGroup canvasDisable)
    {
        canvasEnable.alpha = 1;
        canvasEnable.interactable = true;
        canvasEnable.blocksRaycasts = true;

        canvasDisable.alpha = 0;
        canvasDisable.interactable = false;
        canvasDisable.blocksRaycasts = false;

        currentCanvas = canvasEnable;
    }

    public void SetFirstButton(GameObject firstButton)
    {
        eventSystem.SetSelectedGameObject(firstButton);
    }

    public void GoToMenu()
    {
        SwitchCanvas(canvasMenu, currentCanvas);
    }

    public void GoToCredits()
    {
        SwitchCanvas(canvasCredits, currentCanvas);
    }

    public void GoToOptions()
    {
        SwitchCanvas(canvasOptions, currentCanvas);
    }
}
