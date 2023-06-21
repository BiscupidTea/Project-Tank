using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Scroll_Track scroll_Track;

    [Header("Setup")]

    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Transform rayWallLineFoward;
    [SerializeField] private Transform rayWallLineSide1;
    [SerializeField] private Transform rayWallLineSide2;

    [Header("Movement")]

    [SerializeField] private float maxSpeed;
    [SerializeField] private float movementForce;
    [SerializeField] private float maxTurnSpeed;
    [SerializeField] private float turnSpeed;

    [Header("WallInteraction")]
    [SerializeField] private float wallDistanceFoward;
    [SerializeField] private float wallDistanceSides;

    private float horizontalMovement;
    private float verticalMovement;
    private bool isRotating;
    private bool isMoving;

    private bool RayColitioningWallFoward;
    private bool RayColitioningWallSide1;
    private bool RayColitioningWallSide2;

    private void Start()
    {
        playerRigidbody ??= GetComponent<Rigidbody>();

        turnSpeed = maxTurnSpeed;
    }
    private void FixedUpdate()
    {
        ModifyTrayectory(movementForce);

        ModifyTurnRotation();

        float force = GetModifiedForceBasedOnRotation(movementForce, isRotating);

        WallLimiter();

        playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(Vector3.up * horizontalMovement * turnSpeed * Time.fixedDeltaTime));

        if (RayColitioningWallFoward && RayColitioningWallSide1 && RayColitioningWallSide2)
        {
            playerRigidbody.AddForce(transform.forward * ((verticalMovement * force)) * Time.fixedDeltaTime, ForceMode.Force);
        }

        playerRigidbody.velocity = Vector3.ClampMagnitude(playerRigidbody.velocity, maxSpeed);

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

    private void WallLimiter()
    {
        if (Physics.Raycast(rayWallLineFoward.transform.position, rayWallLineFoward.transform.forward*verticalMovement, wallDistanceFoward))
        {
            RayColitioningWallFoward = false;
        }
        else
        {
            RayColitioningWallFoward = true;
        }

        if (Physics.Raycast(rayWallLineSide1.transform.position, rayWallLineSide1.transform.forward * verticalMovement, wallDistanceSides))
        {
            RayColitioningWallSide1 = false;
        }
        else
        {
            RayColitioningWallSide1 = true;
        }

        if (Physics.Raycast(rayWallLineSide2.transform.position, rayWallLineSide2.transform.forward * verticalMovement, wallDistanceSides))
        {
            RayColitioningWallSide2 = false;
        }
        else
        {
            RayColitioningWallSide2 = true;
        }
    }

    private void OnDrawGizmos()
    {
        if (RayColitioningWallFoward)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawRay(rayWallLineFoward.transform.position, rayWallLineFoward.transform.forward * wallDistanceFoward * verticalMovement);
        
        if (RayColitioningWallSide1)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawRay(rayWallLineSide1.transform.position, rayWallLineSide1.transform.forward * wallDistanceSides * verticalMovement);

        if (RayColitioningWallSide2)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawRay(rayWallLineSide2.transform.position, rayWallLineSide2.transform.forward * wallDistanceSides * verticalMovement);
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
        GUILayout.Label($"velocity: {playerRigidbody.velocity.magnitude:F3}");
        GUILayout.EndArea();
    }
#endif
}