using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Sprite redKeySprite;
    [SerializeField] private Sprite blueKeySprite;
    [SerializeField] private Sprite greenKeySprite;
    [SerializeField] private Sprite yellowKeySprite;

    private List<GameObject> inventoryImages = new List<GameObject>();

    public void InitializeInventory()
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
        inventoryImages.Clear();
    }

    public void AddItem(string name)
    {
        GameObject heart = Instantiate(itemPrefab, gameObject.transform);
        Image heartImage = heart.GetComponentInChildren<Image>();
        if (name == "Red Key")
        {
            heartImage.sprite = redKeySprite;
        }
        else if (name == "Blue Key")
        {
            heartImage.sprite = blueKeySprite;
        } 
        else if (name == "Green Key")
        {
            heartImage.sprite = greenKeySprite;
        } 
        else if (name == "Yellow Key")
        {
            heartImage.sprite = yellowKeySprite;
        }
        inventoryImages.Add(heart);
    }
}
