using UnityEngine;

/// <summary>
/// Rotate the Enemy wheel Texture
/// </summary>
public class Scroll_TrackEnemy : MonoBehaviour {

    [SerializeField]
    private float scrollSpeed = 0.05f;

    private float offset = 0.0f;
    private Renderer r;

    void Start()
    {
        r = GetComponent<Renderer>();
    }

    void Update()
    {
        offset = (offset + Time.deltaTime * scrollSpeed) % 1f;
        r.material.SetTextureOffset("_MainTex", new Vector2(offset, 0f));
    }
}
