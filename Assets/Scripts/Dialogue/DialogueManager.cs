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
    public Image portraitObj;

    [SerializeField] private float RegularTypeSpeed = 0.01f;
    [SerializeField] private float FastTypeSpeed = 0.001f;
    private float CurrentTypeSpeed;

    private bool typingSentence;
    public bool finishedTalking;
    public bool talking = false;
    public Queue<string> sentences;
    public Queue<Sprite> sentencePortrait;
    public Queue<Color> sentenceColor;

    //private AudioSource Hum;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        sentencePortrait = new Queue<Sprite>();
        sentenceColor = new Queue<Color>();
        //Hum = gameObject.GetComponent<AudioSource>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation with " + dialogue.name);

        sentences.Clear();
        sentencePortrait.Clear();
        sentenceColor.Clear();

        for(int i = 0; i < dialogue.sentence.Length; i++)
        {
            sentences.Enqueue(dialogue.sentence[i]);

            if (dialogue.CharacterPortrait[i] != null)
            {
                sentencePortrait.Enqueue(dialogue.CharacterPortrait[i]);
            }
            if (dialogue.TextColor[i] != null)
            {
                sentenceColor.Enqueue(dialogue.TextColor[i]);
            }
            else
            {
                sentenceColor.Enqueue(Color.black);
            }
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

        Sprite portrait;
        if (sentencePortrait.Count != 0) { portrait = sentencePortrait.Dequeue(); }
        else { portrait = null; }

        Color color = sentenceColor.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence, portrait, color));
    }

    IEnumerator TypeSentence(string sentence, Sprite portrait, Color color)
    {
        typingSentence = true;
        dialogueText.text = "";
        dialogueText.color = color;
        portraitObj.sprite = portrait;

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
        portraitObj.sprite = null;
        dialogueText.color = Color.black;
        
        talking = false;
        finishedTalking = true;
        introDoor.gameObject.SetActive(false);
    }
}
