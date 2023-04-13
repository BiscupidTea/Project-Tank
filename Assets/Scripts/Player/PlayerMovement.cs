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
    private float VerticalMovement;

    private Vector3 initialFoward;

    private Vector3 _currentMovement;

    private void Start()
    {
        _rigidbody ??= GetComponent<Rigidbody>();
        initialFoward = transform.forward;
        turnSpeed = MaxTurnSpeed;
    }
    private void FixedUpdate()
    {
        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(Vector3.up * HorizontalMovement * turnSpeed * Time.deltaTime));

        _rigidbody.velocity = transform.forward * VerticalMovement * movementSpeed + Vector3.up * _rigidbody.velocity.y;
    }

    public void OnMoveFB(InputValue input)
    {
        var currentInput = input.Get<float>();

        VerticalMovement = currentInput;

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

    }

    public void OnMoveRo(InputValue input)
    {
        HorizontalMovement = input.Get<float>();
    }
}
