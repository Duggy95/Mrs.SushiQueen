using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    Image timer; //Ÿ�̸� �̹���
    float maxTime; //�ִ�ð�
    float currTime;  //����ð�
    Color currColor;  //�����
    Color initColor = new Vector4(0f, 1f, 0f, 1f);  //�ʱ��

    void Start()
    {
        timer = GetComponent<Image>();
        maxTime = 180;

        currTime = maxTime;  // �ʱⰪ
        timer.color = initColor;  //�ʱ�� �ʷϻ�����
        currColor = initColor;  //����� �ʷϻ�����
    }

    void Update()
    {
        currTime -= Time.deltaTime;  //�ð��� �پ��

        float currTimePercent = currTime / maxTime;  //�����ð� ����
        if (currTimePercent > 0.5f)  // ���̻� ������ ��
        {
            currColor.r = (1 - currTimePercent) * 2f;  //��������� �������� ���� �߰�
        }
        else  // ������ ������ ��
        {
            currColor.g = currTimePercent * 2;  //��������� �ʷϻ� ���� ����
        }

        timer.color = currColor;  //Ÿ�̸� ���� ���������
        timer.fillAmount = currTimePercent;  //Ÿ�̸Ӵ� �����ð� ������ �°� �پ��
        if (currTime <= 0)
        {
            LoadMainScene();
        }
    }

    void LoadMainScene()  //Ȩ ȭ�� ��ȯ
    {
        if (currTime <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
