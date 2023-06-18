using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Scroll_Track scroll_Track;

    [Header("Setup")]

    [SerializeField] private Rigidbody _rigidbody;

    [Header("Movement")]

    [SerializeField] private float maxMovementSpeed = 10f;
    [SerializeField] private float movementSpeed = 0;
    [SerializeField] private float maxTurnSpeed = 30f;
    [SerializeField] private float turnSpeed = 0f;

    private float horizontalMovement;
    private float verticalMovement;
    private bool isRotating;
    private bool isMoving;

    private Vector3 initialFoward;

    private Vector3 _currentMovement;

    private void Start()
    {
        _rigidbody ??= GetComponent<Rigidbody>();

        initialFoward = transform.forward;

        turnSpeed = maxTurnSpeed;
        movementSpeed = maxMovementSpeed;
    }
    private void FixedUpdate()
    {
        ModifyTrayectory(movementSpeed);

        ModifyTurnRotation();

        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(Vector3.up * horizontalMovement * turnSpeed * Time.deltaTime));

        ModifyMovementSpeed();

        _rigidbody.AddForce(transform.forward * (verticalMovement * movementSpeed), ForceMode.Force);

        scroll_Track.AssignMoveTrack(RotateTexture());
    }

    private void ModifyMovementSpeed()
    {
        movementSpeed = isRotating ? maxMovementSpeed / 2 : maxMovementSpeed;
    }

    private void ModifyTurnRotation()
    {
        if (isMoving)
        {
            turnSpeed = maxTurnSpeed;
        }
        else
        {
            turnSpeed = maxTurnSpeed + maxTurnSpeed / 8;
        }
    }

    public void OnMoveFB(InputAction.CallbackContext input)
    {
        var currentInput = input.ReadValue<float>();

        verticalMovement = currentInput;
    }

    private void ModifyTrayectory(float currentInput)
    {
        if (currentInput > 0)
        {
            _currentMovement = transform.forward;
        }
        else if (currentInput < 0)
        {
            _currentMovement = -transform.forward;
        }
        else
        {
            _currentMovement = Vector3.zero;
        }

        isMoving = verticalMovement != 0 ? true : false;
    }

    public void OnMoveRo(InputAction.CallbackContext input)
    {
        horizontalMovement = input.ReadValue<float>();

        if (horizontalMovement != 0)
        {
            isRotating = true;
        }
        else
        {
            isRotating = false;
        }

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