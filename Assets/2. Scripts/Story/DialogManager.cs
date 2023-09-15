using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogManager : MonoBehaviour
{
    public Text dialogTxt;  //대사 텍스트
    public GameObject nextTxt;
    public CanvasGroup canvasGroup;
    public Queue<string> sentences;  //대사들을 담을 큐
    WaitForSeconds ws;

    public string currSentence;  //현재 대사
    public float typingSpeed = 0.075f;  //출력 스피드
    public bool isTyping = false;  //대사 출력중을 나타냄
    public bool complete = false;  //스토리 완료 여부

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        sentences = new Queue<string>();
        ws = new WaitForSeconds(typingSpeed);
    }

    public void Ondialog(string[] lines)
    {
        sentences.Clear();  //만약을 위해 큐 초기화
        foreach (string line in lines)  //대사들 큐에 넣기
        {
            sentences.Enqueue(line);
        }
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        NextSentence();
    }

    public void NextSentence()
    {
        if (sentences.Count != 0)  //큐에 대사가 남아있으면 
        {
            currSentence = sentences.Dequeue();  //큐에서 대사를 추출. 현재 대사에 넣는다.
            isTyping = true;
            nextTxt.SetActive(false);
            StopAllCoroutines();
            StartCoroutine(Typing(currSentence));  //현재 대사 코루틴으로 출력
        }
        else  //없으면 전체 대사 완료.
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            complete = true;
        }
    }

    IEnumerator Typing(string line)
    {
        dialogTxt.text = "";
        foreach (char letter in line.ToCharArray())  //한 글자씩 타이핑 효과
        {
            audioSource.PlayOneShot(SoundManager.instance.storyText, 0.25f);
            dialogTxt.text += letter;
            yield return ws;
        }
    }

    public void SkipSentence()  //바로 현재 대사로 완성시키는 메서드
    {
        StopAllCoroutines();
        dialogTxt.text = "";
        dialogTxt.text = currSentence;
    }

    void Update()
    {
        if (dialogTxt.text.Equals(currSentence))  //현재 대사가 완성되면
        {
            nextTxt.SetActive(true);
            isTyping = false;
        }
    }

    public void Next()
    {
        if (!isTyping && Input.GetMouseButtonDown(0))
        {
            NextSentence();  //대사가 끝났고 마우스 왼쪽 클릭시 다음 대사 출력
        }
        else if (isTyping && Input.GetMouseButtonDown(0))
        {
            SkipSentence();  //대사가 출력중이고 마우스 왼쪽 클릭시 현재 대사 완성시키기
        }
    }
}

