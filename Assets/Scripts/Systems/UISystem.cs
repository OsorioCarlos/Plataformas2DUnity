using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISystem : MonoBehaviour
{
    [SerializeField] private GameObject UICanvas;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject levelCompleteUI;

    private HealthUI healthUI;
    private InventoryUI inventoryUI;

    // Start is called before the first frame update
    void Awake()
    {
        healthUI = UICanvas.GetComponentInChildren<HealthUI>();
        inventoryUI = UICanvas.GetComponentInChildren<InventoryUI>();
    }

    public void InitializeHealthUI(int maxHealth)
    {
        if (healthUI != null)
        {
            healthUI.InitializeHearts(maxHealth);
        }
    }

    public void UpdateHealthUI(int hearts)
    {
        if (healthUI != null)
        {
            healthUI.UpdateHeartsUI(hearts);
        }
    }

    public void InitializeInventoryUI()
    {
        if (inventoryUI != null)
        {
            inventoryUI.InitializeInventory();
        }
    }

    public void UpdateInventoryUI(string item)
    {
        if (inventoryUI != null)
        {
            inventoryUI.AddItem(item);
        }
    }

    public void ShowLevelCompleteUI()
    {
        levelCompleteUI.SetActive(true);
    }

    public void ShowGameOverUI()
    {
        gameOverUI.SetActive(true);
    }
}
