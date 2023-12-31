using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCustomer : MonoBehaviour
{
    public Text orderTxt;  //주문 텍스트
    public string[] sushis;  //초밥 종류 배열
    public string[] wasabis;  //와사비
    public string[] success;  //성공 문구.
    public string[] fail;  //실패 문구.
    public string orderRecipe;
    public GameObject yesBtn;  //수락 버튼
    public GameObject noBtn;  //거절 버튼
    public GameObject timerObj; //타이머 오브젝트
    public Image timer; //타이머 이미지

    Image[] images;  //이미지
    Text[] texts; //텍스트
    List<Order> orders = new List<Order>();  //주문을 담을 리스트
    Dish dish;  //초밥 담는 접시
    TutorialCook tc;  //쿡 매니저.
    AudioSource audioSource;
    string message = "주세요.";
    string order;  //주문
    //float maxTime; //최대시간
    //float currTime;  //현재시간
    //float currTimePercent;  //현재시간 비율
    //bool isTimer;  //타이머 존재
    bool isOrdered;  //주문수락확인.
    int orderIndex;

    //초밥 종류
    string sushiName1;
    string sushiName2;
    //초밥 갯수
    int sushiCount1;
    int sushiCount2;
    //와사비 종류
    string wasabi1;
    string wasabi2;

    void Awake()
    {
        tc = GameObject.FindGameObjectWithTag("TC").GetComponent<TutorialCook>();
        images = GetComponentsInChildren<Image>();
        texts = GetComponentsInChildren<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        dish = GameObject.FindGameObjectWithTag("DISH").GetComponent<Dish>();  //접시 오브젝트 Find로 가져오기
        //maxTime = 20;  //최대시간.
        //currTime = maxTime;  //타이머 초기값 설정
    }

    void OnEnable()
    {
        StartCoroutine(FadeIn());
        orderIndex = Random.Range(0, 2);

        switch (orderIndex)
        {
            case 0:
                //초밥 한 종류. Random.Range를 이용해서 초밥종류, 갯수, 와사비를 결정.
                sushiName1 = sushis[Random.Range(0, sushis.Length)];
                sushiCount1 = 2;
                wasabi1 = wasabis[1];
                order = sushiName1 + "초밥 " + "와사비 " + wasabi1 + " " + sushiCount1 + "개 " + message;
                AddOrder(sushiName1, wasabi1, sushiCount1);  //랜덤하게 결정한 요소들을 주문 리스트에 추가
                orderTxt.text = order;  //주문 텍스트 출력.
                orderRecipe = sushiName1 + ", " + wasabi1 + ", " + sushiCount1;
                tc.Order(orderRecipe);
                break;
            case 1:
                sushiName1 = sushis[1];
                sushiName2 = sushis[2];
                sushiCount1 = 1;
                sushiCount2 = 2;
                wasabi1 = wasabis[Random.Range(0, wasabis.Length)];
                wasabi2 = wasabis[Random.Range(0, wasabis.Length)];
                order = sushiName1 + "초밥 " + "와사비 " + wasabi1 + " " + sushiCount1 + "개 " + "\n" +
                            sushiName2 + "초밥 " + "와사비 " + wasabi2 + " " + sushiCount2 + "개 " + message;
                //랜덤하게 결정한 요소들 주문 리스트에 추가.
                AddOrder(sushiName1, wasabi1, sushiCount1);
                AddOrder(sushiName2, wasabi2, sushiCount2);
                orderTxt.text = order;  //주문 텍스트 출력
                orderRecipe = sushiName1 + ", " + wasabi1 + ", " + sushiCount1 + "\n" +
                                       sushiName2 + ", " + wasabi2 + ", " + sushiCount2;
                tc.Order(orderRecipe);
                break;
        }
    }

    public void ShowTimer()  //손님 타이머 활성화
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        tc.canMake = true;  //만들기 가능.
        //isTimer = true;  //타이머 활성화 상태로 판단.
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
        orderTxt.text = fail[0];  //실패 텍스트 출력.
        isOrdered = false;
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
        if (isOrdered)
        {
            List<Order> orders = this.orders;  //주문 리스트
            Dictionary<string, int> sushiCounts = dish.sushiCounts;  //접시 딕셔너리 (초밥의 종류, 갯수)
            List<Sushi> sushis = dish.sushiList;  //접시 리스트 (초밥의 가격)
            int totalPrice = 0;
            bool orderMatch = true;  //주문 일치
            tc.canMake = false;
            foreach (Order order in orders)
            {
                string sushiKey = order.sushiName + "_" + order.wasabi;  //주문 초밥 + 와사비를 키로.

                if (sushiCounts.ContainsKey(sushiKey))  //주문한 초밥 키와 일치하는게 접시 딕셔너리에 있는가?
                {
                    int sushiCount = sushiCounts[sushiKey];  //있다면 그 초밥 키의 갯수를 주문 갯수와 비교.
                    if (!(sushiCount == order.count))  //주문갯수와 초밥 갯수가 일치한다면
                    {
                        orderMatch = false;  //불일치
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

                        totalPrice += sushi.gold * order.count * 2;  //생선가격 * 주문갯수 * 2
                        tc.priceTxt.text = totalPrice.ToString("N0");

                        audioSource.PlayOneShot(SoundManager.instance.orderSuccess, 1);
                    }

                }
                else
                {
                    orderMatch = false;  //불일치
                    break;
                }
            }

            if (orderMatch)  //일치 시
            {
                orderTxt.text = success[Random.Range(0, success.Length)];
                orders.Clear();  //주문 리스트 클리어.
                dish.sushiCounts.Clear();  //초밥 딕셔너리 클리어.
                dish.sushiList.Clear();  //초밥 리스트 클리어.
                dish.ClearSushi();  //접시 위 초밥 삭제.
                StartCoroutine(FadeOut());
                isOrdered = false;
                tc.sucess = true;
            }
            else  //불일치 시
            {
                audioSource.PlayOneShot(SoundManager.instance.orderFail, 1);
                orderTxt.text = fail[Random.Range(0, fail.Length)];
                orders.Clear();  //주문 리스트 클리어.
                dish.sushiCounts.Clear();  //초밥 딕셔너리 클리어.
                dish.sushiList.Clear();  //초밥 리스트 클리어.
                dish.ClearSushi();  //접시 위 초밥 삭제.
                StartCoroutine(FadeOut());
                isOrdered = false;
                tc.sucess = true;
            }
        }
    }

    IEnumerator FadeIn()
    {
        Vector2 initPos = tc.customerStartPos;
        Vector2 targetPos = initPos + new Vector2(30, 0);
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, 0);
        }

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
        tc.isCustomer = true;
    }

    IEnumerator FadeOut()
    {
        timer.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
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

        StartCoroutine(tc.Create());
        tc.priceTxt.text = "0";
        Destroy(this.gameObject, 2.5f);
    }
}
