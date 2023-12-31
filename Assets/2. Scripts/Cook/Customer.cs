using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public Text orderTxt;  //주문 텍스트
    public string[] sushis;  //초밥 종류 배열
    public string[] easySushis;  //매우 쉬운 초밥 종류 배열
    public string[] normalSushis;  //쉬운 초밥 종류 배열
    public string[] wasabis;  //와사비
    public string[] success;  //성공 문구.
    public string[] fail;  //실패 문구.
    public string message;  //말 끝
    public string orderRecipe;  //주문 내용 간추린거
    public GameObject yesBtn;  //수락 버튼
    public GameObject noBtn;  //거절 버튼
    public GameObject timerObj; //타이머 오브젝트
    public Image timer; //타이머 이미지

    Image[] images;  //이미지들
    Text[] texts; //텍스트들
    List<Order> orders = new List<Order>();  //주문을 담을 리스트
    Dish dish;  //초밥 담는 접시
    CookManager cookManager;  //쿡 매니저.
    AudioSource audioSource;

    string order;  //주문
    float maxTime; //최대시간
    float currTime;  //현재시간
    float currTimePercent;  //현재시간 비율
    bool isTimer;  //타이머 존재
    bool isOrdered;  //주문수락확인.
    int randomOrder;  //주문 랜덤

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
        audioSource = GetComponent<AudioSource>();
        //쿡 매니저 찾기
        cookManager = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<CookManager>();
        images = GetComponentsInChildren<Image>();
        texts = GetComponentsInChildren<Text>();
    }

    private void Start()
    {
        // StartCoroutine(cookManager.GameOverCoroutine());
        dish = GameObject.FindGameObjectWithTag("DISH").GetComponent<Dish>();  //접시 오브젝트 Find로 가져오기

        maxTime = int.Parse(GameManager.instance.data.customerTime);  //최대시간.
        currTime = maxTime;  //타이머 초기값 설정
    }

    void OnEnable()
    {
        StartCoroutine(FadeIn());  //나타나는 효과

        if (int.Parse(GameManager.instance.data.dateCount) <= 10)  //5일차 이하
        {
            randomOrder = 0;
           // print("이지 난이도");
        }
        else if (int.Parse(GameManager.instance.data.dateCount) > 10 && int.Parse(GameManager.instance.data.dateCount) <= 20)  //10일차 이하
        {
            randomOrder = 1;
        }
        else if (int.Parse(GameManager.instance.data.score) <= 600)  //평판 600점 이하
        {
            //80퍼, 17퍼, 3퍼
            RandomChance(80, 97);
        }
        else if (int.Parse(GameManager.instance.data.score) <= 900)  //평판 900점 이하
        {
            //60퍼, 30퍼, 10퍼
            RandomChance(60, 90);
        }
        else  //그 외
        {
            //20퍼, 40퍼, 40퍼
            RandomChance(20, 60);
        }

        switch (randomOrder)  //랜덤한 주문.
        {
            //이지 난이도
            case 0:
                sushiName1 = easySushis[Random.Range(0, easySushis.Length)];  //초밥이름
                sushiCount1 = Random.Range(1, 4);  //갯수
                wasabi1 = wasabis[Random.Range(0, wasabis.Length)];  //와사비
                order = sushiName1 + "초밥 " + "와사비 " + wasabi1 + " " + sushiCount1 + "개 " + message;
                AddOrder(sushiName1, wasabi1, sushiCount1);  //랜덤하게 결정한 요소들을 주문 리스트에 추가
                orderTxt.text = order;  //주문 텍스트 출력.
                orderRecipe = sushiName1 + ", " + wasabi1 + ", " + sushiCount1;
                cookManager.Order(orderRecipe);
                break;
            case 1:
                sushiName1 = normalSushis[Random.Range(0, normalSushis.Length)];  //초밥이름
                sushiCount1 = Random.Range(1, 4);  //갯수
                wasabi1 = wasabis[Random.Range(0, wasabis.Length)];  //와사비
                order = sushiName1 + "초밥 " + "와사비 " + wasabi1 + " " + sushiCount1 + "개 " + message;
                AddOrder(sushiName1, wasabi1, sushiCount1);  //랜덤하게 결정한 요소들을 주문 리스트에 추가
                orderTxt.text = order;  //주문 텍스트 출력.
                orderRecipe = sushiName1 + ", " + wasabi1 + ", " + sushiCount1;
                cookManager.Order(orderRecipe);
                break;
            case 2:
                //초밥 한 종류. Random.Range를 이용해서 초밥종류, 갯수, 와사비를 결정.
                sushiName1 = sushis[Random.Range(0, sushis.Length)];  //초밥이름
                sushiCount1 = Random.Range(1, 4);  //갯수
                wasabi1 = wasabis[Random.Range(0, wasabis.Length)];  //와사비
                order = sushiName1 + "초밥 " + "와사비 " + wasabi1 + " " + sushiCount1 + "개 " + message;
                AddOrder(sushiName1, wasabi1, sushiCount1);  //랜덤하게 결정한 요소들을 주문 리스트에 추가
                orderTxt.text = order;  //주문 텍스트 출력.
                orderRecipe = sushiName1 + ", " + wasabi1 + ", " + sushiCount1;
                cookManager.Order(orderRecipe);
                break;
            case 3:
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
                sushiName1 = sushis[0];  //초밥이름
                sushiName2 = sushis[1];
                sushiCount1 = Random.Range(1, 4);  //갯수
                sushiCount2 = Random.Range(1, 4);
                wasabi1 = wasabis[Random.Range(0, wasabis.Length)];  //와사비
                wasabi2 = wasabis[Random.Range(0, wasabis.Length)];
                order = sushiName1 + "초밥 " + "와사비 " + wasabi1 + " " + sushiCount1 + "개 " + "\n" +
                            sushiName2 + "초밥 " + "와사비 " + wasabi2 + " " + sushiCount2 + "개 " + message;
                //랜덤하게 결정한 요소들 주문 리스트에 추가.
                AddOrder(sushiName1, wasabi1, sushiCount1);
                AddOrder(sushiName2, wasabi2, sushiCount2);
                orderTxt.text = order;  //주문 텍스트 출력
                orderRecipe = sushiName1 + ", " + wasabi1 + ", " + sushiCount1 + "\n" +
                                       sushiName2 + ", " + wasabi2 + ", " + sushiCount2;
                cookManager.Order(orderRecipe);
                break;
            case 4:
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
                sushiName1 = sushis[0];  //초밥이름
                sushiName2 = sushis[1];
                sushiName3 = sushis[2];
                sushiCount1 = Random.Range(1, 3);  //갯수
                sushiCount2 = Random.Range(1, 3);
                sushiCount3 = Random.Range(1, 3);
                wasabi1 = wasabis[Random.Range(0, wasabis.Length)];  //와사비
                wasabi2 = wasabis[Random.Range(0, wasabis.Length)];
                wasabi3 = wasabis[Random.Range(0, wasabis.Length)];
                order = sushiName1 + "초밥 " + "와사비 " + wasabi1 + " " + sushiCount1 + "개 " + "\n" +
                            sushiName2 + "초밥 " + "와사비 " + wasabi2 + " " + sushiCount2 + "개 " + "\n" +
                            sushiName3 + "초밥 " + "와사비 " + wasabi3 + " " + sushiCount3 + "개 " + message;
                //랜덤하게 결정한 요소들 주문 리스트에 추가
                AddOrder(sushiName1, wasabi1, sushiCount1);
                AddOrder(sushiName2, wasabi2, sushiCount2);
                AddOrder(sushiName3, wasabi3, sushiCount3);
                orderTxt.text = order;  //주문 텍스트 출력.
                orderRecipe = sushiName1 + ", " + wasabi1 + ", " + sushiCount1 + "\n" +
                                       sushiName2 + ", " + wasabi2 + ", " + sushiCount2 + "\n" +
                                       sushiName3 + ", " + wasabi3 + ", " + sushiCount3;
                cookManager.Order(orderRecipe);
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
               // print("님 평판 깎임.");
                int _score = int.Parse(GameManager.instance.data.score) - 20;  //평판 감소
                GameManager.instance.data.score = _score.ToString();
                GameManager.instance.todayData.score -= 20;
                audioSource.PlayOneShot(SoundManager.instance.orderFail, 1);

                //평점 0이하로 떨어지면 게임오버 코루틴시작
                if (int.Parse(GameManager.instance.data.score) <= 0)
                {
                    StartCoroutine(cookManager.GameOverCoroutine());
                }

                cookManager.UIUpdate();  //UI 최신화
                cookManager.GoOrder();  //주문 창으로 넘어옴.
                cookManager.canMake = false;
                orderTxt.text = fail[0];  //실패 텍스트 출력.
                orders.Clear();  //주문 리스트 클리어.
                dish.sushiList.Clear();  //초밥 리스트 클리어.
                dish.sushiCounts.Clear();  //초밥 딕셔너리 클리어.
                dish.ClearSushi();  //접시 위 초밥 삭제.
                dish.ClearBoard();  //도마 위 초밥 삭제.
                StartCoroutine(FadeOut());
                isTimer = false;  //타이머 비활성화 상태로 판단
                isOrdered = false;  //주문을 안받았음.
            }
        }
    }

    public void ShowTimer()  //손님 타이머 활성화
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

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
        audioSource.PlayOneShot(SoundManager.instance.orderFail, 1);

        int _score = int.Parse(GameManager.instance.data.score) - 5;  //평판 감소
        GameManager.instance.data.score = _score.ToString();
        GameManager.instance.todayData.score -= 5;

        //평점 0이하로 떨어지면 게임오버 코루틴시작
        if (int.Parse(GameManager.instance.data.score) <= 0)
        {
            StartCoroutine(cookManager.GameOverCoroutine());
        }

        cookManager.UIUpdate();  //UI 최신화
        orderTxt.text = fail[0];  //실패 텍스트 출력.
        isOrdered = false;  //주문안받은 상태로
        StartCoroutine(FadeOut());
        yesBtn.SetActive(false);  //버튼 비활성화
        noBtn.SetActive(false);  //버튼 비활성화
    }

    void AddOrder(string sushiName, string wasabi, int count)  //주문 추가 메서드
    {
        Order order = new Order(sushiName, wasabi, count);  //초밥종류, 와사비, 갯수 정보저장.
        orders.Add(order);
    }

    public void CompareOrders()  //주문비교 메서드.
    {
        isTimer = false;

        //주문을 받은 상태에서만 작동
        if (isOrdered)
        {
            List<Order> orders = this.orders;  //주문 리스트
            Dictionary<string, int> sushiCounts = dish.sushiCounts;  //접시 딕셔너리 (초밥의 종류, 갯수)
            List<Sushi> sushis = dish.sushiList;  //접시 리스트 (초밥의 가격 불러올려고)
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
                        //Debug.Log($"주문과 초밥 정보 일치 - 종류: {order.sushiName}, 와사비: {order.wasabi}, 갯수: {order.count}");
                        break;
                    }
                    else
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
                        //시간이 반이상 남았으면
                        if (currTimePercent >= 0.5)
                        {
                            totalPrice += sushi.gold * order.count * 3;  //생선가격 * 주문갯수 * 4
                        }
                        else  //그 외
                        {
                            totalPrice += sushi.gold * order.count * 2;  //생선가격 * 주문갯수 * 2
                        }

                        int _gold = int.Parse(GameManager.instance.data.gold) + totalPrice;
                        GameManager.instance.data.gold = _gold.ToString();
                        GameManager.instance.todayData.gold += totalPrice;
                        cookManager.priceTxt.text = totalPrice.ToString("N0");
                        audioSource.PlayOneShot(SoundManager.instance.orderSuccess, 1);
                    }
                }
                else //없다면
                {
                    orderMatch = false;  //불일치
                    //Debug.Log($"주문과 초밥 정보 불일치 - 종류: {order.sushiName}, 와사비: {order.wasabi}, 갯수: {order.count}");
                    break;
                }
            }

            if (orderMatch)  //일치 시
            {
                int _score = int.Parse(GameManager.instance.data.score) + 20;  //평판 증가
                GameManager.instance.data.score = _score.ToString();
                GameManager.instance.todayData.score += 20;

                cookManager.UIUpdate();
                orderTxt.text = success[Random.Range(0, success.Length)];  //성공 멘트 랜덤
                orders.Clear();  //주문 리스트 클리어.
                dish.sushiCounts.Clear();  //초밥 딕셔너리 클리어.
                dish.sushiList.Clear();  //초밥 리스트 클리어.
                dish.ClearSushi();  //접시 위 초밥 삭제.
                StartCoroutine(FadeOut());  //사라지게 하기
                isOrdered = false;  //주문 안받은 상태
            }
            else  //불일치 시
            {
                int _score = int.Parse(GameManager.instance.data.score) - 20;  //평판 감소
                GameManager.instance.data.score = _score.ToString();
                GameManager.instance.todayData.score -= 20;
                //평점 0이하로 떨어지면 게임오버 코루틴시작
                if (int.Parse(GameManager.instance.data.score) <= 0)
                {
                    StartCoroutine(cookManager.GameOverCoroutine());
                }

                audioSource.PlayOneShot(SoundManager.instance.orderFail, 1);
                cookManager.UIUpdate();
                orderTxt.text = fail[Random.Range(0, fail.Length)];  //실패 멘트 랜덤
                orders.Clear();  //주문 리스트 클리어.
                dish.sushiCounts.Clear();  //초밥 딕셔너리 클리어.
                dish.sushiList.Clear();  //초밥 리스트 클리어.
                dish.ClearSushi();  //접시 위 초밥 삭제.
                StartCoroutine(FadeOut());  //사라지게 하기
                isOrdered = false;  //주문 안받은 상태
            }
        }
    }

    void RandomChance(int num1, int num2)  //주문 패턴 확률 구현메서드
    {
        int randomNum = Random.Range(1, 101);
        //인수1 보다 작으면
        if (randomNum <= num1)
        {
            randomOrder = 2;  //한 줄 패턴
        }
        //인수2 보다 작으면
        else if (randomNum <= num2)
        {
            randomOrder = 3;  //두 줄 패턴
        }
        else  //그외
        {
            randomOrder = 4;  //세 줄 패턴
        }
    }

    IEnumerator FadeIn() //나타나는 효과
    {
        Vector2 initPos = cookManager.customerStartPos;
        Vector2 targetPos = initPos + new Vector2(30, 0);
        //구성 이미지 투명
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, 0);
        }
        //구성 텍스트 투명
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].color = new Color(texts[i].color.r, texts[i].color.g, texts[i].color.b, 0);
        }
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // 시간 비율 계산
            for (int i = 0; i < images.Length; i++)
            {
                images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, t);
            }

            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].color = new Color(texts[i].color.r, texts[i].color.g, texts[i].color.b, t);
            }

            this.transform.localPosition = Vector2.Lerp(initPos, targetPos, t);
            yield return null;
        }
    }

    IEnumerator FadeOut()  //사라지는 효과
    {
        timer.gameObject.SetActive(false);  //타이머 지우기
        yield return new WaitForSeconds(1);  //실패멘트를 볼 수 있게 잠시 텀을 주기
        Vector2 initPos = this.transform.localPosition;
        Vector2 targetPos = new Vector2(this.transform.localPosition.x - 30, this.transform.localPosition.y);
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // 시간 비율 계산
            float alpha = 1 - t;

            for (int i = 0; i < images.Length; i++)
            {
                images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, alpha);
            }

            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].color = new Color(texts[i].color.r, texts[i].color.g, texts[i].color.b, alpha);
            }
            this.transform.localPosition = Vector2.Lerp(initPos, targetPos, t);
            yield return null;
        }

        StartCoroutine(cookManager.Create());  //사라지고 다음 손님 생성
        cookManager.priceTxt.text = "0";  
        Destroy(this.gameObject, 2.5f);  //생성될 동안 대기 후 삭제
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
