using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Text orderTxt;  //주문 텍스트
    Image timer; //타이머 이미지
    float maxTime; //최대시간
    float currTime;  //현재시간
    Color currColor;  //현재색
    Color initColor = new Vector4(0f, 1f, 0f, 1f);  //초기색

    void Start()
    {
        timer = GetComponent<Image>();

        if(gameObject.CompareTag("CUSTOMER"))  //게임오브젝트 태그가 CUSTOMER이면 
        {
            maxTime = 30;
        }
        else
        {
            maxTime = 100;
        }

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
        if(currTime <= 0)
        {
            if(gameObject.CompareTag("TIMER"))  //태그가 TIMER이면 홈 화면으로
            {
                LoadMainScene();
            }
            else if(gameObject.CompareTag("CUSTOMER"))  //태그가 CUSTOMER이면 평판감소 텍스트 출력.
            {
                //평판 깎임.
                print("님 평판 깎임.");
                orderTxt.text = "님 실망임.";
                CookManager.instance.Create();
            }
        }
    }

    void LoadMainScene()  //홈 화면 전환
    {
        if(currTime <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
