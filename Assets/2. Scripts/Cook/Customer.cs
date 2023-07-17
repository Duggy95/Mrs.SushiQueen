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

    Dish dish;  //���� ������Ʈ
    CookManager cookManager;
    string message = "�ּ���.";
    string order;  //�ֹ�
    float maxTime; //�ִ�ð�
    float currTime;  //����ð�
    float currTimePercent;  //����ð� ����
    bool isTimer;  //Ÿ�̸� ����
    bool isOrdered;  //�ֹ�����Ȯ��.

    private void Awake()
    {
        cookManager = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<CookManager>();
    }

    private void Start()
    {
        dish = GameObject.FindGameObjectWithTag("DISH").GetComponent<Dish>();

        maxTime = 10;  //�ִ�ð�.
        currTime = maxTime;  //Ÿ�̸� �ʱⰪ ����
    }

    void OnEnable()
    {
        int randomSprite = Random.Range(0, sprites.Length);  //�����ϰ� �׸� ����.
        GetComponent<Image>().sprite = sprites[randomSprite];  //������ �׸� ����.

        int randomSushi = Random.Range(0, sushis.Length);  //�����ϰ� �ʹ����� ����.
        //int randomWasabi = Random.Range(0, wasabis.Length);
        int randomWasabi = 1;
        int count = Random.Range(1, 4);

        order = sushis[randomSushi] + "�ʹ� " +
                    count + "�� " +
                    "�ͻ�� " + wasabis[randomWasabi] + message;  //�ֹ�

        orderTxt.text = order;  //�ؽ�Ʈ�� �ֹ����� �ֱ�.
        AddOrder(sushis[randomSushi], wasabis[randomWasabi], count);  //�ֹ� ����Ʈ�� �߰�.
    }

    private void Update()
    {
        if(isTimer)
        {
            currTime -= Time.deltaTime;  //�ð��� �پ��
            currTimePercent = currTime / maxTime;  //�����ð� ����
            timer.fillAmount = currTimePercent;  //Ÿ�̸Ӵ� �����ð� ������ �°� �پ��
            if (currTime <= 0)
            {
                print("�� ���� ����.");
                orderTxt.text = "�� �Ǹ���.";
                Destroy(gameObject, 0.5f);
                cookManager.Create();
                orders.Clear();
                isTimer = false;
                isOrdered = false;
            }
        }
    }

    public void ShowTimer()  //�մ� Ÿ�̸� Ȱ��ȭ
    {
        isTimer = true;
        isOrdered = true;
        dish.sushiCounts.Clear();
        dish.ClearSushi();
        timerObj.SetActive(true);
        yesBtn.SetActive(false);
        noBtn.SetActive(false);
    }

    public void NoBtn() //���� ��ư.
    {
        orderTxt.text = "�� �Ǹ���.";
        Destroy(gameObject, 0.5f);
        cookManager.Create();
        yesBtn.SetActive(false);
        noBtn.SetActive(false);
        print("�� ���� ����");
    }

    void AddOrder(string sushiName, string wasabi, int count)  //�ֹ� �߰� �޼���
    {
        Order order = new Order(sushiName, wasabi, count);  //�ʹ�����, �ͻ��, ���� ��������.
        orders.Add(order);
    }

    public void CompareOrders()  //�ֹ��� �޼���.
    {
        if(isOrdered)
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
                        Destroy(gameObject, 0.5f);
                        orders.Clear();
                        dish.sushiCounts.Clear();
                        dish.ClearSushi();
                        cookManager.Create();
                    }
                    else
                    {
                        Debug.Log($"�ֹ��� �ʹ� ���� ����ġ - ����: {order.sushiName}, �ͻ��: {order.wasabi}, ����: {order.count}");
                        orderTxt.text = "���� ������";
                        Destroy(gameObject, 0.5f);
                        orders.Clear();
                        dish.sushiCounts.Clear();
                        dish.ClearSushi();
                        cookManager.Create();
                    }
                }
                else
                {
                    Debug.Log($"�ֹ��� �ʹ� ���� ����ġ - ����: {order.sushiName}, �ͻ��: {order.wasabi}, ����: {order.count}");
                    orderTxt.text = "���� ������";
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
