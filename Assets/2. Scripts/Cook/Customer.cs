using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public Text orderTxt;  //�ֹ� �ؽ�Ʈ
    //public Sprite[] sprites;  //�մ� ��������Ʈ �迭
    public string[] sushis;  //�ʹ� ���� �迭
    public string[] wasabis;  //�ͻ��
    public string[] compliment;  //�뼺�� ����.
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
    CookManager cookManager;  //�� �Ŵ���.
    Transform tr;  //��ġ
    public string message;
    string order;  //�ֹ�
    float maxTime; //�ִ�ð�
    float currTime;  //����ð�
    float currTimePercent;  //����ð� ����
    bool isTimer;  //Ÿ�̸� ����
    bool isOrdered;  //�ֹ�����Ȯ��.
    int randomOrder;

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

    private void Awake()
    {
        //�� �Ŵ��� ã��
        cookManager = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<CookManager>();
        images = GetComponentsInChildren<Image>();
        texts = GetComponentsInChildren<Text>();
    }

    private void Start()
    {
        dish = GameObject.FindGameObjectWithTag("DISH").GetComponent<Dish>();  //���� ������Ʈ Find�� ��������
        tr = GetComponent<Transform>();

        maxTime = 20;  //�ִ�ð�.
        currTime = maxTime;  //Ÿ�̸� �ʱⰪ ����
    }

    void OnEnable()
    {

        StartCoroutine(FadeIn());

        //randomOrder = Random.Range(0, 3);  //�ֹ��� ���� �����ϰ� ����.
        if (int.Parse(GameManager.instance.data.score) <= 600)
        {
            //80��, 17��, 3��
            RandomChance(80, 97);
            print(randomOrder);
            print("ù��°");
        }
        else if (int.Parse(GameManager.instance.data.score) <= 900)
        {
            //60��, 30��, 10��
            RandomChance(60, 90);
            print("�ι�°");
        }
        else
        {
            //40��, 40��, 20��
            RandomChance(40, 80);
            print("����°");
        }

        //int randomSprite = Random.Range(0, sprites.Length);  //�����ϰ� �׸� ����.
        //GetComponent<Image>().sprite = sprites[randomSprite];  //������ �׸� ����.

        switch (randomOrder)  //������ �ֹ�.
        {
            case 0:
                //�ʹ� �� ����. Random.Range�� �̿��ؼ� �ʹ�����, ����, �ͻ�� ����.
                sushiName1 = sushis[Random.Range(0, sushis.Length)];
                sushiCount1 = Random.Range(1, 4);
                wasabi1 = wasabis[Random.Range(0, wasabis.Length)];
                order = sushiName1 + "�ʹ� " + "�ͻ�� " + wasabi1 + " " + sushiCount1 + "�� " + message;
                AddOrder(sushiName1, wasabi1, sushiCount1);  //�����ϰ� ������ ��ҵ��� �ֹ� ����Ʈ�� �߰�
                orderTxt.text = order;  //�ֹ� �ؽ�Ʈ ���.
                orderRecipe = sushiName1 + ", " + wasabi1 + ", " + sushiCount1;
                cookManager.Order(orderRecipe);
                break;
            case 1:
                //�ʹ� �� ����.
                //The Fisher-Yates Shuffle �˰����� �̿��Ͽ� �迭�� �������� ����
                //���� ���¿��� �տ������� ������� ��Ҹ� ����.
                for (int i = sushis.Length - 1; i > 0; i--)
                {
                    int randomIndex = Random.Range(0, i + 1);
                    // ��Ҹ� ���� ��ȯ
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
                order = sushiName1 + "�ʹ� " + "�ͻ�� " + wasabi1 + " " + sushiCount1 + "�� " + "\n" +
                            sushiName2 + "�ʹ� " + "�ͻ�� " + wasabi2 + " " + sushiCount2 + "�� " + message;
                //�����ϰ� ������ ��ҵ� �ֹ� ����Ʈ�� �߰�.
                AddOrder(sushiName1, wasabi1, sushiCount1);
                AddOrder(sushiName2, wasabi2, sushiCount2);
                orderTxt.text = order;  //�ֹ� �ؽ�Ʈ ���
                orderRecipe = sushiName1 + ", " + wasabi1 + ", " + sushiCount1 + "\n" +
                                       sushiName2 + ", " + wasabi2 + ", " + sushiCount2;
                cookManager.Order(orderRecipe);
                break;
            case 2:
                //�ʹ� �� ����.
                //The Fisher-Yates Shuffle �˰����� �̿��Ͽ� �迭�� �������� ����
                //���� ���¿��� �տ������� ������� ��Ҹ� ����.
                for (int i = sushis.Length - 1; i > 0; i--)
                {
                    int randomIndex = Random.Range(0, i + 1);
                    // ��Ҹ� ���� ��ȯ
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
                order = sushiName1 + "�ʹ� " + "�ͻ�� " + wasabi1 + " " + sushiCount1 + "�� " + "\n" +
                            sushiName2 + "�ʹ� " + "�ͻ�� " + wasabi2 + " " + sushiCount2 + "�� " + "\n" +
                            sushiName3 + "�ʹ� " + "�ͻ�� " + wasabi3 + " " + sushiCount3 + "�� " + message;
                //�����ϰ� ������ ��ҵ� �ֹ� ����Ʈ�� �߰�
                AddOrder(sushiName1, wasabi1, sushiCount1);
                AddOrder(sushiName2, wasabi2, sushiCount2);
                AddOrder(sushiName3, wasabi3, sushiCount3);
                orderTxt.text = order;  //�ֹ� �ؽ�Ʈ ���.
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
            currTime -= Time.deltaTime;  //�ð��� �پ��
            currTimePercent = currTime / maxTime;  //�����ð� ����
            timer.fillAmount = currTimePercent;  //Ÿ�̸Ӵ� �����ð� ������ �°� �پ��

            //�մ� Ÿ�̸Ӱ� �� ������
            if (currTime <= 0)
            {
                print("�� ���� ����.");
                int _score = int.Parse(GameManager.instance.data.score) - 20;  //���� ����
                GameManager.instance.data.score = _score.ToString();

                GameManager.instance.todayData.score -= 20;

                //GameManager.instance.Save("d");   //���� ����
                cookManager.UIUpdate();  //UI �ֽ�ȭ
                cookManager.ViewOrder();  //�ֹ� â���� �Ѿ��.
                cookManager.canMake = false;
                orderTxt.text = fail[0];  //���� �ؽ�Ʈ ���.
                //Destroy(gameObject, 3f);  //�մ� ����
                StartCoroutine(FadeOut());
                //StartCoroutine(Move());  //�մ� �� ���̴� ������ �ű��.
                //StartCoroutine(cookManager.Create());  //�մԻ���
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
        cookManager.canMake = true;  //����� ����.
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
        int _score = int.Parse(GameManager.instance.data.score) - 2;  //���� ����
        GameManager.instance.data.score = _score.ToString();

        GameManager.instance.todayData.score -= 2;

        //GameManager.instance.Save("d");   //���� ����
        cookManager.UIUpdate();  //UI �ֽ�ȭ
        orderTxt.text = fail[0];  //���� �ؽ�Ʈ ���.
        isOrdered = false;
        //Destroy(gameObject, 3f);  //�մ� ����
        StartCoroutine(FadeOut());
        //StartCoroutine(Move());  //�Ⱥ��̴� ������ �ű��
        //StartCoroutine(cookManager.Create());  //�մԻ���
        yesBtn.SetActive(false);  //��ư ��Ȱ��ȭ
        noBtn.SetActive(false);  //��ư ��Ȱ��ȭ
        isOrdered = false;
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
            List<Sushi> sushis = dish.sushiList;  //���� ����Ʈ (�ʹ��� ���� �ҷ��÷���)
            int totalPrice = 0;
            bool orderMatch = true;  //�ֹ� ��ġ
            cookManager.canMake = false;
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
                        if(currTimePercent >= 0.5)
                        {
                            totalPrice += sushi.gold * order.count * 3;  //�������� * �ֹ����� * 4
                        }
                        else
                        {
                            totalPrice += sushi.gold * order.count * 2;  //�������� * �ֹ����� * 2
                        }

                        int _gold = int.Parse(GameManager.instance.data.gold) + totalPrice;
                        GameManager.instance.data.gold = _gold.ToString();

                        GameManager.instance.todayData.gold += totalPrice;

                        //GameManager.instance.Save("d"); 
                        cookManager.UIUpdate();
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
                int _score = int.Parse(GameManager.instance.data.score) + 20;  //���� ����
                GameManager.instance.data.score = _score.ToString();

                GameManager.instance.todayData.score += 20;

                //GameManager.instance.Save("d");  //���� ����
                cookManager.UIUpdate();
                orderTxt.text = success[Random.Range(0, success.Length)];
                Debug.Log("�� ����: " + totalPrice);
                orders.Clear();  //�ֹ� ����Ʈ Ŭ����.
                dish.sushiCounts.Clear();  //�ʹ� ��ųʸ� Ŭ����.
                dish.sushiList.Clear();  //�ʹ� ����Ʈ Ŭ����.
                dish.ClearSushi();  //���� �� �ʹ� ����.
                isOrdered = false;
                //Destroy(gameObject, 4f);  //�մ� ����
                StartCoroutine(FadeOut());
                //StartCoroutine(Move());
                //StartCoroutine(cookManager.Create());  //�մԻ���
                isOrdered = false;
            }
            else  //����ġ ��
            {
                int _score = int.Parse(GameManager.instance.data.score) - 20;  //���� ����
                GameManager.instance.data.score = _score.ToString();

                GameManager.instance.todayData.score -= 20;


                //GameManager.instance.Save("d");  //���� ����
                cookManager.UIUpdate();
                orderTxt.text = fail[Random.Range(0, fail.Length)];
                orders.Clear();  //�ֹ� ����Ʈ Ŭ����.
                dish.sushiCounts.Clear();  //�ʹ� ��ųʸ� Ŭ����.
                dish.sushiList.Clear();  //�ʹ� ����Ʈ Ŭ����.
                dish.ClearSushi();  //���� �� �ʹ� ����.
                isOrdered = false;
                //Destroy(gameObject, 4f);  //�մ� ����
                StartCoroutine(FadeOut());
                //StartCoroutine(Move());
                //StartCoroutine(cookManager.Create());  //�մԻ���
                isOrdered = false;
            }
        }
    }

    void RandomChance(int num1, int num2)
    {
        int randomNum = Random.Range(1, 101);

        if (randomNum <= num1)
        {
            print(randomNum);
            randomOrder = 0;
        }
        else if (randomNum <= num2)
        {
            print(randomNum);
            randomOrder = 1;
        }
        else
        {
            print(randomNum);
            randomOrder = 2;
        }
    }

    IEnumerator FadeIn()
    {
        Vector2 initPos = cookManager.customerStartPos;
        print(initPos);
        Vector2 targetPos = initPos + new Vector2(30, 0);
        for(int i=0; i < images.Length; i++)
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
            for(int i = 0; i < images.Length; i++)
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

    IEnumerator FadeOut()
    {
        timer.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        Vector2 initPos = this.transform.localPosition;
        Vector2 targetPos = new Vector2(this.transform.localPosition.x - 30, this.transform.localPosition.y);
        //image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // �ð� ���� ���
            float alpha = 1 - t;

            for(int i =0; i< images.Length; i++)
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

        StartCoroutine(cookManager.Create());
        Destroy(this.gameObject, 4);
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
