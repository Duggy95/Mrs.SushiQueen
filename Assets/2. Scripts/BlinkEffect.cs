using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkEffect : MonoBehaviour
{
    Text text;  //타이틀 화면 터치텍스트

    void OnEnable()
    {
        text = GetComponent<Text>();
        //효과 시작
        StartCoroutine(FadeIn());
        StartCoroutine(ScaleUp());
    }
    
    // 사라지는 효과
    IEnumerator FadeIn()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);

        while (text.color.a > 0)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / 2.0f));
            yield return null;
        }
        StartCoroutine(FadeOut());
    }

    //다시 나타나는 효과
    IEnumerator FadeOut()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);

        while (text.color.a < 1)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / 2.0f));
            yield return null;
        }
        StartCoroutine(FadeIn());
    }

    //크기 커지는 효과
    IEnumerator ScaleUp()
    {
        Vector3 initialScale = new Vector3(1, 1, 1);
        Vector3 targetScale = new Vector3(1.5f, 1.5f, 1.5f);
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // 시간 비율 계산

            text.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }
        StartCoroutine(ScaleDown());
    }

    //크기 작아지는 효과
    IEnumerator ScaleDown()
    {
        Vector3 initialScale = new Vector3(1.5f, 1.5f, 1.5f);
        Vector3 targetScale = new Vector3(1, 1, 1);
        float duration = 1f; // 크기 변화에 걸리는 시간

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // 시간 비율 계산

            text.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }

        StartCoroutine(ScaleUp());
    }
}
