using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiChangeButton : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private CanvasGroup canvasMenu;
    [SerializeField] private CanvasGroup canvasCredits;
    [SerializeField] private CanvasGroup canvasOptions;
    [SerializeField] private CanvasGroup currentCanvas;

    private void Awake()
    {
        canvasMenu.alpha = 0;
        canvasCredits.alpha = 0;
        canvasOptions.alpha = 0;
        currentCanvas.alpha = 1;
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
        Debug.Log(eventSystem.currentSelectedGameObject); 
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
