using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private int damagePerTick;
    [SerializeField] private float damageInterval;

    private bool isPlayerInZone = false;
    private Coroutine damageCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInZone = true;

            // Iniciar la corrutina de daño si no está activa
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(ApplyDamage(collision.gameObject));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInZone = false;

            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator ApplyDamage(GameObject player)
    {
        HealthSystem playerHealth = player.GetComponent<HealthSystem>();

        while (isPlayerInZone)
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damagePerTick);
            }

            yield return new WaitForSeconds(damageInterval);
        }
    }
}
