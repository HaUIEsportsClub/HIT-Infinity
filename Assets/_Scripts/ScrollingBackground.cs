using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed = 0.1f;
    private Material bgMaterial;

    void Start()
    {
        bgMaterial = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        if (GameManager.Instance.gameActive == true)
        {
            Vector2 offset = bgMaterial.mainTextureOffset;
            offset.x += scrollSpeed * Time.deltaTime;
            bgMaterial.mainTextureOffset = offset;
        }

    }
}