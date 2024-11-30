using UnityEngine;

public class Collector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        ICollectible collectable = other.GetComponent<ICollectible>();
        if (collectable != null)
        {
            collectable.Collect();
        }
    }
}
