using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    Image timer; //타이머 이미지
    float maxTime = 100; //최대시간
    float currTime;  //현재시간
    Color currColor;  //현재색
    Color initColor = new Vector4(0f, 1f, 0f, 1f);  //초기색
    bool isFish = false; //낚시중인지 아닌지 판단 

    void Start()
    {
        timer = GetComponent<Image>();

        currTime = maxTime;  // 초기값
        timer.color = initColor;  //초기색 초록색으로
        currColor = initColor;  //현재색 초록새으로
    }

    void Update()
    {
        currTime -= Time.deltaTime * 5;  //시간이 줄어듬

        float currTimePercent = currTime / maxTime;  //남은시간 비율
        if (currTimePercent > 0.5f)  // 반이상 남았을 때
        {
            currColor.r = (1 - currTimePercent) * 2f;  //현재색에서 빨간색을 점점 추가
        }
        else  // 반이하 남았을 때
        {
            currColor.g = currTimePercent * 2;  //현재색에서 초록색 점점 감소
        }

        timer.color = currColor;  //타이머 색을 현재색으로
        timer.fillAmount = currTimePercent;  //타이머는 남은시간 비율에 맞게 줄어듬

        LoadCookScene();
    }

    void LoadCookScene()
    {
        if((currTime <= 0 && !isFish)) // 현재시간이 0이고 낚시중이 아닐 때
        {
            SceneManager.LoadScene(2);  //요리씬으로 넘어감
        }
    }
}
