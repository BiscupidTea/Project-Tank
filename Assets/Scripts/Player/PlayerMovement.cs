using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Scroll_Track scroll_Track;

    [Header("Setup")]

    [SerializeField] private Rigidbody _rigidbody;

    [Header("Movement")]

    [SerializeField] private float maxSpeed;
    [SerializeField] private float movementForce;
    [SerializeField] private float maxTurnSpeed;
    [SerializeField] private float turnSpeed;

    private float horizontalMovement;
    private float verticalMovement;
    private bool isRotating;
    private bool isMoving;

    private void Start()
    {
        _rigidbody ??= GetComponent<Rigidbody>();

        turnSpeed = maxTurnSpeed;
    }
    private void FixedUpdate()
    {
        ModifyTrayectory(movementForce);

        ModifyTurnRotation();

        float force = GetModifiedForceBasedOnRotation(movementForce, isRotating);

        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(Vector3.up * horizontalMovement * turnSpeed * Time.fixedDeltaTime));

        _rigidbody.AddForce(transform.forward * ((verticalMovement * force)) * Time.fixedDeltaTime, ForceMode.Force);

        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxSpeed);

        scroll_Track.AssignMoveTrack(RotateTexture());
    }

    private float GetModifiedForceBasedOnRotation(float force, bool IsRotating)
    {
        force = isRotating ? force / 2 : force;

        return force;
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

    private void ModifyTrayectory(float currentInput)
    {
        isMoving = verticalMovement != 0 ? true : false;
    }

    public void OnMoveFB(InputAction.CallbackContext input)
    {
        var currentInput = input.ReadValue<float>();

        float verticalRotation = transform.eulerAngles.x;

        if (verticalRotation < 80f || verticalRotation > 280f)
        {
            verticalMovement = currentInput;
        }
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

#if UNITY_EDITOR
    private void OnGUI()
    {
        Rect rect = new Rect(10, 400, 250, 550);
        GUILayout.BeginArea(rect);
        GUI.skin.label.fontSize = 35;
        GUI.skin.label.normal.textColor = Color.white;
        GUILayout.Label($"velocity: {_rigidbody.velocity.magnitude:F3}");
        GUILayout.EndArea();
    }
#endif
}