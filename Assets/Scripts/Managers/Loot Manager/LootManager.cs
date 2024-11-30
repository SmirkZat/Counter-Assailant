using UnityEngine;

public class LootManager : MonoBehaviour
{
    [SerializeField] Transform collectiblesParentTransform;
    Transform playerTransform;

    void Start()
    {
        playerTransform = PlayerController.Instance.transform;
    }

    void OnEnable()
    {
        LevelManager.OnLevelCompleted += HandleLootLevelComplete;
    }

    void OnDisable()
    {
        LevelManager.OnLevelCompleted -= HandleLootLevelComplete;
    }

    void HandleLootLevelComplete()
    {
        if (playerTransform != null)
        {
            foreach (Transform collectibleTransform in collectiblesParentTransform)
            {
                ICollectible collectible = collectibleTransform.GetComponent<ICollectible>();
                collectible.MagnetToTarget(playerTransform);
            }
        }
    }
}
