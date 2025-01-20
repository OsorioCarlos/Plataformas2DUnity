using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockableBlock : MonoBehaviour
{
    [SerializeField] private Key key;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent<Player>(out Player player))
            {
                if (player.ExistsItemInInventory(key.Keyname))
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
