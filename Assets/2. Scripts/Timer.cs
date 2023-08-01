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
        
        currTime = maxTime;  // 초기값
        timer.color = initColor;  //초기색 초록색으로
        currColor = initColor;  //현재색 초록새으로
    }

    void Update()
    {
        if(currentScene.buildIndex == 2)
        {
            if(cookManager.isReady)
                currTime -= Time.deltaTime;  //시간이 줄어듬
        }
        else
        {
            currTime -= Time.deltaTime;  //시간이 줄어듬
        }

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

    void LoadMainScene()  //홈 화면 전환
    {
        if (currTime <= 0)
        {
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }
    }
}
