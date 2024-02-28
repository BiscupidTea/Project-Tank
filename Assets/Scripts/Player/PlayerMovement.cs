using System;
using UnityEngine;

/// <summary>
/// Player Movment manager
/// </summary>
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

    public struct WheelConfig
    {
        public bool forward;
        public bool back;

        public WheelConfig(bool forward, bool back)
        {
            this.forward = forward;
            this.back = back;
        }

        public int ReturnIntWheelMove()
        {
            if (!forward && !back || forward && back)
            {
                return 0;
            }
            else if (forward)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }

    [SerializeField] private Scroll_Track scroll_Track;

    [Header("Setup")]

    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private WallDetectionOrigin[] collisionCheckers;

    [Header("Movement")]

    [SerializeField] private float maxSpeed;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float movementForce;
    [SerializeField] private float maxTurnSpeed;
    [SerializeField] private float turnSpeed;

    private float horizontalMovement;
    private float verticalMovement;
    private bool isRotating;

    public float CurrentSpeed { get => currentSpeed; set => currentSpeed = value; }
    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }

    private void Start()
    {
        playerRigidbody ??= GetComponent<Rigidbody>();

        CurrentSpeed = MaxSpeed;
        turnSpeed = maxTurnSpeed;
    }
    private void FixedUpdate()
    {
        float force = GetModifiedForceBasedOnRotation(movementForce * CurrentSpeed, isRotating);

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

        playerRigidbody.velocity = Vector3.ClampMagnitude(playerRigidbody.velocity, CurrentSpeed);

        AssingWheelRotation();
    }

    private float GetModifiedForceBasedOnRotation(float force, bool IsRotating)
    {
        force = isRotating ? force / 2 : force;

        return force;
    }

    public void MovePlayerForwardBack(float input)
    {
        var currentInput = input;

        float verticalRotation = transform.eulerAngles.x;

        if (verticalRotation < 80f || verticalRotation > 280f)
        {
            verticalMovement = currentInput;
        }
    }

    public void MovePlayerRightLeft(float input)
    {
        horizontalMovement = input;

        if (horizontalMovement != 0)
        {
            isRotating = true;
        }
        else
        {
            isRotating = false;
        }
    }
    /// <summary>
    /// assign the wheel rotation on the left and right
    /// </summary>
    private void AssingWheelRotation()
    {
        WheelConfig newWheelMovemntRight = new WheelConfig(false, false);
        WheelConfig newWheelMovemntLeft = new WheelConfig(false, false);

        if (verticalMovement == 1)
        {
            newWheelMovemntRight.forward = true;
            newWheelMovemntLeft.forward = true;
        }
        else if (verticalMovement == -1)
        {
            newWheelMovemntRight.back = true;
            newWheelMovemntLeft.back = true;
        }


        if (horizontalMovement == 1)
        {
            newWheelMovemntRight.back = true;
            newWheelMovemntLeft.forward = true;
        }
        else if (horizontalMovement == -1)
        {
            newWheelMovemntRight.forward = true;
            newWheelMovemntLeft.back = true;
        }

        scroll_Track.AssignTrackMove(newWheelMovemntRight, newWheelMovemntLeft);
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