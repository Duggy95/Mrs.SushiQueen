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
    public GameObject yesBtn;  //���� ��ư
    public GameObject noBtn;  //���� ��ư
    public GameObject timerObj; //Ÿ�̸� ������Ʈ
    public Image timer; //Ÿ�̸� �̹���
    public int orderIndex;
    public int num;
    List<Order> orders = new List<Order>();  //�ֹ��� ���� ����Ʈ

    Dish dish;  //�ʹ� ��� ����
    TutorialCook tc;  //�� �Ŵ���.
    Transform tr;  //��ġ
    string message = "�ּ���.";
    string order;  //�ֹ�
    float maxTime; //�ִ�ð�
    float currTime;  //����ð�
    float currTimePercent;  //����ð� ����
    bool isTimer;  //Ÿ�̸� ����
    bool isOrdered;  //�ֹ�����Ȯ��.

    //�ʹ� ����
    string sushiName1;
    string sushiName2;
    string sushiName3;
    //�ʹ� ����
    int sushiCount1;
    int sushiCount2;
    int sushiCount3;
    //�ͻ�� ����
    string wasabi1;
    string wasabi2;
    string wasabi3;

    void Awake()
    {
        tc = GameObject.FindGameObjectWithTag("TC").GetComponent<TutorialCook>();
    }

    void Start()
    {
        dish = GameObject.FindGameObjectWithTag("DISH").GetComponent<Dish>();  //���� ������Ʈ Find�� ��������
        tr = GetComponent<Transform>();

        maxTime = 20;  //�ִ�ð�.
        currTime = maxTime;  //Ÿ�̸� �ʱⰪ ����

        switch(orderIndex)
        {
            case 0:
                //�ʹ� �� ����. Random.Range�� �̿��ؼ� �ʹ�����, ����, �ͻ�� ����.
                sushiName1 = sushis[0];
                sushiCount1 = 1;
                wasabi1 = wasabis[1];
                order = sushiName1 + "�ʹ� " + "�ͻ�� " + wasabi1 + " " + sushiCount1 + "�� " + message;
                AddOrder(sushiName1, wasabi1, sushiCount1);  //�����ϰ� ������ ��ҵ��� �ֹ� ����Ʈ�� �߰�
                orderTxt.text = order;  //�ֹ� �ؽ�Ʈ ���.
                break;
            case 1:
                 sushiName1 = sushis[1];
                sushiName2 = sushis[2];
                sushiCount1 = 2;
                sushiCount2 = 2;
                wasabi1 = wasabis[Random.Range(0, wasabis.Length)];
                wasabi2 = wasabis[Random.Range(0, wasabis.Length)];
                order = sushiName1 + "�ʹ� " + "�ͻ�� " + wasabi1 + " " + sushiCount1 + "�� " + "\n" +
                            sushiName2 + "�ʹ� " + "�ͻ�� " + wasabi2 + " " + sushiCount2 + "�� " + message;
                //�����ϰ� ������ ��ҵ� �ֹ� ����Ʈ�� �߰�.
                AddOrder(sushiName1, wasabi1, sushiCount1);
                AddOrder(sushiName2, wasabi2, sushiCount2);
                orderTxt.text = order;  //�ֹ� �ؽ�Ʈ ���
                break;
        }
    }

    /*void OnEnable()
    {
        int randomOrder = Random.Range(0, 2);  //�ֹ��� ���� �����ϰ� ����.
        int randomSprite = Random.Range(0, sprites.Length);  //�����ϰ� �׸� ����.
        GetComponent<Image>().sprite = sprites[randomSprite];  //������ �׸� ����.

        switch (randomOrder)  //������ �ֹ�.
        {
            case 0:
                //�ʹ� �� ����. Random.Range�� �̿��ؼ� �ʹ�����, ����, �ͻ�� ����.
                sushiName1 = sushis[tc.count];
                sushiCount1 = 2;
                wasabi1 = wasabis[Random.Range(1, wasabis.Length)];
                order = sushiName1 + "�ʹ� " + "�ͻ�� " + wasabi1 + " " + sushiCount1 + "�� " + message;
                AddOrder(sushiName1, wasabi1, sushiCount1);  //�����ϰ� ������ ��ҵ��� �ֹ� ����Ʈ�� �߰�
                orderTxt.text = order;  //�ֹ� �ؽ�Ʈ ���.
                break;
            case 1:
                //�ʹ� �� ����.
                //The Fisher-Yates Shuffle �˰����� �̿��Ͽ� �迭�� �������� ����
                //���� ���¿��� �տ������� ������� ��Ҹ� ����.
                *//*for (int i = sushis.Length - 1; i > 0; i--)
                {
                    int randomIndex = Random.Range(0, i + 1);
                    // ��Ҹ� ���� ��ȯ
                    string temp = sushis[i];
                    sushis[i] = sushis[randomIndex];
                    sushis[randomIndex] = temp;
                }*//*
                sushiName1 = sushis[1];
                sushiName2 = sushis[2];
                sushiCount1 = 2;
                sushiCount2 = 2;
                wasabi1 = wasabis[Random.Range(0, wasabis.Length)];
                wasabi2 = wasabis[Random.Range(0, wasabis.Length)];
                order = sushiName1 + "�ʹ� " + "�ͻ�� " + wasabi1 + " " + sushiCount1 + "�� " + "\n" +
                            sushiName2 + "�ʹ� " + "�ͻ�� " + wasabi2 + " " + sushiCount2 + "�� " + message;
                //�����ϰ� ������ ��ҵ� �ֹ� ����Ʈ�� �߰�.
                AddOrder(sushiName1, wasabi1, sushiCount1);
                AddOrder(sushiName2, wasabi2, sushiCount2);
                orderTxt.text = order;  //�ֹ� �ؽ�Ʈ ���
                break;
        }
    }*/

    void Update()
    {
        if (isTimer)
        {
            //currTime -= Time.deltaTime;  //�ð��� �پ��
            currTimePercent = currTime / maxTime;  //�����ð� ����
            timer.fillAmount = currTimePercent;  //Ÿ�̸Ӵ� �����ð� ������ �°� �پ��

            //�մ� Ÿ�̸Ӱ� �� ������
            if (currTime <= 0)
            {
                print("�� ���� ����.");
                //int _score = int.Parse(GameManager.instance.data.score) - 20;  //���� ����
                //GameManager.instance.data.score = _score.ToString();
                //GameManager.instance.Save("s");  //���� ����
                //cookManager.UIUpdate();  //UI �ֽ�ȭ
                tc.ViewOrder();  //�ֹ� â���� �Ѿ��.
                tc.canMake = false;
                orderTxt.text = fail[0];  //���� �ؽ�Ʈ ���.
                Destroy(gameObject, 3f);  //�մ� ����
                StartCoroutine(tc.Create(num + 1));  //�մԻ���
                orders.Clear();  //�ֹ� ����Ʈ Ŭ����.
                dish.sushiList.Clear();  //�ʹ� ����Ʈ Ŭ����.
                dish.sushiCounts.Clear();  //�ʹ� ��ųʸ� Ŭ����.
                dish.ClearSushi();  //���� �� �ʹ� ����.
                dish.ClearBoard();  //���� �� �ʹ� ����.
                isTimer = false;  //Ÿ�̸� ��Ȱ��ȭ ���·� �Ǵ�
                isOrdered = false;  //�ֹ��� �ȹ޾���.
            }
        }
    }

    public void ShowTimer()  //�մ� Ÿ�̸� Ȱ��ȭ
    {
        tc.canMake = true;  //����� ����.
        isTimer = true;  //Ÿ�̸� Ȱ��ȭ ���·� �Ǵ�.
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
        int _score = int.Parse(GameManager.instance.data.score) - 20;  //���� ����
        //GameManager.instance.data.score = _score.ToString();
        //GameManager.instance.Save("s");  //���� ����
        //cookManager.UIUpdate();  //UI �ֽ�ȭ
        orderTxt.text = fail[0];  //���� �ؽ�Ʈ ���.
        Destroy(gameObject, 3f);  //�մ� ����
        StartCoroutine(tc.Create(num + 1));  //�մԻ���
        yesBtn.SetActive(false);  //��ư ��Ȱ��ȭ
        noBtn.SetActive(false);  //��ư ��Ȱ��ȭ
        print("�� ���� ����");
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
                        int _gold = int.Parse(GameManager.instance.data.gold) + totalPrice;
                        //GameManager.instance.data.gold = _gold.ToString();
                        //GameManager.instance.Save("s");
                        //cookManager.UIUpdate();
                    }
                }
                else
                {
                    orderMatch = false;  //����ġ
                    Debug.Log($"�ֹ��� �ʹ� ���� ����ġ - ����: {order.sushiName}, �ͻ��: {order.wasabi}, ����: {order.count}");
                }
            }

            if (orderMatch)  //��ġ ��
            {
                //int _score = int.Parse(GameManager.instance.data.score) + 20;  //���� ����
                //GameManager.instance.data.score = _score.ToString();
                //GameManager.instance.Save("s");  //���� ����
                //cookManager.UIUpdate();
                orderTxt.text = success[Random.Range(0, success.Length)];
                Debug.Log("�� ����: " + totalPrice);
                orders.Clear();  //�ֹ� ����Ʈ Ŭ����.
                dish.sushiCounts.Clear();  //�ʹ� ��ųʸ� Ŭ����.
                dish.sushiList.Clear();  //�ʹ� ����Ʈ Ŭ����.
                dish.ClearSushi();  //���� �� �ʹ� ����.
                Destroy(gameObject, 3f);  //�մ� ����
                StartCoroutine(tc.Create(num + 1));  //�մԻ���
            }
            else  //����ġ ��
            {
                //int _score = int.Parse(GameManager.instance.data.score) - 20;  //���� ����
                //GameManager.instance.data.score = _score.ToString();
                //GameManager.instance.Save("s");  //���� ����
                //cookManager.UIUpdate();
                orderTxt.text = fail[Random.Range(0, fail.Length)];
                orders.Clear();  //�ֹ� ����Ʈ Ŭ����.
                dish.sushiCounts.Clear();  //�ʹ� ��ųʸ� Ŭ����.
                dish.sushiList.Clear();  //�ʹ� ����Ʈ Ŭ����.
                dish.ClearSushi();  //���� �� �ʹ� ����.
                Destroy(gameObject, 3f);  //�մ� ����
                StartCoroutine(tc.Create(num + 1));  //�մԻ���
            }
        }
    }
}
