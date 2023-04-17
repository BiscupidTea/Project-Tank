using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
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

    public void OnMoveFB(InputValue input)
    {
        var currentInput = input.Get<float>();

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

    public void OnMoveRo(InputValue input)
    {
        HorizontalMovement = input.Get<float>();

        if (HorizontalMovement != 0)
        {
            IsRotating = true;
        }
        else
        {
            IsRotating = false;
        }

    }
}
