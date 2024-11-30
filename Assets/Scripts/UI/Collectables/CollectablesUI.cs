using TMPro;
using UnityEngine;

public class CollectablesUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] TextMeshProUGUI gemText;

    InventoryManager inventory;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            inventory = GameManager.Instance.Inventory;
        }
    }

    void Update()
    {
        if (inventory != null)
        {
            coinsText.text = inventory.Coins.ToString();
            gemText.text = inventory.Gems.ToString();
        }
    }
}
