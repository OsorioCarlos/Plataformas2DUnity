using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private bool addExtraHeart;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent<HealthSystem>(out HealthSystem healthSystem))
            {
                if (addExtraHeart)
                {
                    healthSystem.ModifyHealth(health);
                }
                else
                {
                    healthSystem.Healing(health);
                }
                Destroy(gameObject);
            }
        }
    }
}
