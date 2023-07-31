using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DialogManager))]
public class TutorialDialog : TutorialBase
{
    public CanvasGroup blockPanel;
    public Image image;
    public string[] dialogs;
    DialogManager dialogManager;
    
    public override void Enter()
    {
        StartCoroutine(ShowImage());
        dialogManager = GetComponent<DialogManager>();
        dialogManager.Ondialog(dialogs);
        blockPanel.blocksRaycasts = true;
    }

    public override void Execute(TutorialManager tutorialManager)
    {
        dialogManager.Next();
        if(this.dialogManager.complete == true)
        {
            //Destroy(this, 2);
            tutorialManager.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        print("시작이요");
        StartCoroutine(FadeImage());
        blockPanel.blocksRaycasts = false;
    }

    IEnumerator ShowImage()
    {
        Vector2 initPos = image.transform.position;
        Vector2 targetPos = new Vector2(image.transform.position.x - 70, image.transform.position.y);
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // 시간 비율 계산
            image.color = new Color(image.color.r, image.color.g, image.color.b, t);

            image.transform.position = Vector2.Lerp(initPos, targetPos, t);
            yield return null;
        }
        print("나타났다");
    }

    IEnumerator FadeImage()
    {

        Vector2 initPos = image.transform.position;
        Vector2 targetPos = new Vector2(image.transform.position.x + 70, image.transform.position.y);
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // 시간 비율 계산
            float alpha = 1 - t;

            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            image.transform.position = Vector2.Lerp(initPos, targetPos, t);
            yield return null;
        }
    }
}
