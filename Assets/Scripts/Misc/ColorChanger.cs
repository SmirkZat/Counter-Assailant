using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Color DefaultColor { get; private set; }

    SpriteRenderer fillSpriteRenderer;

    void Awake()
    {
        fillSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetDefaultColor(Color color)
    {
        DefaultColor = color;
        SetColor(color);
    }

    public void SetColor(Color color)
    {
        fillSpriteRenderer.color = color;
    }
}
