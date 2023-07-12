using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public Text orderTxt;  //주문 텍스트
    public Sprite[] sprites;  //손님 스프라이트 배열
    public string[] sushis;  //초밥 종류 배열
    public string[] wasabis;  //와사비
    public GameObject yesBtn;  //수락 버튼
    public GameObject noBtn;  //거절 버튼
    public GameObject timerObj; //타이머 오브젝트
    public Image timer; //타이머 이미지

    CookManager cookManager;
    string message = "주세요.";
    string order;  //주문
    float maxTime; //최대시간
    float currTime;  //현재시간
    //float currTimePercent;  //현재시간 비율
    bool isTimer;

    private void Awake()
    {
        cookManager = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<CookManager>();
    }

    private void Start()
    {
        maxTime = 30;
        currTime = maxTime;  //타이머 초기값 설정
    }

    void OnEnable()
    {
        int randomSprite = Random.Range(0, sprites.Length);
        GetComponent<Image>().sprite = sprites[randomSprite];

        int randomSushi = Random.Range(0, sushis.Length);
        int randomWasabi = Random.Range(0, wasabis.Length);
        order = sushis[randomSushi] + "초밥 " +
                    Random.Range(1, 4) + "개 " +
                    "와사비 " + wasabis[randomWasabi] + message;

        orderTxt.text = order;
    }

    private void Update()
    {
        if(isTimer)
        {
            currTime -= Time.deltaTime * 5;  //시간이 줄어듬
            float currTimePercent = currTime / maxTime;  //남은시간 비율
            timer.fillAmount = currTimePercent;  //타이머는 남은시간 비율에 맞게 줄어듬
            if (currTime <= 0)
            {
                print("님 평판 깎임.");
                orderTxt.text = "님 실망임.";
                Destroy(gameObject, 0.5f);
                cookManager.Create();
                isTimer = false;
            }
        }
    }

    public void ShowTimer()  //손님 타이머 활성화
    {
        isTimer = true;
        timerObj.SetActive(true);
        yesBtn.SetActive(false);
        noBtn.SetActive(false);
    }

    public void NoBtn() //거절 버튼.
    {
        orderTxt.text = "님 실망임.";
        Destroy(gameObject, 0.5f);
        cookManager.Create();
        yesBtn.SetActive(false);
        noBtn.SetActive(false);
        print("님 평판 깎임");
    }
}
