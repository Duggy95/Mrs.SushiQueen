using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCustomer : MonoBehaviour
{
    public Text orderTxt;  //�ֹ� �ؽ�Ʈ
    public string[] sushis;  //�ʹ� ���� �迭
    public string[] wasabis;  //�ͻ��
    public string[] success;  //���� ����.
    public string[] fail;  //���� ����.
    public string orderRecipe;
    public GameObject yesBtn;  //���� ��ư
    public GameObject noBtn;  //���� ��ư
    public GameObject timerObj; //Ÿ�̸� ������Ʈ
    public Image timer; //Ÿ�̸� �̹���

    Image[] images;  //�̹���
    Text[] texts; //�ؽ�Ʈ
    List<Order> orders = new List<Order>();  //�ֹ��� ���� ����Ʈ
    Dish dish;  //�ʹ� ��� ����
    TutorialCook tc;  //�� �Ŵ���.
    AudioSource audioSource;
    string message = "�ּ���.";
    string order;  //�ֹ�
    //float maxTime; //�ִ�ð�
    //float currTime;  //����ð�
    //float currTimePercent;  //����ð� ����
    //bool isTimer;  //Ÿ�̸� ����
    bool isOrdered;  //�ֹ�����Ȯ��.
    int orderIndex;

    //�ʹ� ����
    string sushiName1;
    string sushiName2;
    //�ʹ� ����
    int sushiCount1;
    int sushiCount2;
    //�ͻ�� ����
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
        dish = GameObject.FindGameObjectWithTag("DISH").GetComponent<Dish>();  //���� ������Ʈ Find�� ��������
        //maxTime = 20;  //�ִ�ð�.
        //currTime = maxTime;  //Ÿ�̸� �ʱⰪ ����
    }

    void OnEnable()
    {
        StartCoroutine(FadeIn());
        orderIndex = Random.Range(0, 2);

        switch (orderIndex)
        {
            case 0:
                //�ʹ� �� ����. Random.Range�� �̿��ؼ� �ʹ�����, ����, �ͻ�� ����.
                sushiName1 = sushis[Random.Range(0, sushis.Length)];
                sushiCount1 = 2;
                wasabi1 = wasabis[1];
                order = sushiName1 + "�ʹ� " + "�ͻ�� " + wasabi1 + " " + sushiCount1 + "�� " + message;
                AddOrder(sushiName1, wasabi1, sushiCount1);  //�����ϰ� ������ ��ҵ��� �ֹ� ����Ʈ�� �߰�
                orderTxt.text = order;  //�ֹ� �ؽ�Ʈ ���.
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
                order = sushiName1 + "�ʹ� " + "�ͻ�� " + wasabi1 + " " + sushiCount1 + "�� " + "\n" +
                            sushiName2 + "�ʹ� " + "�ͻ�� " + wasabi2 + " " + sushiCount2 + "�� " + message;
                //�����ϰ� ������ ��ҵ� �ֹ� ����Ʈ�� �߰�.
                AddOrder(sushiName1, wasabi1, sushiCount1);
                AddOrder(sushiName2, wasabi2, sushiCount2);
                orderTxt.text = order;  //�ֹ� �ؽ�Ʈ ���
                orderRecipe = sushiName1 + ", " + wasabi1 + ", " + sushiCount1 + "\n" +
                                       sushiName2 + ", " + wasabi2 + ", " + sushiCount2;
                tc.Order(orderRecipe);
                break;
        }
    }

    void Update()
    {
        /*if (isTimer)
        {
            currTime -= Time.deltaTime;  //�ð��� �پ��
            currTimePercent = currTime / maxTime;  //�����ð� ����
            timer.fillAmount = currTimePercent;  //Ÿ�̸Ӵ� �����ð� ������ �°� �پ��

            //�մ� Ÿ�̸Ӱ� �� ������
            if (currTime <= 0)
            {
                //cookManager.UIUpdate();  //UI �ֽ�ȭ
                tc.GoOrder();  //�ֹ� â���� �Ѿ��.
                tc.canMake = false;
                orderTxt.text = fail[0];  //���� �ؽ�Ʈ ���.
                orders.Clear();  //�ֹ� ����Ʈ Ŭ����.
                dish.sushiList.Clear();  //�ʹ� ����Ʈ Ŭ����.
                dish.sushiCounts.Clear();  //�ʹ� ��ųʸ� Ŭ����.
                dish.ClearSushi();  //���� �� �ʹ� ����.
                dish.ClearBoard();  //���� �� �ʹ� ����.
                StartCoroutine(FadeOut());
                isTimer = false;  //Ÿ�̸� ��Ȱ��ȭ ���·� �Ǵ�
                isOrdered = false;  //�ֹ��� �ȹ޾���.
            }
        }*/
    }

    public void ShowTimer()  //�մ� Ÿ�̸� Ȱ��ȭ
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        tc.canMake = true;  //����� ����.
        //isTimer = true;  //Ÿ�̸� Ȱ��ȭ ���·� �Ǵ�.
        isOrdered = true;  //�ֹ��� �޾���.
        dish.sushiCounts.Clear();  //�ʹ� ��ųʸ� Ŭ����.
        dish.sushiList.Clear();  //�ʹ� ����Ʈ Ŭ����.
        dish.ClearSushi();  //���� �� �ʹ� ����.
        timerObj.SetActive(true);  //Ÿ�̸� Ȱ��ȭ.
        yesBtn.SetActive(false);  //��ư ��Ȱ��ȭ.
        noBtn.SetActive(false);  //��ư ��Ȱ��ȭ.
    }

    public void NoBtn() //���� ��ư.
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        orderTxt.text = fail[0];  //���� �ؽ�Ʈ ���.
        isOrdered = false;
        StartCoroutine(FadeOut());
        yesBtn.SetActive(false);  //��ư ��Ȱ��ȭ
        noBtn.SetActive(false);  //��ư ��Ȱ��ȭ
    }

    void AddOrder(string sushiName, string wasabi, int count)  //�ֹ� �߰� �޼���
    {
        Order order = new Order(sushiName, wasabi, count);  //�ʹ�����, �ͻ��, ���� ��������.
        orders.Add(order);
    }

    public void CompareOrders()  //�ֹ��� �޼���.
    {
        if (isOrdered)
        {
            List<Order> orders = this.orders;  //�ֹ� ����Ʈ
            Dictionary<string, int> sushiCounts = dish.sushiCounts;  //���� ��ųʸ� (�ʹ��� ����, ����)
            List<Sushi> sushis = dish.sushiList;  //���� ����Ʈ (�ʹ��� ����)
            int totalPrice = 0;
            bool orderMatch = true;  //�ֹ� ��ġ
            tc.canMake = false;
            foreach (Order order in orders)
            {
                string sushiKey = order.sushiName + "_" + order.wasabi;  //�ֹ� �ʹ� + �ͻ�� Ű��.

                if (sushiCounts.ContainsKey(sushiKey))  //�ֹ��� �ʹ� Ű�� ��ġ�ϴ°� ���� ��ųʸ��� �ִ°�?
                {
                    int sushiCount = sushiCounts[sushiKey];  //�ִٸ� �� �ʹ� Ű�� ������ �ֹ� ������ ��.
                    if (!(sushiCount == order.count))  //�ֹ������� �ʹ� ������ ��ġ�Ѵٸ�
                    {
                        orderMatch = false;  //����ġ
                        Debug.Log($"�ֹ��� �ʹ� ���� ��ġ - ����: {order.sushiName}, �ͻ��: {order.wasabi}, ����: {order.count}");
                        break;
                    }
                    else
                    {
                        //���� ����Ʈ �ȿ� �ֹ��� ��ġ�ϴ� �ʹ��ִ��� Ȯ��
                        Sushi sushi = null;
                        foreach (Sushi item in sushis)
                        {
                            if (item.sushiName == order.sushiName && item.wasabi == order.wasabi)
                            {
                                sushi = item;
                                break;
                            }
                        }

                        totalPrice += sushi.gold * order.count * 2;  //�������� * �ֹ����� * 2
                        tc.priceTxt.text = totalPrice.ToString("N0");

                        audioSource.PlayOneShot(SoundManager.instance.orderSuccess, 1);
                    }

                }
                else
                {
                    orderMatch = false;  //����ġ
                    Debug.Log($"�ֹ��� �ʹ� ���� ����ġ - ����: {order.sushiName}, �ͻ��: {order.wasabi}, ����: {order.count}");
                    break;
                }
            }

            if (orderMatch)  //��ġ ��
            {
                orderTxt.text = success[Random.Range(0, success.Length)];
                Debug.Log("�� ����: " + totalPrice);
                orders.Clear();  //�ֹ� ����Ʈ Ŭ����.
                dish.sushiCounts.Clear();  //�ʹ� ��ųʸ� Ŭ����.
                dish.sushiList.Clear();  //�ʹ� ����Ʈ Ŭ����.
                dish.ClearSushi();  //���� �� �ʹ� ����.
                StartCoroutine(FadeOut());
                isOrdered = false;
                tc.sucess = true;
            }
            else  //����ġ ��
            {
                orderTxt.text = fail[Random.Range(0, fail.Length)];
                orders.Clear();  //�ֹ� ����Ʈ Ŭ����.
                dish.sushiCounts.Clear();  //�ʹ� ��ųʸ� Ŭ����.
                dish.sushiList.Clear();  //�ʹ� ����Ʈ Ŭ����.
                dish.ClearSushi();  //���� �� �ʹ� ����.
                StartCoroutine(FadeOut());
                isOrdered = false;
                tc.sucess = true;
            }
        }
    }

    IEnumerator FadeIn()
    {
        Vector2 initPos = tc.customerStartPos;
        print(initPos);
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
            float t = Mathf.Clamp01(elapsedTime / duration); // �ð� ���� ���
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
            float t = Mathf.Clamp01(elapsedTime / duration); // �ð� ���� ���
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
