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

    private void Start()
    {
        
    }
    public override void Enter()
    {
        print("��ư�̿�");
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
        // ��ư�� Ŭ���Ǿ��� �� ó���� �۾��� ���⿡ �߰�
        // ��: tutorialManager.SetNextTutorial();
        manager.SetNextTutorial();
        //Destroy(this);
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
            float t = Mathf.Clamp01(elapsedTime / duration); // �ð� ���� ���

            pulse.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }
        StartCoroutine(ScaleDown());
    }

    IEnumerator ScaleDown()
    {
        Vector2 initialScale = new Vector2(1.08f, 1.08f);
        Vector2 targetScale = new Vector2(1, 1);
        float duration = 1f; // ũ�� ��ȭ�� �ɸ��� �ð�

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // �ð� ���� ���

            pulse.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }

        StartCoroutine(ScaleUp());
    }
}
