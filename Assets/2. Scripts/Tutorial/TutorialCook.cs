using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCook : TutorialBase
{
    public GameObject[] customer;
    public GameObject orderView;
    public GameObject cookView;

    WaitForSeconds ws;

    bool isCustomer;
    public bool canMake;
    public int count = 0;
    Vector2 customerTr = Vector2.zero;

    public override void Enter()
    {
        ws = new WaitForSeconds(2f);
        //���� ���� = �ֹ�ȭ�� ���̰�
        orderView.SetActive(true);
        cookView.SetActive(false);
        StartCoroutine(Create(0));
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

    public void ViewOrder()  //�ֹ�ȭ�� �丮ȭ�� ��ȯ �޼���
    {
        if (canMake)  //���� �� ���� ��
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

    public IEnumerator Create(int num)
    {
        //2�� �� �մ� ����.
        yield return ws;
        /*GameObject customer = Instantiate(customerPrefab, customerTr,
                                                                Quaternion.identity, orderView.transform);*/
        customer[num].transform.localPosition = new Vector2(-400, -100);  //�մ� ��ġ
        //customer.transform.SetSiblingIndex(1);  //2��° �ڽ�.
        isCustomer = true;
    }
}
