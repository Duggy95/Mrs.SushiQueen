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
    Scene currentScene;
    CookManager cookManager;
    FishingManager fishingManager;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();

        timer = GetComponent<Image>();
        if (currentScene.buildIndex == 1)
        {
            fishingManager = GameObject.FindWithTag("MANAGER").GetComponent<FishingManager>();
            maxTime = int.Parse(GameManager.instance.data.fishTime);
        }
        else if (currentScene.buildIndex == 2)
        {
            cookManager = GameObject.FindWithTag("MANAGER").GetComponent<CookManager>();
            maxTime = int.Parse(GameManager.instance.data.cookTime);
        }
        
        currTime = maxTime;  // �ʱⰪ
        timer.color = initColor;  //�ʱ�� �ʷϻ�����
        currColor = initColor;  //����� �ʷϻ�����
    }

    void Update()
    {
        if(currentScene.buildIndex == 2)
        {
            if(cookManager.isReady)
                currTime -= Time.deltaTime;  //�ð��� �پ��
        }
        else
        {
            currTime -= Time.deltaTime;  //�ð��� �پ��
        }

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
            //LoadMainScene();
            if(currentScene.buildIndex == 1)
            {
                fishingManager.endScenePanel.SetActive(true);
                fishingManager.blockFullImg.SetActive(true);
            }
            else
            {
                cookManager.endScenePanel.SetActive(true);
                cookManager.blockFullImg.SetActive(true);
                cookManager.isEnd = true;
                Time.timeScale = 0;
            }
        }
    }

    void LoadMainScene()  //Ȩ ȭ�� ��ȯ
    {
        if (currTime <= 0)
        {
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }
    }
}
