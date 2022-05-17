using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{

    public Animator animator;

    public Animator doubleDoorAnimator;

    public GameObject leverUI;

    public bool switched;

    public bool atLever;

    public void Update()
    {
        if (atLever)
        {
            if (Input.GetKeyDown(KeyCode.E) && !switched)
            {
                animator.SetBool("Interact", true);
                doubleDoorAnimator.SetBool("Open", true);
                switched = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player") && !switched)
        {
            leverUI.SetActive(true);
            atLever = true;
        }
        else
        {
            leverUI.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            leverUI.SetActive(false);
            atLever = false;
        }
    }
}
