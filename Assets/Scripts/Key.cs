using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private string keyname;

    public string Keyname { get => keyname; }

    // Start is called before the first frame update
    void Start()
    {
        keyname = gameObject.name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent<Player>(out Player player))
            {
                player.SaveItemInInventory(keyname);
                gameObject.SetActive(false);
            }
        }
    }
}
