using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public PlayerStats playerStats;

    public GameObject keyUI;

    public bool atKey;

    public void Update()
    {
        if (atKey)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                this.gameObject.SetActive(false);
                playerStats.hasKey = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        keyUI.SetActive(true);

        if (other.gameObject.CompareTag("Player"))
        {
            atKey = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            keyUI.SetActive(false);
            atKey = false;
        }
    }
}
