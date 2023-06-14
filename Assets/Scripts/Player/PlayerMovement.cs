using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Scroll_Track scroll_Track;

    [Header("Setup")]

    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private WheelCollider[] wheelsR;
    [SerializeField] private WheelCollider[] wheelsL;

    [Header("Movement")]

    [SerializeField] private float maxMovementSpeed = 10f;
    [SerializeField] private float movementSpeed = 0;
    [SerializeField] private float maxTurnSpeed = 30f;
    [SerializeField] private float turnSpeed = 0f;

    private float horizontalMovement;
    private float verticalMovement;
    private bool isRotating;
    private bool isMoving;

    private Vector3 _currentMovement;

    private void Start()
    {
        playerRigidbody ??= GetComponent<Rigidbody>();

        turnSpeed = maxTurnSpeed;
        movementSpeed = maxMovementSpeed;
    }
    private void FixedUpdate()
    {
        ModifyTurnRotation();

        scroll_Track.AssignMoveTrack(RotateTexture());
    }

    private void ModifyTurnRotation()
    {
        //if (isMoving)
        //{
        //    turnSpeed = maxTurnSpeed;
        //}
        //else
        //{
        //    turnSpeed = maxTurnSpeed + maxTurnSpeed / 8;
        //}
    }

    public void OnMoveFB(InputAction.CallbackContext input)
    {
        for (int i = 0; i < wheelsR.Length; i++)
        {
            wheelsR[i].motorTorque = movementSpeed * input.ReadValue<float>();
            Debug.Log(movementSpeed);
        }

        for (int i = 0; i < wheelsL.Length; i++)
        {
            wheelsL[i].motorTorque = movementSpeed * input.ReadValue<float>();
        }
    }

    public void OnMoveRo(InputAction.CallbackContext input)
    {
        //horizontalMovement = input.ReadValue<float>();

        //if (horizontalMovement != 0)
        //{
        //    isRotating = true;
        //}
        //else
        //{
        //    isRotating = false;
        //}

    }

    private int RotateTexture()
    {

        if (verticalMovement == 1 && horizontalMovement == 0)
        {
            return 1;
        }
        if (verticalMovement == -1 && horizontalMovement == 0)
        {
            return 2;
        }
        if (verticalMovement == 1 && horizontalMovement == 1)
        {
            return 3;
        }
        if (verticalMovement == 1 && horizontalMovement == -1)
        {
            return 4;
        }
        if (verticalMovement == -1 && horizontalMovement == -1)
        {
            return 5;
        }
        if (verticalMovement == -1 && horizontalMovement == 1)
        {
            return 6;
        }
        if (verticalMovement == 0 && horizontalMovement == 1)
        {
            return 7;
        }
        if (verticalMovement == 0 && horizontalMovement == -1)
        {
            return 8;
        }

        return 0;
    }
}
