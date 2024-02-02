using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// Pause manager
/// </summary>
public class PauseSystem : MonoBehaviour
{
    [SerializeField] public UnityEvent<GameObject> onPause;
    [SerializeField] private CanvasGroup canvasPause;
    [SerializeField] private GameObject firstButtonPause;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private bool isPause;

    public bool IsPause { get => isPause; set => isPause = value; }

    private void Start()
    {
        IsPause = false;
        Time.timeScale = 1;
    }
    /// <summary>
    /// Change the pause state, change the time scale and show/hide cursor
    /// </summary>
    public void SwitchPause()
    {
        isPause = !isPause;
        if (IsPause)
        {
            eventSystem.SetSelectedGameObject(firstButtonPause);
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
        onPause.Invoke(this.gameObject);
    }
}
