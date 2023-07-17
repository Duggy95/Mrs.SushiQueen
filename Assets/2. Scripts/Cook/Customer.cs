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
    List<Order> orders = new List<Order>();  //주문을 담을 리스트

    Dish dish;  //접시 오브젝트
    CookManager cookManager;
    string message = "주세요.";
    string order;  //주문
    float maxTime; //최대시간
    float currTime;  //현재시간
    float currTimePercent;  //현재시간 비율
    bool isTimer;  //타이머 존재
    bool isOrdered;  //주문수락확인.

    private void Awake()
    {
        cookManager = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<CookManager>();
    }

    private void Start()
    {
        dish = GameObject.FindGameObjectWithTag("DISH").GetComponent<Dish>();

        maxTime = 10;  //최대시간.
        currTime = maxTime;  //타이머 초기값 설정
    }

    void OnEnable()
    {
        int randomSprite = Random.Range(0, sprites.Length);  //랜덤하게 그림 결정.
        GetComponent<Image>().sprite = sprites[randomSprite];  //랜덤한 그림 적용.

        int randomSushi = Random.Range(0, sushis.Length);  //랜덤하게 초밥종류 결정.
        //int randomWasabi = Random.Range(0, wasabis.Length);
        int randomWasabi = 1;
        int count = Random.Range(1, 4);

        order = sushis[randomSushi] + "초밥 " +
                    count + "개 " +
                    "와사비 " + wasabis[randomWasabi] + message;  //주문

        orderTxt.text = order;  //텍스트에 주문내용 넣기.
        AddOrder(sushis[randomSushi], wasabis[randomWasabi], count);  //주문 리스트에 추가.
    }

    private void Update()
    {
        if(isTimer)
        {
            currTime -= Time.deltaTime;  //시간이 줄어듬
            currTimePercent = currTime / maxTime;  //남은시간 비율
            timer.fillAmount = currTimePercent;  //타이머는 남은시간 비율에 맞게 줄어듬
            if (currTime <= 0)
            {
                print("님 평판 깎임.");
                orderTxt.text = "님 실망임.";
                Destroy(gameObject, 0.5f);
                cookManager.Create();
                orders.Clear();
                isTimer = false;
                isOrdered = false;
            }
        }
    }

    public void ShowTimer()  //손님 타이머 활성화
    {
        isTimer = true;
        isOrdered = true;
        dish.sushiCounts.Clear();
        dish.ClearSushi();
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

    void AddOrder(string sushiName, string wasabi, int count)  //주문 추가 메서드
    {
        Order order = new Order(sushiName, wasabi, count);  //초밥종류, 와사비, 갯수 정보저장.
        orders.Add(order);
    }

    public void CompareOrders()  //주문비교 메서드.
    {
        if(isOrdered)
        {
            List<Order> orders = this.orders;  //주문 리스트
            Dictionary<string, int> sushiCounts = dish.sushiCounts;  //접시 딕셔너리
            foreach (Order order in orders)
            {
                string sushiKey = order.sushiName + "_" + order.wasabi;  //주문 초밥 + 와사비를 키로.

                if (sushiCounts.ContainsKey(sushiKey))  //주문한 초밥 키와 일치하는게 접시 딕셔너리에 있는가?
                {
                    int sushiCount = sushiCounts[sushiKey];  //있다면 그 초밥 키의 갯수를 주문 갯수와 비교.
                    if (sushiCount == order.count)  //주문갯수와 초밥 갯수가 일치한다면
                    {
                        Debug.Log($"주문과 초밥 정보 일치 - 종류: {order.sushiName}, 와사비: {order.wasabi}, 갯수: {order.count}");
                        orderTxt.text = "잘 먹을게요!";
                        Destroy(gameObject, 0.5f);
                        orders.Clear();
                        dish.sushiCounts.Clear();
                        dish.ClearSushi();
                        cookManager.Create();
                    }
                    else
                    {
                        Debug.Log($"주문과 초밥 정보 불일치 - 종류: {order.sushiName}, 와사비: {order.wasabi}, 갯수: {order.count}");
                        orderTxt.text = "가게 접으셈";
                        Destroy(gameObject, 0.5f);
                        orders.Clear();
                        dish.sushiCounts.Clear();
                        dish.ClearSushi();
                        cookManager.Create();
                    }
                }
                else
                {
                    Debug.Log($"주문과 초밥 정보 불일치 - 종류: {order.sushiName}, 와사비: {order.wasabi}, 갯수: {order.count}");
                    orderTxt.text = "가게 접으셈";
                    Destroy(gameObject, 0.5f);
                    orders.Clear();
                    dish.sushiCounts.Clear();
                    dish.ClearSushi();
                    cookManager.Create();
                }
            }
        }
    }
}

public class Order  //주문 클래스
{
    public string sushiName;  //초밥 종류
    public string wasabi;  //와사비 유무
    public int count;  //갯수

    public Order(string sushiName, string wasabi, int count)  //생성자
    {
        this.sushiName = sushiName;
        this.wasabi = wasabi;
        this.count = count;
    }
}
