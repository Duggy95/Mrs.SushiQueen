using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkEffect : MonoBehaviour
{
    Text text;  //Ÿ��Ʋ ȭ�� ��ġ�ؽ�Ʈ

    void OnEnable()
    {
        text = GetComponent<Text>();
        //ȿ�� ����
        StartCoroutine(FadeIn());
        StartCoroutine(ScaleUp());
    }
    
    // ������� ȿ��
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

    //�ٽ� ��Ÿ���� ȿ��
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

    //ũ�� Ŀ���� ȿ��
    IEnumerator ScaleUp()
    {
        Vector3 initialScale = new Vector3(1, 1, 1);
        Vector3 targetScale = new Vector3(1.5f, 1.5f, 1.5f);
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // �ð� ���� ���

            text.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }
        StartCoroutine(ScaleDown());
    }

    //ũ�� �۾����� ȿ��
    IEnumerator ScaleDown()
    {
        Vector3 initialScale = new Vector3(1.5f, 1.5f, 1.5f);
        Vector3 targetScale = new Vector3(1, 1, 1);
        float duration = 1f; // ũ�� ��ȭ�� �ɸ��� �ð�

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // �ð� ���� ���

            text.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }

        StartCoroutine(ScaleUp());
    }
}
