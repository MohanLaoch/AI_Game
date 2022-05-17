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

        if (other.gameObject.CompareTag("Player") && !chestOpened)
        {
            chestUI.SetActive(true);
            atChest = true;
        }
        else
        {
            chestUI.SetActive(false);
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
