using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject chestUI;

    public Animator animator;

    public PlayerStats playerStats;

    public bool atChest;

    public bool chestOpened;

    public void Update()
    {
        if (atChest)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetBool("Open", true);
                chestOpened = true;
                playerStats.hasKey = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        chestUI.SetActive(true);

        if (other.gameObject.CompareTag("Player") && !chestOpened)
        {
            atChest = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            chestUI.SetActive(false);
            atChest = false;
        }
    }
}
