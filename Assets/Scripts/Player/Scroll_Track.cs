using UnityEngine;

/// <summary>
/// Rotate the player wheel Texture
/// </summary>
public class Scroll_Track : MonoBehaviour
{
    [SerializeField] private Renderer right;
    [SerializeField] private Renderer left;
    [SerializeField] private float scrollSpeed = 0.05f;

    private float scrollSpeedRight = 1;
    private float scrollSpeedLeft = 1;
    private float offSetRight;
    private float offSetLeft;

    void Update()
    {
        offSetRight = +(offSetRight + Time.deltaTime * scrollSpeed * scrollSpeedRight);
        offSetLeft = +(offSetLeft + Time.deltaTime * scrollSpeed * scrollSpeedLeft);

        right.material.SetTextureOffset("_MainTex", new Vector2(offSetRight, 0f));
        left.material.SetTextureOffset("_MainTex", new Vector2(offSetLeft, 0f));
    }

    public void AssignTrackMove(PlayerMovement.WheelConfig wheelConfigRight, PlayerMovement.WheelConfig wheelConfigLeft)
    {
        scrollSpeedRight = wheelConfigRight.ReturnIntWheelMove();
        scrollSpeedLeft = wheelConfigLeft.ReturnIntWheelMove();
    }
}
