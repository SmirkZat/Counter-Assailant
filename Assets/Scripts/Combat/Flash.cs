using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material flashMaterial;
    [SerializeField] float flashTime = .1f;

    // if in the future there will be multiple sprite
    // replace with SpriteRenderer[] _spriteRenderers;
    // and loop over the sprites renderers
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartFlash()
    {
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        spriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(flashTime);
        SetDefaultMaterial();
    }

    void SetDefaultMaterial()
    {
        spriteRenderer.material = defaultMaterial;
    }
}
