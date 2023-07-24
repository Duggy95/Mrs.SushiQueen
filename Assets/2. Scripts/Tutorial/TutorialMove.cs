using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMove : TutorialBase
{
    public Image image;
    public bool isShow;
    bool isComplete = false;

    public override void Enter()
    {
        if(!isShow)
        {
            StartCoroutine(ShowImage());
        }
        else
        {
            StartCoroutine(FadeImage());
        }
    }

    public override void Execute(TutorialManager tutorialManager)
    {
        if(isComplete)
        {
            tutorialManager.SetNextTutorial();
            isComplete = false;
        }
    }

    public override void Exit()
    {
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
        isShow = true;
        isComplete = true;
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
        isShow = false;
        isComplete = true;
    }
}
