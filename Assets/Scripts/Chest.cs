using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject chestUI;

    public GameObject key;

    public Animator animator;

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
                key.SetActive(true);
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
