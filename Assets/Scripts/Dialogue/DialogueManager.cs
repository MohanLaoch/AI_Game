using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject introDoor;
    public TMP_Text dialogueText;

    [SerializeField] private float RegularTypeSpeed = 0.01f;
    [SerializeField] private float FastTypeSpeed = 0.001f;
    private float CurrentTypeSpeed;

    private bool typingSentence;
    public bool finishedTalking;
    public bool talking = false;
    public Queue<string> sentences;

    //private AudioSource Hum;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        //Hum = gameObject.GetComponent<AudioSource>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation with " + dialogue.name);

        sentences.Clear();

        foreach (var sentence in dialogue.sentence)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
    public void Update()
    {
        if (talking == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (typingSentence == true)
                {
                    CurrentTypeSpeed = FastTypeSpeed;
                }
                else
                {
                    DisplayNextSentence();
                }
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void DisplayNextSentence()
    {
        CurrentTypeSpeed = RegularTypeSpeed;

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        typingSentence = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            /*if (!Hum.isPlaying)
            {
                Hum.Play();
            }*/
            yield return new WaitForSeconds(CurrentTypeSpeed);
        }
        typingSentence = false;
    }

    private void EndDialogue()
    {
        Debug.Log("End of Conversation");
        StopAllCoroutines();
        dialogueText.text = "";
        talking = false;
        finishedTalking = true;
        introDoor.gameObject.SetActive(false);
    }
}
