using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerShoot playerShoot;
    [SerializeField] private PlayerMovement PlayerMovemnt;

    public void ShootInput(InputAction.CallbackContext input)
    {
        playerShoot.ShootWeapon();
    }

    public void ChangeWeapon(InputAction.CallbackContext input)
    {
        if (input.performed)
        {
            playerShoot.SwitchToNextWeapon();
        }
    }

    public void OnMoveFB(InputAction.CallbackContext input)
    {
        float ActualInput = input.ReadValue<float>();
        PlayerMovemnt.MovePlayerForwardBack(ActualInput);
    }

    public void OnMoveRo(InputAction.CallbackContext input)
    {
        float ActualInput = input.ReadValue<float>();
        PlayerMovemnt.MovePlayerRightLeft(ActualInput);
    }
}
