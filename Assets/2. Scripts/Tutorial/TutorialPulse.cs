using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TutorialPulse : TutorialBase
{
    TutorialManager manager;
    public GameObject pulse;
    public CanvasGroup canvas;

    public override void Enter()
    {
        //print("버튼이요");
        manager = GetComponentInParent<TutorialManager>();
        Button button = pulse.GetComponent<Button>();
        button.onClick.AddListener(OnPulseButtonClick);
        StartCoroutine(ScaleUp());
        canvas.blocksRaycasts = true;
    }

    public override void Execute(TutorialManager tutorialManager)
    {

    }

    public override void Exit()
    {

    }

    void OnPulseButtonClick()
    {
        //버튼이 클릭될 때 호출
        //다음 튜토리얼로
        manager.SetNextTutorial();
        StopAllCoroutines();
        pulse.transform.localScale = new Vector2(1, 1);
        Destroy(this);
    }

    IEnumerator ScaleUp()
    {
        Vector2 initialScale = new Vector2(1, 1);
        Vector2 targetScale = new Vector2(1.08f, 1.08f);
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // 시간 비율 계산

            pulse.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }
        StartCoroutine(ScaleDown());
    }

    IEnumerator ScaleDown()
    {
        Vector2 initialScale = new Vector2(1.08f, 1.08f);
        Vector2 targetScale = new Vector2(1, 1);
        float duration = 1f; // 크기 변화에 걸리는 시간

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // 시간 비율 계산

            pulse.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }

        StartCoroutine(ScaleUp());
    }
}
