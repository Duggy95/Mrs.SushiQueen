using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public Text orderTxt;  //�ֹ� �ؽ�Ʈ
    public Sprite[] sprites;
    public string[] sushis;
    public string[] wasabis;

    CookManager cookManager;
    string message = "�ּ���.";
    string order;
    Image timer; //Ÿ�̸� �̹���
    float maxTime; //�ִ�ð�
    float currTime;  //����ð�
    float currTimePercent;

    void OnEnable()
    {
        int randomSprite = Random.Range(0, sprites.Length);
        GetComponent<Image>().sprite = sprites[randomSprite];

        int randomSushi = Random.Range(0, sushis.Length);
        int randomWasabi = Random.Range(0, wasabis.Length);
        order = sushis[randomSushi] + "�ʹ� " +
                    Random.Range(1, 4) + "�� " +
                    "�ͻ�� " + wasabis[randomWasabi] + message;
        Text orderTxt = GameObject.Find("Order_Text").GetComponent<Text>();
        orderTxt.text = order;
    }

    private void Awake()
    {
        cookManager = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<CookManager>();
    }

    private void Start()
    {
        timer = GetComponent<Image>();
        maxTime = 30;
        currTime = maxTime;  // �ʱⰪ

    }

    private void Update()
    {
        currTime -= Time.deltaTime * 5;  //�ð��� �پ��
        currTimePercent = currTime / maxTime;  //�����ð� ����
        timer.fillAmount = currTimePercent;  //Ÿ�̸Ӵ� �����ð� ������ �°� �پ��
        if (currTime <= 0)
        {
            //���� ����.
            print("�� ���� ����.");
            orderTxt.text = "�� �Ǹ���.";
            cookManager.Create();
            Destroy(gameObject);
        }
    }
}
