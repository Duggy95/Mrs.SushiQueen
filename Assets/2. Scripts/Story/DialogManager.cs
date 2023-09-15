using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogManager : MonoBehaviour
{
    public Text dialogTxt;  //��� �ؽ�Ʈ
    public GameObject nextTxt;
    public CanvasGroup canvasGroup;
    public Queue<string> sentences;  //������ ���� ť
    WaitForSeconds ws;

    public string currSentence;  //���� ���
    public float typingSpeed = 0.075f;  //��� ���ǵ�
    public bool isTyping = false;  //��� ������� ��Ÿ��
    public bool complete = false;  //���丮 �Ϸ� ����

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        sentences = new Queue<string>();
        ws = new WaitForSeconds(typingSpeed);
    }

    public void Ondialog(string[] lines)
    {
        sentences.Clear();  //������ ���� ť �ʱ�ȭ
        foreach (string line in lines)  //���� ť�� �ֱ�
        {
            sentences.Enqueue(line);
        }
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        NextSentence();
    }

    public void NextSentence()
    {
        if (sentences.Count != 0)  //ť�� ��簡 ���������� 
        {
            currSentence = sentences.Dequeue();  //ť���� ��縦 ����. ���� ��翡 �ִ´�.
            isTyping = true;
            nextTxt.SetActive(false);
            StopAllCoroutines();
            StartCoroutine(Typing(currSentence));  //���� ��� �ڷ�ƾ���� ���
        }
        else  //������ ��ü ��� �Ϸ�.
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            complete = true;
        }
    }

    IEnumerator Typing(string line)
    {
        dialogTxt.text = "";
        foreach (char letter in line.ToCharArray())  //�� ���ھ� Ÿ���� ȿ��
        {
            audioSource.PlayOneShot(SoundManager.instance.storyText, 0.25f);
            dialogTxt.text += letter;
            yield return ws;
        }
    }

    public void SkipSentence()  //�ٷ� ���� ���� �ϼ���Ű�� �޼���
    {
        StopAllCoroutines();
        dialogTxt.text = "";
        dialogTxt.text = currSentence;
    }

    void Update()
    {
        if (dialogTxt.text.Equals(currSentence))  //���� ��簡 �ϼ��Ǹ�
        {
            nextTxt.SetActive(true);
            isTyping = false;
        }
    }

    public void Next()
    {
        if (!isTyping && Input.GetMouseButtonDown(0))
        {
            NextSentence();  //��簡 ������ ���콺 ���� Ŭ���� ���� ��� ���
        }
        else if (isTyping && Input.GetMouseButtonDown(0))
        {
            SkipSentence();  //��簡 ������̰� ���콺 ���� Ŭ���� ���� ��� �ϼ���Ű��
        }
    }
}

