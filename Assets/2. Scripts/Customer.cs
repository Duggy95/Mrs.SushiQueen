using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public Text orderTxt;  //주문 텍스트
    public Sprite[] sprites;
    public string[] sushis;
    public string[] wasabis;

    CookManager cookManager;
    string message = "주세요.";
    string order;
    Image timer; //타이머 이미지
    float maxTime; //최대시간
    float currTime;  //현재시간
    float currTimePercent;

    void OnEnable()
    {
        int randomSprite = Random.Range(0, sprites.Length);
        GetComponent<Image>().sprite = sprites[randomSprite];

        int randomSushi = Random.Range(0, sushis.Length);
        int randomWasabi = Random.Range(0, wasabis.Length);
        order = sushis[randomSushi] + "초밥 " +
                    Random.Range(1, 4) + "개 " +
                    "와사비 " + wasabis[randomWasabi] + message;
        Text orderTxt = GameObject.Find("Order_Text").GetComponent<Text>();
        orderTxt.text = order;
    }

    private void Awake()
    {
        cookManager = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<CookManager>();
    }

    private void Start()
    {
        timer = GetComponent<Image>();
        maxTime = 30;
        currTime = maxTime;  // 초기값

    }

    private void Update()
    {
        currTime -= Time.deltaTime * 5;  //시간이 줄어듬
        currTimePercent = currTime / maxTime;  //남은시간 비율
        timer.fillAmount = currTimePercent;  //타이머는 남은시간 비율에 맞게 줄어듬
        if (currTime <= 0)
        {
            //평판 깎임.
            print("님 평판 깎임.");
            orderTxt.text = "님 실망임.";
            cookManager.Create();
            Destroy(gameObject);
        }
    }
}
