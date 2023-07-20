using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public Text orderTxt;  //주문 텍스트
    public Sprite[] sprites;  //손님 스프라이트 배열
    public string[] sushis;  //초밥 종류 배열
    public string[] wasabis;  //와사비
    public string[] compliment;  //대성공 문구.
    public string[] success;  //성공 문구.
    public string[] fail;  //실패 문구.
    public GameObject yesBtn;  //수락 버튼
    public GameObject noBtn;  //거절 버튼
    public GameObject timerObj; //타이머 오브젝트
    public Image timer; //타이머 이미지
    List<Order> orders = new List<Order>();  //주문을 담을 리스트

    Dish dish;  //초밥 담는 접시
    CookManager cookManager;  //쿡 매니저.
    Transform tr;  //위치
    string message = "주세요.";
    string order;  //주문
    float maxTime; //최대시간
    float currTime;  //현재시간
    float currTimePercent;  //현재시간 비율
    bool isTimer;  //타이머 존재
    bool isOrdered;  //주문수락확인.

    //초밥 종류
    string sushiName1;
    string sushiName2;
    string sushiName3;
    //초밥 갯수
    int sushiCount1;
    int sushiCount2;
    int sushiCount3;
    //와사비 종류
    string wasabi1;
    string wasabi2;
    string wasabi3;

    private void Awake()
    {
        //쿡 매니저 찾기
        cookManager = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<CookManager>();
    }

    private void Start()
    {
        dish = GameObject.FindGameObjectWithTag("DISH").GetComponent<Dish>();  //접시 오브젝트 Find로 가져오기
        tr = GetComponent<Transform>();

        maxTime = 20;  //최대시간.
        currTime = maxTime;  //타이머 초기값 설정
    }

    void OnEnable()
    {
        int randomOrder = Random.Range(0, 3);  //주문할 종류 랜덤하게 결정.
        int randomSprite = Random.Range(0, sprites.Length);  //랜덤하게 그림 결정.
        GetComponent<Image>().sprite = sprites[randomSprite];  //랜덤한 그림 적용.

        switch (randomOrder)  //랜덤한 주문.
        {
            case 0:
                //초밥 한 종류. Random.Range를 이용해서 초밥종류, 갯수, 와사비를 결정.
                sushiName1 = sushis[Random.Range(0, sushis.Length)];
                sushiCount1 = Random.Range(1, 4);
                wasabi1 = wasabis[Random.Range(0, wasabis.Length)];
                order = sushiName1 + "초밥 " + "와사비 " + wasabi1 + " " + sushiCount1 + "개 " +  message;
                AddOrder(sushiName1, wasabi1, sushiCount1);  //랜덤하게 결정한 요소들을 주문 리스트에 추가
                orderTxt.text = order;  //주문 텍스트 출력.
                break;
            case 1:
                //초밥 두 종류.
                //The Fisher-Yates Shuffle 알고리즘을 이용하여 배열을 무작위로 섞고
                //섞인 상태에서 앞에서부터 순서대로 요소를 결정.
                for (int i = sushis.Length - 1; i > 0; i--)
                {
                    int randomIndex = Random.Range(0, i + 1);
                    // 요소를 서로 교환
                    string temp = sushis[i];
                    sushis[i] = sushis[randomIndex];
                    sushis[randomIndex] = temp;
                }
                sushiName1 = sushis[0];
                sushiName2 = sushis[1];
                sushiCount1 = Random.Range(1, 4);
                sushiCount2 = Random.Range(1, 4);
                wasabi1 = wasabis[Random.Range(0, wasabis.Length)];
                wasabi2 = wasabis[Random.Range(0, wasabis.Length)];
                order = sushiName1 + "초밥 " + "와사비 " + wasabi1 + " " + sushiCount1 + "개 " +  "\n" +
                            sushiName2 + "초밥 " + "와사비 " + wasabi2 + " " + sushiCount2 + "개 " +  message;
                //랜덤하게 결정한 요소들 주문 리스트에 추가.
                AddOrder(sushiName1, wasabi1, sushiCount1);
                AddOrder(sushiName2, wasabi2, sushiCount2);
                orderTxt.text = order;  //주문 텍스트 출력
                break;
            case 2:
                //초밥 세 종류.
                //The Fisher-Yates Shuffle 알고리즘을 이용하여 배열을 무작위로 섞고
                //섞인 상태에서 앞에서부터 순서대로 요소를 결정.
                for (int i = sushis.Length - 1; i > 0; i--)
                {
                    int randomIndex = Random.Range(0, i + 1);
                    // 요소를 서로 교환
                    string temp = sushis[i];
                    sushis[i] = sushis[randomIndex];
                    sushis[randomIndex] = temp;
                }
                sushiName1 = sushis[0];
                sushiName2 = sushis[1];
                sushiName3 = sushis[2];
                sushiCount1 = Random.Range(1, 3);
                sushiCount2 = Random.Range(1, 3);
                sushiCount3 = Random.Range(1, 3);
                wasabi1 = wasabis[Random.Range(0, wasabis.Length)];
                wasabi2 = wasabis[Random.Range(0, wasabis.Length)];
                wasabi3 = wasabis[Random.Range(0, wasabis.Length)];
                order = sushiName1 + "초밥 " + "와사비 " + wasabi1 + " " + sushiCount1 + "개 " + "\n" +
                            sushiName2 + "초밥 " + "와사비 " + wasabi2 + " " + sushiCount2 + "개 " +  "\n" +
                            sushiName3 + "초밥 " + "와사비 " + wasabi3 + " " + sushiCount3 + "개 " +  message;
                //랜덤하게 결정한 요소들 주문 리스트에 추가
                AddOrder(sushiName1, wasabi1, sushiCount1);
                AddOrder(sushiName2, wasabi2, sushiCount2);
                AddOrder(sushiName3, wasabi3, sushiCount3);
                orderTxt.text = order;  //주문 텍스트 출력.
                break;
        }
    }

    private void Update()
    {
        if (isTimer)
        {
            currTime -= Time.deltaTime;  //시간이 줄어듬
            currTimePercent = currTime / maxTime;  //남은시간 비율
            timer.fillAmount = currTimePercent;  //타이머는 남은시간 비율에 맞게 줄어듬

            //손님 타이머가 다 끝나면
            if (currTime <= 0)
            {
                print("님 평판 깎임.");
                int _score = int.Parse(GameManager.instance.save.score) - 20;  //평판 감소
                GameManager.instance.save.score = _score.ToString();
                GameManager.instance.Save("s");  //평판 저장
                //cookManager.UIUpdate();  //UI 최신화
                cookManager.ViewOrder();  //주문 창으로 넘어옴.
                cookManager.canMake = false;
                orderTxt.text = fail[0];  //실패 텍스트 출력.
                Destroy(gameObject, 3f);  //손님 삭제
                StartCoroutine(Move());  //손님 안 보이는 곳으로 옮기기.
                StartCoroutine(cookManager.Create());  //손님생성
                orders.Clear();  //주문 리스트 클리어.
                dish.sushiList.Clear();  //초밥 리스트 클리어.
                dish.sushiCounts.Clear();  //초밥 딕셔너리 클리어.
                dish.ClearSushi();  //접시 위 초밥 삭제.
                dish.ClearBoard();  //도마 위 초밥 삭제.
                isTimer = false;  //타이머 비활성화 상태로 판단
                isOrdered = false;  //주문을 안받았음.
            }
        }
    }

    public void ShowTimer()  //손님 타이머 활성화
    {
        cookManager.canMake = true;  //만들기 가능.
        isTimer = true;  //타이머 활성화 상태로 판단.
        isOrdered = true;  //주문을 받았음.
        dish.sushiCounts.Clear();  //초밥 딕셔너리 클리어.
        dish.sushiList.Clear();  //초밥 리스트 클리어.
        dish.ClearSushi();  //접시 위 초밥 삭제.
        timerObj.SetActive(true);  //타이머 활성화.
        yesBtn.SetActive(false);  //버튼 비활성화.
        noBtn.SetActive(false);  //버튼 비활성화.
    }

    public void NoBtn() //거절 버튼.
    {
        int _score = int.Parse(GameManager.instance.save.score) - 20;  //평판 감소
        GameManager.instance.save.score = _score.ToString();
        GameManager.instance.Save("s");  //평판 저장
        //cookManager.UIUpdate();  //UI 최신화
        orderTxt.text = fail[0];  //실패 텍스트 출력.
        Destroy(gameObject, 3f);  //손님 삭제
        StartCoroutine(Move());  //안보이는 곳으로 옮기기
        StartCoroutine(cookManager.Create());  //손님생성
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
            Dictionary<string, int> sushiCounts = dish.sushiCounts;  //접시 딕셔너리 (초밥의 종류, 갯수)
            List<Sushi> sushis = dish.sushiList;  //접시 리스트 (초밥의 가격)
            int totalPrice = 0;
            bool orderMatch = true;  //주문 일치
            cookManager.canMake = false;
            foreach (Order order in orders)
            {
                string sushiKey = order.sushiName + "_" + order.wasabi;  //주문 초밥 + 와사비를 키로.

                if (sushiCounts.ContainsKey(sushiKey))  //주문한 초밥 키와 일치하는게 접시 딕셔너리에 있는가?
                {
                    int sushiCount = sushiCounts[sushiKey];  //있다면 그 초밥 키의 갯수를 주문 갯수와 비교.
                    if (!(sushiCount == order.count))  //주문갯수와 초밥 갯수가 불일치한다면
                    {
                        orderMatch = false;  //불일치
                        Debug.Log($"주문과 초밥 정보 일치 - 종류: {order.sushiName}, 와사비: {order.wasabi}, 갯수: {order.count}");
                    }
                    else  //일치한다면
                    {
                        //접시 리스트 안에 주문과 일치하는 초밥있는지 확인
                        Sushi sushi = null;
                        foreach (Sushi item in sushis)
                        {
                            if (item.sushiName == order.sushiName && item.wasabi == order.wasabi)
                            {
                                sushi = item;
                                break;
                            }
                        }

                        if(currTimePercent > 0.5f)  //남은 시간이 반 이상일 때는
                        {
                            totalPrice += sushi.gold * order.count * 3;  //생선가격 * 주문갯수 * 3
                            print("골드 3배 : " + totalPrice);
                        }
                        else if(currTimePercent > 0)
                        {
                            totalPrice += sushi.gold * order.count * 2;  //생선가격 * 주문갯수 * 2
                            print("골드 2배 : " + totalPrice);
                            print(totalPrice);
                        }

                        int _gold = int.Parse(GameManager.instance.save.gold) + totalPrice;
                        GameManager.instance.save.gold = _gold.ToString();
                        GameManager.instance.Save("s");
                        //cookManager.UIUpdate();
                    }
                }
                else
                {
                    orderMatch = false;  //불일치
                    Debug.Log($"주문과 초밥 정보 불일치 - 종류: {order.sushiName}, 와사비: {order.wasabi}, 갯수: {order.count}");
                }
            }

            if (orderMatch)  //일치 시
            {
                if (currTimePercent > 0.5f)
                {
                    int _score = int.Parse(GameManager.instance.save.score) + 40;  //평판 증가
                    print("평판증가 :" + _score);
                    GameManager.instance.save.score = _score.ToString();
                }
                else if (currTimePercent > 0)
                {
                    int _score = int.Parse(GameManager.instance.save.score) + 20;
                    print("평판증가 :" + _score);
                    GameManager.instance.save.score = _score.ToString();
                }
                
                
                GameManager.instance.Save("s");  //평판 저장
                //cookManager.UIUpdate();
                orderTxt.text = success[Random.Range(0, success.Length)];
                Debug.Log("총 가격: " + totalPrice);
                orders.Clear();  //주문 리스트 클리어.
                dish.sushiCounts.Clear();  //초밥 딕셔너리 클리어.
                dish.sushiList.Clear();  //초밥 리스트 클리어.
                dish.ClearSushi();  //접시 위 초밥 삭제.
                Destroy(gameObject, 3f);  //손님 삭제
                StartCoroutine(Move());
                StartCoroutine(cookManager.Create());  //손님생성
            }
            else  //불일치 시
            {
                int _score = int.Parse(GameManager.instance.save.score) - 20;  //평판 감소
                GameManager.instance.save.score = _score.ToString();
                GameManager.instance.Save("s");  //평판 저장
                //cookManager.UIUpdate();
                orderTxt.text = fail[Random.Range(0, fail.Length)];
                orders.Clear();  //주문 리스트 클리어.
                dish.sushiCounts.Clear();  //초밥 딕셔너리 클리어.
                dish.sushiList.Clear();  //초밥 리스트 클리어.
                dish.ClearSushi();  //접시 위 초밥 삭제.
                Destroy(gameObject, 3f);  //손님 삭제
                StartCoroutine(Move());
                StartCoroutine(cookManager.Create());  //손님생성
            }
        }
    }

    IEnumerator Move()  //삭제 전 이동 메서드
    {
        yield return new WaitForSeconds(1f);
        tr.position = new Vector3(0, -3000, 0);
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
