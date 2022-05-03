using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private bool playerNear = false;
    //public AudioSource SoundFX;

    private bool thisDialogueTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (FindObjectOfType<DialogueManager>().talking != true)
            {
                if (thisDialogueTriggered != true)
                {
                    FindObjectOfType<DialogueManager>().finishedTalking = false;
                    TriggerDialogue();
                    
                }
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        playerNear = false;
        FindObjectOfType<DialogueManager>().talking = true;

        thisDialogueTriggered = true;

        /*if (SoundFX != null)
        {
            if (!SoundFX.isPlaying)
            {
                SoundFX.Play();
            }
        }*/
    }
}
