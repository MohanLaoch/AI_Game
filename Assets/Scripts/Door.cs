using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject doorUI;

    public Animator animator;

    public PlayerStats playerStats;

    public bool atDoor;

    public bool doorOpened;

    public bool doorLocked;

    public void Update()
    {
        if (atDoor)
        {
            if (Input.GetKeyDown(KeyCode.E) && !doorLocked)
            {
                animator.SetBool("Open", true);
                FindObjectOfType<AudioManager>().Play("DoorOpen");
                doorOpened = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        doorUI.SetActive(true);

        if (other.gameObject.CompareTag("Player") && !doorOpened)
        {
            atDoor = true;
        }

        if (playerStats.hasKey)
        {
            doorLocked = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            doorUI.SetActive(false);
            atDoor = false;
        }
    }
}
