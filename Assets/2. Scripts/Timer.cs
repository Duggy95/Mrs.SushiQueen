using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    Image timer; //타이머 이미지
    float maxTime; //최대시간
    float currTime;  //현재시간
    Color currColor;  //현재색
    Color initColor = new Vector4(0f, 1f, 0f, 1f);  //초기색

    void Start()
    {
        timer = GetComponent<Image>();
        maxTime = 180;

        currTime = maxTime;  // 초기값
        timer.color = initColor;  //초기색 초록색으로
        currColor = initColor;  //현재색 초록새으로
    }

    void Update()
    {
        currTime -= Time.deltaTime;  //시간이 줄어듬

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
        if (currTime <= 0)
        {
            LoadMainScene();
        }
    }

    void LoadMainScene()  //홈 화면 전환
    {
        if (currTime <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
