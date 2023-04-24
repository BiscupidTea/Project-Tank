using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scroll_Track : MonoBehaviour {

    [SerializeField] private float scrollSpeed = 0.05f;
    private float scrollSpeedRight = 1;
    private float scrollSpeedLeft = 1;

    private float offsetRight;
    private float offsetLeft;

    [SerializeField] private Renderer right;
    [SerializeField] private Renderer left;
    void Update()
    {
         offsetRight =+ (offsetRight + Time.deltaTime * scrollSpeed * scrollSpeedRight) % 1f;
         offsetLeft =+ (offsetLeft  + Time.deltaTime * scrollSpeed * scrollSpeedLeft) % 1f;

        right.material.SetTextureOffset("_MainTex", new Vector2(offsetRight, 0f));
        left.material.SetTextureOffset("_MainTex", new Vector2(offsetLeft, 0f));
    }

    public void AssignMoveTrack(int trackPosition)
    {
        switch (trackPosition) 
        {
            //foward
            case 1:
                scrollSpeedRight = 1;
                scrollSpeedLeft = 1;
                break;

            //backward
            case 2:
                scrollSpeedRight = -1;
                scrollSpeedLeft = -1;
                break;

            //foward right
            case 3:
                scrollSpeedRight = 0;
                scrollSpeedLeft = 1;
                break;

            //foward left
            case 4:
                scrollSpeedRight = 1;
                scrollSpeedLeft = 0;
                break;

            //backward right
            case 5:
                scrollSpeedRight = 0;
                scrollSpeedLeft = -1;
                break;

            //backward left
            case 6:
                scrollSpeedRight = -1;
                scrollSpeedLeft = 0;
                break;

            //rotate right
            case 7:
                scrollSpeedRight = -1;
                scrollSpeedLeft = 1;
                break;

            //rotate left
            case 8:
                scrollSpeedRight = 1;
                scrollSpeedLeft = -1;
                break;
            //default case
            default:
                scrollSpeedRight = 0;
                scrollSpeedLeft = 0;
                break;
        }
    }
    
}
