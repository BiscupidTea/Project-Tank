using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private CanvasGroup[] tutorialCanvas;
    private CanvasGroup currentCanvas;
    private int currentCanvasInt = 0;

    public CanvasGroup CurrentCanvas { get => currentCanvas; set => currentCanvas = value; }

    private void Start()
    {
        playerController.continueTutorial.AddListener(NextTextTutorial);

        for (int i = 0; i < tutorialCanvas.Length; i++)
        {
            DisableCanvas(tutorialCanvas[i]);
        }

        currentCanvas = tutorialCanvas[currentCanvasInt];
        EnableCanvas(tutorialCanvas[currentCanvasInt]);
    }

    private void NextTextTutorial(GameObject gameObject)
    {
        currentCanvasInt++;
        if (currentCanvasInt < tutorialCanvas.Length)
        {
            SwitchCanvas(tutorialCanvas[currentCanvasInt], currentCanvas);
        }
        else
        {
            DisableCanvas(currentCanvas);
        }
    }

    private void SwitchCanvas(CanvasGroup canvasEnable, CanvasGroup canvasDisable)
    {
        EnableCanvas(canvasEnable);
        DisableCanvas(canvasDisable);
        CurrentCanvas = canvasEnable;
        Debug.Log("Change canvas form: " + canvasDisable + " to " + canvasEnable);
    }

    public void DisableCanvas(CanvasGroup canvas)
    {
        canvas.alpha = 0;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }

    public void EnableCanvas(CanvasGroup canvas)
    {
        canvas.alpha = 1;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
    }
}
