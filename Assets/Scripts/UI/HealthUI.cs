using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private Sprite halfHeartSprite;
    [SerializeField] private Sprite emptyHeartSprite;

    private List<GameObject> heartImages = new List<GameObject>();

    public void InitializeHearts(int maxHealth)
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
        heartImages.Clear();

        AddHearts(Mathf.CeilToInt(maxHealth / 2f));
    }

    public void UpdateHeartsUI(int currentHealth)
    {
        if (currentHealth > (heartImages.Count * 2))
        {
            AddHearts(Mathf.CeilToInt((currentHealth - (heartImages.Count * 2)) / 2f));
        }

        int remainingHealth = currentHealth;

        for (int i = 0; i < heartImages.Count; i++)
        {
            Image heartImage = heartImages[i].GetComponentInChildren<Image>();

            if (remainingHealth >= 2)
            {
                heartImage.sprite = fullHeartSprite;
                remainingHealth -= 2;
            }
            else if (remainingHealth == 1)
            {
                heartImage.sprite = halfHeartSprite;
                remainingHealth -= 1;
            }
            else
            {
                heartImage.sprite = emptyHeartSprite;
            }
        }
    }

    private void AddHearts(int totalHearts)
    {
        for (int i = 0; i < totalHearts; i++)
        {
            GameObject heart = Instantiate(heartPrefab, gameObject.transform);
            heartImages.Add(heart);
        }
    }
}
