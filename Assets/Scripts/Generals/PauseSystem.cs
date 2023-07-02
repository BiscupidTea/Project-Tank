using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasPause;
    [SerializeField] private bool isPause;

    public bool IsPause { get => isPause; set => isPause = value; }

    private void Start()
    {
        IsPause = false;
        Time.timeScale = 1;

        canvasPause.alpha = IsPause ? 1 : 0;
        canvasPause.blocksRaycasts = IsPause;
        canvasPause.interactable = IsPause;
    }

    public void SwitchPause()
    {
        IsPause = !IsPause;
        canvasPause.alpha = IsPause ? 1 : 0;
        canvasPause.blocksRaycasts = IsPause;
        canvasPause.interactable = IsPause;

        if (IsPause)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }

    }
}
