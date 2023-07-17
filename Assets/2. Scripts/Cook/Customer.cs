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

    Dish dish;  //초밥 담는 접시
    CookManager cookManager;
    string message = "주세요.";
    string order;  //주문
    float maxTime; //최대시간
    float currTime;  //현재시간
    float currTimePercent;  //현재시간 비율
    bool isTimer;  //타이머 존재
    bool isOrdered;  //주문수락확인.

    string sushiName1;
    string sushiName2;
    string sushiName3;
    int sushiCount1;
    int sushiCount2;
    int sushiCount3;
    string wasabi1;
    string wasabi2;
    string wasabi3;

    private void Awake()
    {
        cookManager = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<CookManager>();
    }

    private void Start()
    {
        dish = GameObject.FindGameObjectWithTag("DISH").GetComponent<Dish>();  //접시 오브젝트 Find로 가져오기

        maxTime = 15;  //최대시간.
        currTime = maxTime;  //타이머 초기값 설정
    }

    void OnEnable()
    {
        //int randomOrder = Random.Range(0, 3);
        int randomOrder = 0;
        int randomSprite = Random.Range(0, sprites.Length);  //랜덤하게 그림 결정.
        GetComponent<Image>().sprite = sprites[randomSprite];  //랜덤한 그림 적용.


        int randomSushi = Random.Range(0, sushis.Length);  //랜덤하게 초밥종류 결정.
        //int randomWasabi = Random.Range(0, wasabis.Length);
        //int randomWasabi = 1;
        int count = Random.Range(1, 4);

        switch (randomOrder)  //초밥의 주문 랜덤한 길이.
        {
            case 0:
                //초밥 한 종류.
                sushiName1 = sushis[Random.Range(0, sushis.Length)];
                sushiCount1 = Random.Range(1, 4);
                wasabi1 = wasabis[Random.Range(0, wasabis.Length)];
                order = sushiName1 + "초밥 " + sushiCount1 + "개 " + "와사비 " + wasabi1 + message;
                AddOrder(sushiName1, wasabi1, sushiCount1);
                orderTxt.text = order;
                break;
            case 1:
                //초밥 두 종류.
                sushiName1 = sushis[Random.Range(0, sushis.Length)];
                sushiName2 = sushis[Random.Range(0, sushis.Length)];
                sushiCount1 = Random.Range(1, 4);
                sushiCount2 = Random.Range(1, 4);
                wasabi1 = wasabis[Random.Range(0, wasabis.Length)];
                wasabi2 = wasabis[Random.Range(0, wasabis.Length)];
                order = sushiName1 + "초밥 " + sushiCount1 + "개 " + "와사비 " + wasabi1 +
                            sushiName2 + "초밥 " + sushiCount2 + "개 " + "와사비" + wasabi2 + message;
                AddOrder(sushiName1, wasabi1, sushiCount1);
                AddOrder(sushiName2, wasabi2, sushiCount2);
                orderTxt.text = order;
                break;
            case 2:
                //초밥 세 종류.
                sushiName1 = sushis[Random.Range(0, sushis.Length)];
                sushiName2 = sushis[Random.Range(0, sushis.Length)];
                sushiName3 = sushis[Random.Range(0, sushis.Length)];
                sushiCount1 = Random.Range(1, 3);
                sushiCount2 = Random.Range(1, 3);
                sushiCount3 = Random.Range(1, 3);
                wasabi1 = wasabis[Random.Range(0, wasabis.Length)];
                wasabi2 = wasabis[Random.Range(0, wasabis.Length)];
                wasabi3 = wasabis[Random.Range(0, wasabis.Length)];
                order = sushiName1 + "초밥 " + sushiCount1 + "개 " + "와사비 " + wasabi1 +
                            sushiName2 + "초밥 " + sushiCount2 + "개 " + "와사비 " + wasabi2 +
                            sushiName3 + "초밥 " + sushiCount3 + "개 " + "와사비 " + wasabi3 + message;
                AddOrder(sushiName1, wasabi1, sushiCount1);
                AddOrder(sushiName2, wasabi2, sushiCount2);
                AddOrder(sushiName3, wasabi3, sushiCount3);
                orderTxt.text = order;
                break;
        }

        /*order = sushis[randomSushi] + "초밥 " +
                    count + "개 " +
                    "와사비 " + wasabis[randomWasabi] + message;*/  //주문

        //텍스트에 주문내용 넣기.
        //orderTxt.text = order; 

        //주문 리스트에 추가.
        //AddOrder(sushis[randomSushi], wasabis[randomWasabi], count);
    }

    private void Update()
    {
        if (isTimer)
        {
            currTime -= Time.deltaTime;  //시간이 줄어듬
            currTimePercent = currTime / maxTime;  //남은시간 비율
            timer.fillAmount = currTimePercent;  //타이머는 남은시간 비율에 맞게 줄어듬
            if (currTime <= 0)
            {
                print("님 평판 깎임.");
                orderTxt.text = "님 실망임.";
                Destroy(gameObject, 0.5f);  //손님 삭제
                cookManager.Create();  //손님 생성.
                orders.Clear();  //주문 리스트 클리어.
                isTimer = false;  //타이머 비활성화 상태로 판단
                isOrdered = false;  //주문을 안받았음.
            }
        }
    }

    public void ShowTimer()  //손님 타이머 활성화
    {
        isTimer = true;  //타이머 활성화 상태로 판단.
        isOrdered = true;  //주문을 받았음.
        dish.sushiCounts.Clear();  //초밥 딕셔너리 클리어.
        dish.ClearSushi();  //접시 위 초밥 삭제.
        timerObj.SetActive(true);  //타이머 활성화.
        yesBtn.SetActive(false);  //버튼 비활성화.
        noBtn.SetActive(false);  //버튼 비활성화.
    }

    public void NoBtn() //거절 버튼.
    {
        orderTxt.text = "님 실망임.";
        Destroy(gameObject, 0.5f);  //손님 삭제
        cookManager.Create();  //손님생성
        yesBtn.SetActive(false);  //버튼 비활성화
        noBtn.SetActive(false);  //버튼 비활성화
        print("님 평판 깎임");
    }

    void AddOrder(string sushiName, string wasabi, int count)  //주문 추가 메서드
    {
        Order order = new Order(sushiName, wasabi, count);  //초밥종류, 와사비, 갯수 정보저장.
        orders.Add(order);
    }

    public void CompareOrders()  //주문비교 메서드.
    {
        if (isOrdered)
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
                        Destroy(gameObject, 0.5f);  //손님 삭제
                        orders.Clear();  //주문 리스트 클리어.
                        dish.sushiCounts.Clear();  //초밥 딕셔너리 클리어.
                        dish.ClearSushi();  //접시 위 초밥 삭제.
                        cookManager.Create();  //손님 생성.
                    }
                    else
                    {
                        Debug.Log($"주문과 초밥 정보 불일치 - 종류: {order.sushiName}, 와사비: {order.wasabi}, 갯수: {order.count}");
                        orderTxt.text = "가게 접으셈";
                        Destroy(gameObject, 0.5f);  //손님 삭제
                        orders.Clear();  //주문 리스트 클리어.
                        dish.sushiCounts.Clear();  //초밥 딕셔너리 클리어.
                        dish.ClearSushi();  //접시 위 초밥 삭제.
                        cookManager.Create();  //초밥 생성.
                    }
                }
                else
                {
                    Debug.Log($"주문과 초밥 정보 불일치 - 종류: {order.sushiName}, 와사비: {order.wasabi}, 갯수: {order.count}");
                    orderTxt.text = "가게 접으셈";
                    Destroy(gameObject, 0.5f);  //손님 삭제
                    orders.Clear();  //주문 리스트 클리어.
                    dish.sushiCounts.Clear();  //초밥 딕셔너리 클리어.
                    dish.ClearSushi();  //접시 위 초밥 삭제.
                    cookManager.Create();  //손님 생성.
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
