using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Serializable]
    private struct WallDetectionOrigin
    {
        [SerializeField] private Transform origin;
        [SerializeField] private float distance;

        public Transform Origin { get => origin; set => origin = value; }
        public float Distance { get => distance; set => distance = value; }

        public bool IsColliding(float directionMultiplier)
        {
            return origin != null && Physics.Raycast(Origin.position, Origin.forward * directionMultiplier, Distance);
        }
    }

    [SerializeField] private Scroll_Track scroll_Track;

    [Header("Setup")]

    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private WallDetectionOrigin[] collisionCheckers;

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
        playerRigidbody ??= GetComponent<Rigidbody>();

        turnSpeed = maxTurnSpeed;
    }
    private void FixedUpdate()
    {
        ModifyTrayectory(movementForce);

        ModifyTurnRotation();

        float force = GetModifiedForceBasedOnRotation(movementForce, isRotating);

        playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(Vector3.up * horizontalMovement * turnSpeed * Time.fixedDeltaTime));

        bool isColidingToWalls = false;

        foreach (var ray in collisionCheckers)
        {
            if (ray.IsColliding(verticalMovement))
            {
                isColidingToWalls = true;
            }
        }

        if (!isColidingToWalls)
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

    private void OnDrawGizmos()
    {
        for (int i = 0; i < collisionCheckers.Length; i++)
        {
            if (collisionCheckers[i].Origin == null)
            {
                continue;
            }

            if (collisionCheckers[i].IsColliding(verticalMovement))
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.green;
            }
            Gizmos.DrawRay(collisionCheckers[i].Origin.position, collisionCheckers[i].Origin.forward * collisionCheckers[i].Distance * verticalMovement);
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
        GUILayout.Label($"velocity: {playerRigidbody.velocity.magnitude:F3}");
        GUILayout.EndArea();
    }
#endif
}