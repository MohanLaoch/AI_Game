using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Animations;

public class Door : MonoBehaviour
{
    public GameObject doorUI;

    public Animator animator;

    public PlayerStats playerStats;

    public bool doorOpened;

    public bool doorLocked;

    private void OnTriggerEnter(Collider other)
    {
        doorUI.SetActive(true);

        if (playerStats.hasKey)
        {
            doorLocked = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !doorOpened)
        {

            if (Input.GetKeyDown(KeyCode.E) && !doorLocked)
            {
                animator.SetBool("Open", true);
                FindObjectOfType<AudioManager>().Play("DoorOpen");
                doorOpened = true;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            doorUI.SetActive(false);
        }
    }
}
