using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCook : TutorialBase
{
    public GameObject customerPrefab;
    public GameObject orderView;
    public GameObject customer;
    public GameObject cookView;
    public GameObject readyBtn;
    public GameObject InventoryImg;
    public Text orderTxt;
    //public CanvasGroup inventoryCanvas;
    public List<string> fishList = new List<string>();
    WaitForSeconds ws;

    public bool isCustomer;
    public bool canMake;
    public bool onReady;
    public bool isReady;
    int count = 0;
    public int sucessCount = 0;
    public Vector2 customerStartPos;
    Vector2 customerTr = Vector2.zero;

    public override void Enter()
    {
        ws = new WaitForSeconds(2f);
        customerStartPos = new Vector2(-450, -100);  //손님 위치
        //시작 세팅 = 주문화면 보이게
        orderView.SetActive(true);
        cookView.SetActive(true);
        //StartCoroutine(Create(0));
    }

    public override void Execute(TutorialManager tutorialManager)
    {
        if(isCustomer)
        {
            tutorialManager.SetNextTutorial();
        }
    }

    public override void Exit()
    {

    }

    public void ViewOrder()  //주문화면 요리화면 전환 메서드
    {
        if (canMake)  //만들 수 있을 때
        {
            count++;
            if (count % 2 == 0)
            {
                cookView.SetActive(false);
                count = 0;
            }
            else
            {
                cookView.SetActive(true);
            }
        }
    }

    public IEnumerator Create()
    {
        if(sucessCount <3)
        {
            //2초 후 손님 생성.
            yield return ws;
            GameObject customer = Instantiate(customerPrefab, customerTr,
                                                                    Quaternion.identity, orderView.transform);
            customer.transform.SetSiblingIndex(1);  //2번째 자식.
        }
    }

    public void ReadyBtn()
    {
        if (onReady)
        {
            isReady = true;
            //StartCoroutine(Create());
            StartCoroutine(ShowCustomer());
            readyBtn.SetActive(false);
            orderView.SetActive(true);
            cookView.SetActive(false);
            InventoryImg.gameObject.SetActive(false);
            //inventoryCanvas.interactable = false;
        }
        else
            return;
    }

    public void GoOrder()
    {
        cookView.SetActive(false);
    }

    public void Order(string txt)
    {
        orderTxt.text = txt;
    }

    IEnumerator ShowCustomer()
    {
        yield return ws;
        customer.gameObject.SetActive(true);
    }
}
