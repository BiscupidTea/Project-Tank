using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseSyestem : MonoBehaviour
{
    [SerializeField] private bool isPause;
    [SerializeField] private CanvasGroup canvasPause;

    private void Start()
    {
        isPause = false;
        Time.timeScale = 1;

        canvasPause.alpha = isPause ? 1 : 0;
        canvasPause.blocksRaycasts = isPause;
        canvasPause.interactable = isPause;
    }

    public void ChangePause(InputAction.CallbackContext input)
    {
        //TODO: Fix - I believe you can set an Interaction for press only in the input settings window 
        if (input.performed)
        {
            isPause = !isPause;
            canvasPause.alpha = isPause ? 1 : 0;
            canvasPause.blocksRaycasts = isPause;
            canvasPause.interactable = isPause;

            if (isPause)
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

    //TODO: Fix - Repeated code - I'm not sure if you're still working on these
    public void ChangePause()
    {
        isPause = !isPause;
        canvasPause.alpha = isPause ? 1 : 0;
        canvasPause.blocksRaycasts = isPause;
        canvasPause.interactable = isPause;

        if (isPause)
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

    //TODO: Fix - Should be native Setter/Getter
    public bool returnIsPaused()
    {
        return isPause;
    }
}
