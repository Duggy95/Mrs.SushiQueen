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

    CookManager cookManager;
    string message = "�ּ���.";
    string order;  //�ֹ�
    float maxTime; //�ִ�ð�
    float currTime;  //����ð�
    //float currTimePercent;  //����ð� ����
    bool isTimer;

    private void Awake()
    {
        cookManager = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<CookManager>();
    }

    private void Start()
    {
        maxTime = 30;
        currTime = maxTime;  //Ÿ�̸� �ʱⰪ ����
    }

    void OnEnable()
    {
        int randomSprite = Random.Range(0, sprites.Length);
        GetComponent<Image>().sprite = sprites[randomSprite];

        int randomSushi = Random.Range(0, sushis.Length);
        int randomWasabi = Random.Range(0, wasabis.Length);
        order = sushis[randomSushi] + "�ʹ� " +
                    Random.Range(1, 4) + "�� " +
                    "�ͻ�� " + wasabis[randomWasabi] + message;

        orderTxt.text = order;
    }

    private void Update()
    {
        if(isTimer)
        {
            currTime -= Time.deltaTime * 5;  //�ð��� �پ��
            float currTimePercent = currTime / maxTime;  //�����ð� ����
            timer.fillAmount = currTimePercent;  //Ÿ�̸Ӵ� �����ð� ������ �°� �پ��
            if (currTime <= 0)
            {
                print("�� ���� ����.");
                orderTxt.text = "�� �Ǹ���.";
                Destroy(gameObject, 0.5f);
                cookManager.Create();
                isTimer = false;
            }
        }
    }

    public void ShowTimer()  //�մ� Ÿ�̸� Ȱ��ȭ
    {
        isTimer = true;
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
}
