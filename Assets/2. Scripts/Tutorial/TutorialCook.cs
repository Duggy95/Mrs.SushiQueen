using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCook : TutorialBase
{
    public GameObject customerPrefab;
    public GameObject orderView;
    public GameObject cookView;

    WaitForSeconds ws;

    bool isCustomer;
    bool canMake;
    int count;
    Vector2 customerTr = Vector2.zero;
    public override void Enter()
    {
        ws = new WaitForSeconds(2f);

        //시작 세팅 = 주문화면 보이게
        orderView.SetActive(true);
        cookView.SetActive(false);
        Create();
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
        //2초 후 손님 생성.
        yield return ws;
        GameObject customer = Instantiate(customerPrefab, customerTr,
                                                                Quaternion.identity, orderView.transform);
        customer.transform.localPosition = new Vector2(-400, -100);  //손님 위치
        customer.transform.SetSiblingIndex(1);  //2번째 자식.
        isCustomer = true;
    }
}
