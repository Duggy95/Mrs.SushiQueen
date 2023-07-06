using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    Image timer; //Ÿ�̸� �̹���
    float maxTime = 100; //�ִ�ð�
    float currTime;  //����ð�
    Color currColor;  //�����
    Color initColor = new Vector4(0f, 1f, 0f, 1f);  //�ʱ��
    bool isFish = false; //���������� �ƴ��� �Ǵ� 

    void Start()
    {
        timer = GetComponent<Image>();

        currTime = maxTime;  // �ʱⰪ
        timer.color = initColor;  //�ʱ�� �ʷϻ�����
        currColor = initColor;  //����� �ʷϻ�����
    }

    void Update()
    {
        currTime -= Time.deltaTime * 5;  //�ð��� �پ��

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

        LoadCookScene();
    }

    void LoadCookScene()
    {
        if((currTime <= 0 && !isFish)) // ����ð��� 0�̰� �������� �ƴ� ��
        {
            SceneManager.LoadScene(2);  //�丮������ �Ѿ
        }
    }
}
