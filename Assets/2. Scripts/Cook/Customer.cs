using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public Text orderTxt;  //�ֹ� �ؽ�Ʈ
    public Sprite[] sprites;  //�մ� ��������Ʈ �迭
    public string[] sushis;  //�ʹ� ���� �迭
    public string[] wasabis;  //�ͻ��
    public GameObject yesBtn;  //���� ��ư
    public GameObject noBtn;  //���� ��ư
    public GameObject timerObj; //Ÿ�̸� ������Ʈ
    public Image timer; //Ÿ�̸� �̹���
    List<Order> orders = new List<Order>();  //�ֹ��� ���� ����Ʈ

    Dish dish;  //�ʹ� ��� ����
    CookManager cookManager;
    string message = "�ּ���.";
    string order;  //�ֹ�
    float maxTime; //�ִ�ð�
    float currTime;  //����ð�
    float currTimePercent;  //����ð� ����
    bool isTimer;  //Ÿ�̸� ����
    bool isOrdered;  //�ֹ�����Ȯ��.

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
        dish = GameObject.FindGameObjectWithTag("DISH").GetComponent<Dish>();  //���� ������Ʈ Find�� ��������

        maxTime = 15;  //�ִ�ð�.
        currTime = maxTime;  //Ÿ�̸� �ʱⰪ ����
    }

    void OnEnable()
    {
        //int randomOrder = Random.Range(0, 3);
        int randomOrder = 0;
        int randomSprite = Random.Range(0, sprites.Length);  //�����ϰ� �׸� ����.
        GetComponent<Image>().sprite = sprites[randomSprite];  //������ �׸� ����.


        int randomSushi = Random.Range(0, sushis.Length);  //�����ϰ� �ʹ����� ����.
        //int randomWasabi = Random.Range(0, wasabis.Length);
        //int randomWasabi = 1;
        int count = Random.Range(1, 4);

        switch (randomOrder)  //�ʹ��� �ֹ� ������ ����.
        {
            case 0:
                //�ʹ� �� ����.
                sushiName1 = sushis[Random.Range(0, sushis.Length)];
                sushiCount1 = Random.Range(1, 4);
                wasabi1 = wasabis[Random.Range(0, wasabis.Length)];
                order = sushiName1 + "�ʹ� " + sushiCount1 + "�� " + "�ͻ�� " + wasabi1 + message;
                AddOrder(sushiName1, wasabi1, sushiCount1);
                orderTxt.text = order;
                break;
            case 1:
                //�ʹ� �� ����.
                sushiName1 = sushis[Random.Range(0, sushis.Length)];
                sushiName2 = sushis[Random.Range(0, sushis.Length)];
                sushiCount1 = Random.Range(1, 4);
                sushiCount2 = Random.Range(1, 4);
                wasabi1 = wasabis[Random.Range(0, wasabis.Length)];
                wasabi2 = wasabis[Random.Range(0, wasabis.Length)];
                order = sushiName1 + "�ʹ� " + sushiCount1 + "�� " + "�ͻ�� " + wasabi1 +
                            sushiName2 + "�ʹ� " + sushiCount2 + "�� " + "�ͻ��" + wasabi2 + message;
                AddOrder(sushiName1, wasabi1, sushiCount1);
                AddOrder(sushiName2, wasabi2, sushiCount2);
                orderTxt.text = order;
                break;
            case 2:
                //�ʹ� �� ����.
                sushiName1 = sushis[Random.Range(0, sushis.Length)];
                sushiName2 = sushis[Random.Range(0, sushis.Length)];
                sushiName3 = sushis[Random.Range(0, sushis.Length)];
                sushiCount1 = Random.Range(1, 3);
                sushiCount2 = Random.Range(1, 3);
                sushiCount3 = Random.Range(1, 3);
                wasabi1 = wasabis[Random.Range(0, wasabis.Length)];
                wasabi2 = wasabis[Random.Range(0, wasabis.Length)];
                wasabi3 = wasabis[Random.Range(0, wasabis.Length)];
                order = sushiName1 + "�ʹ� " + sushiCount1 + "�� " + "�ͻ�� " + wasabi1 +
                            sushiName2 + "�ʹ� " + sushiCount2 + "�� " + "�ͻ�� " + wasabi2 +
                            sushiName3 + "�ʹ� " + sushiCount3 + "�� " + "�ͻ�� " + wasabi3 + message;
                AddOrder(sushiName1, wasabi1, sushiCount1);
                AddOrder(sushiName2, wasabi2, sushiCount2);
                AddOrder(sushiName3, wasabi3, sushiCount3);
                orderTxt.text = order;
                break;
        }

        /*order = sushis[randomSushi] + "�ʹ� " +
                    count + "�� " +
                    "�ͻ�� " + wasabis[randomWasabi] + message;*/  //�ֹ�

        //�ؽ�Ʈ�� �ֹ����� �ֱ�.
        //orderTxt.text = order; 

        //�ֹ� ����Ʈ�� �߰�.
        //AddOrder(sushis[randomSushi], wasabis[randomWasabi], count);
    }

    private void Update()
    {
        if (isTimer)
        {
            currTime -= Time.deltaTime;  //�ð��� �پ��
            currTimePercent = currTime / maxTime;  //�����ð� ����
            timer.fillAmount = currTimePercent;  //Ÿ�̸Ӵ� �����ð� ������ �°� �پ��
            if (currTime <= 0)
            {
                print("�� ���� ����.");
                orderTxt.text = "�� �Ǹ���.";
                Destroy(gameObject, 0.5f);  //�մ� ����
                cookManager.Create();  //�մ� ����.
                orders.Clear();  //�ֹ� ����Ʈ Ŭ����.
                isTimer = false;  //Ÿ�̸� ��Ȱ��ȭ ���·� �Ǵ�
                isOrdered = false;  //�ֹ��� �ȹ޾���.
            }
        }
    }

    public void ShowTimer()  //�մ� Ÿ�̸� Ȱ��ȭ
    {
        isTimer = true;  //Ÿ�̸� Ȱ��ȭ ���·� �Ǵ�.
        isOrdered = true;  //�ֹ��� �޾���.
        dish.sushiCounts.Clear();  //�ʹ� ��ųʸ� Ŭ����.
        dish.ClearSushi();  //���� �� �ʹ� ����.
        timerObj.SetActive(true);  //Ÿ�̸� Ȱ��ȭ.
        yesBtn.SetActive(false);  //��ư ��Ȱ��ȭ.
        noBtn.SetActive(false);  //��ư ��Ȱ��ȭ.
    }

    public void NoBtn() //���� ��ư.
    {
        orderTxt.text = "�� �Ǹ���.";
        Destroy(gameObject, 0.5f);  //�մ� ����
        cookManager.Create();  //�մԻ���
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
            Dictionary<string, int> sushiCounts = dish.sushiCounts;  //���� ��ųʸ�
            foreach (Order order in orders)
            {
                string sushiKey = order.sushiName + "_" + order.wasabi;  //�ֹ� �ʹ� + �ͻ�� Ű��.

                if (sushiCounts.ContainsKey(sushiKey))  //�ֹ��� �ʹ� Ű�� ��ġ�ϴ°� ���� ��ųʸ��� �ִ°�?
                {
                    int sushiCount = sushiCounts[sushiKey];  //�ִٸ� �� �ʹ� Ű�� ������ �ֹ� ������ ��.
                    if (sushiCount == order.count)  //�ֹ������� �ʹ� ������ ��ġ�Ѵٸ�
                    {
                        Debug.Log($"�ֹ��� �ʹ� ���� ��ġ - ����: {order.sushiName}, �ͻ��: {order.wasabi}, ����: {order.count}");
                        orderTxt.text = "�� �����Կ�!";
                        Destroy(gameObject, 0.5f);  //�մ� ����
                        orders.Clear();  //�ֹ� ����Ʈ Ŭ����.
                        dish.sushiCounts.Clear();  //�ʹ� ��ųʸ� Ŭ����.
                        dish.ClearSushi();  //���� �� �ʹ� ����.
                        cookManager.Create();  //�մ� ����.
                    }
                    else
                    {
                        Debug.Log($"�ֹ��� �ʹ� ���� ����ġ - ����: {order.sushiName}, �ͻ��: {order.wasabi}, ����: {order.count}");
                        orderTxt.text = "���� ������";
                        Destroy(gameObject, 0.5f);  //�մ� ����
                        orders.Clear();  //�ֹ� ����Ʈ Ŭ����.
                        dish.sushiCounts.Clear();  //�ʹ� ��ųʸ� Ŭ����.
                        dish.ClearSushi();  //���� �� �ʹ� ����.
                        cookManager.Create();  //�ʹ� ����.
                    }
                }
                else
                {
                    Debug.Log($"�ֹ��� �ʹ� ���� ����ġ - ����: {order.sushiName}, �ͻ��: {order.wasabi}, ����: {order.count}");
                    orderTxt.text = "���� ������";
                    Destroy(gameObject, 0.5f);  //�մ� ����
                    orders.Clear();  //�ֹ� ����Ʈ Ŭ����.
                    dish.sushiCounts.Clear();  //�ʹ� ��ųʸ� Ŭ����.
                    dish.ClearSushi();  //���� �� �ʹ� ����.
                    cookManager.Create();  //�մ� ����.
                }
            }
        }
    }
}

public class Order  //�ֹ� Ŭ����
{
    public string sushiName;  //�ʹ� ����
    public string wasabi;  //�ͻ�� ����
    public int count;  //����

    public Order(string sushiName, string wasabi, int count)  //������
    {
        this.sushiName = sushiName;
        this.wasabi = wasabi;
        this.count = count;
    }
}
