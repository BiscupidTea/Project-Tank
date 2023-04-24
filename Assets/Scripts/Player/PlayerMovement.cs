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

    [SerializeField] private float MaxMovementSpeed = 10f;
    [SerializeField] private float movementSpeed = 0;
    [SerializeField] private float MaxTurnSpeed = 30f;
    [SerializeField] private float turnSpeed = 0f;

    private float HorizontalMovement;
    private bool IsRotating;
    private float VerticalMovement;
    private bool IsMoving;

    private Vector3 initialFoward;

    private Vector3 _currentMovement;

    private void Start()
    {
        _rigidbody ??= GetComponent<Rigidbody>();

        initialFoward = transform.forward;

        turnSpeed = MaxTurnSpeed;
        movementSpeed = MaxMovementSpeed;
    }
    private void FixedUpdate()
    {
        ModifyTrayectory(movementSpeed);

        ModifyTurnRotation();

        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(Vector3.up * HorizontalMovement * turnSpeed * Time.deltaTime));

        ModifyMovementSpeed();

        _rigidbody.velocity = transform.forward * VerticalMovement * movementSpeed + Vector3.up * _rigidbody.velocity.y;

        scroll_Track.AssignMoveTrack(RotateTexture());
    }

    private void ModifyMovementSpeed()
    {
        movementSpeed = IsRotating ? MaxMovementSpeed / 2 : MaxMovementSpeed;
    }

    private void ModifyTurnRotation()
    {
        if (IsMoving)
        {
            turnSpeed = MaxTurnSpeed;
        }
        else
        {
            turnSpeed = MaxTurnSpeed + MaxTurnSpeed / 8;
        }
    }

    public void OnMoveFB(InputAction.CallbackContext input)
    {
        var currentInput = input.ReadValue<float>();

        VerticalMovement = currentInput;
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

        IsMoving = VerticalMovement != 0 ? true : false;
    }

    public void OnMoveRo(InputAction.CallbackContext input)
    {
        HorizontalMovement = input.ReadValue<float>();

        if (HorizontalMovement != 0)
        {
            IsRotating = true;
        }
        else
        {
            IsRotating = false;
        }

    }

    private int RotateTexture()
    {

        if (VerticalMovement == 1 && HorizontalMovement == 0)
        {
            return 1;
        }
        if (VerticalMovement == -1 && HorizontalMovement == 0)
        {
            return 2;
        }
        if (VerticalMovement == 1 && HorizontalMovement == 1)
        {
            return 3;
        }
        if (VerticalMovement == 1 && HorizontalMovement == -1)
        {
            return 4;
        }
        if (VerticalMovement == -1 && HorizontalMovement == -1)
        {
            return 5;
        }
        if (VerticalMovement == -1 && HorizontalMovement == 1)
        {
            return 6;
        }
        if (VerticalMovement == 0 && HorizontalMovement == 1)
        {
            return 7;
        }
        if (VerticalMovement == 0 && HorizontalMovement == -1)
        {
            return 8;
        }

        return 0;
    }
}
