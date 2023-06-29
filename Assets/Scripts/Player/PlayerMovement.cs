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

    public struct WheelConfig
    {
        public bool foward;
        public bool back;

        public WheelConfig(bool foward, bool back)
        {
            this.foward = foward;
            this.back = back;
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

        //TODO: TP2 - SOLID
        RotateTexture();
    }

    //TODO: TP2 - Remove unused parameters
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

    //TODO: TP2 - Remove unused parameters
    private void ModifyTrayectory(float currentInput)
    {
        isMoving = verticalMovement != 0 ? true : false;
    }

    public void MovePlayerFB(float input)
    {
        var currentInput = input;

        float verticalRotation = transform.eulerAngles.x;

        if (verticalRotation < 80f || verticalRotation > 280f)
        {
            verticalMovement = currentInput;
        }
    }

    //TODO: Fix - Unclear name
    public void MovePlayerRL(float input)
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

    private void RotateTexture()
    {
        WheelConfig newWheelMovemntRight = new WheelConfig(false, false);
        WheelConfig newWheelMovemntLeft = new WheelConfig(false, false);

        Debug.Log(verticalMovement);

        if (verticalMovement == 1)
        {
            newWheelMovemntRight.foward = true;
            newWheelMovemntLeft.foward = true;
        }
        else if (verticalMovement == -1)
        {
            newWheelMovemntRight.back = true;
            newWheelMovemntLeft.back = true;
        }


        if (horizontalMovement == 1)
        {
            newWheelMovemntRight.back = true;
            newWheelMovemntLeft.foward = true;
        }
        else if(horizontalMovement == -1)
        {
            newWheelMovemntRight.foward = true;
            newWheelMovemntLeft.back = true;
        }

        scroll_Track.AssignMoveTrack(newWheelMovemntRight, newWheelMovemntLeft);
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