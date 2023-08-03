using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DialogManager))]
public class TutorialDialog : TutorialBase
{
    DialogManager dialogManager;
    public CanvasGroup blockPanel;
    public CanvasGroup dialog;
    public string[] dialogs;
    
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
            tutorialManager.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        StartCoroutine(FadeImage());
        blockPanel.blocksRaycasts = false;
    }

    IEnumerator ShowImage()
    {
        Vector2 initPos = dialog.transform.position;
        Vector2 targetPos = new Vector2(dialog.transform.position.x - 70, dialog.transform.position.y);
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // 시간 비율 계산
            dialog.alpha = t;
            dialog.transform.position = Vector2.Lerp(initPos, targetPos, t);
            yield return null;
        }
    }

    IEnumerator FadeImage()
    {

        Vector2 initPos = dialog.transform.position;
        Vector2 targetPos = new Vector2(dialog.transform.position.x + 70, dialog.transform.position.y);
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // 시간 비율 계산
            float alpha = 1 - t;
            dialog.alpha = alpha;
            dialog.transform.position = Vector2.Lerp(initPos, targetPos, t);
            yield return null;
        }

        //Destroy(this);
    }
}
