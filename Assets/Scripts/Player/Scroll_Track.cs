using UnityEngine;

public class Scroll_Track : MonoBehaviour
{

    [SerializeField] private float scrollSpeed = 0.05f;
    private float scrollSpeedRight = 1;
    private float scrollSpeedLeft = 1;

    private float offsetRight;
    private float offsetLeft;

    [SerializeField] private Renderer right;
    [SerializeField] private Renderer left;
    void Update()
    {
        offsetRight = +(offsetRight + Time.deltaTime * scrollSpeed * scrollSpeedRight) % 1f;
        offsetLeft = +(offsetLeft + Time.deltaTime * scrollSpeed * scrollSpeedLeft) % 1f;

        right.material.SetTextureOffset("_MainTex", new Vector2(offsetRight, 0f));
        left.material.SetTextureOffset("_MainTex", new Vector2(offsetLeft, 0f));
    }

    public void AssignMoveTrack(PlayerMovement.WheelConfig wheelConfigRight, PlayerMovement.WheelConfig wheelConfigLeft)
    {
        if (!wheelConfigRight.forward && !wheelConfigRight.back
            || wheelConfigRight.forward && wheelConfigRight.back)
        {
            scrollSpeedRight = 0;
        }
        else if(wheelConfigRight.forward)
        {
            scrollSpeedRight = 1;
        }
        else
        {
            scrollSpeedRight = -1;
        }

        if (!wheelConfigLeft.forward && !wheelConfigLeft.back
            || wheelConfigLeft.forward && wheelConfigLeft.back)
        {
            scrollSpeedLeft = 0;
        }
        else if (wheelConfigLeft.forward)
        {
            scrollSpeedLeft = 1;
        }
        else
        {
            scrollSpeedLeft = -1;
        }
    }
}
