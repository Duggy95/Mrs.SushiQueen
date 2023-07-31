using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogManager : MonoBehaviour
{
    //public static DialogManager instance;

    public Text dialogTxt;
    public GameObject nextTxt;
    public CanvasGroup canvasGroup;
    public Queue<string> sentences;
    WaitForSeconds ws;
    
    public string currSentence;
    public float typingSpeed = 0.05f;
    public bool isTyping = false;
    public bool complete = false;

    void Start()
    {
        sentences = new Queue<string>();
        ws = new WaitForSeconds(typingSpeed);
    }

    public void Ondialog(string[] lines)
    {
        sentences.Clear();
        foreach(string line in lines)
        {
            sentences.Enqueue(line);
        }
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        NextSentence();
    }

    public void NextSentence()
    {
        if (sentences.Count != 0)
        {
            //print(sentences.Count);
            currSentence = sentences.Dequeue();
            isTyping = true;
            nextTxt.SetActive(false);
            StartCoroutine(Typing(currSentence));
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            complete = true;
        }
    }

    IEnumerator Typing(string line)
    {
        dialogTxt.text = "";
        foreach(char letter in line.ToCharArray())
        {
            dialogTxt.text += letter;
            yield return ws;
        }
    }

    void Update()
    {
        if(dialogTxt.text.Equals(currSentence))
        {
            nextTxt.SetActive(true);
            isTyping = false;
        }
    }

    public void Next()
    {
        print("호출이요");
        if(!isTyping && Input.GetMouseButtonDown(0))
        {
            NextSentence();
        }
    }
}
