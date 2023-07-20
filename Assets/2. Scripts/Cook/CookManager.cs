using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class CookManager : MonoBehaviour
{
    public Canvas canvas;  //Order Canvas �ֹ� ĵ����
    public GameObject customerPrefab;  //�մ� ������
    public GameObject orderView;  //�ֹ�ȭ��
    public GameObject cookView;  //�丮ȭ��
    public Text dateTxt;  //��¥ + ����
    public Text goldTxt;  //���
    public GameObject InventoryImg;  //�κ��丮
    public Image[] fishImg;  //�����̹���
    public bool canMake = false;

    WaitForSeconds ws;

    Vector2 customerTr = Vector2.zero;
    int count = 0;

    void Start()
    {
        //���� ���� = �ֹ�ȭ�� ���̰�
        orderView.SetActive(true);
        cookView.SetActive(false);
        ws = new WaitForSeconds(2f);
        UIUpdate();

        StartCoroutine(Create());
    }

    void Update()
    {
        UIUpdate();
    }

    public void GoEndScene()  //�������
    {
        SceneManager.LoadScene(3);
    }

    void UIUpdate()
    {
        dateTxt.text = GameManager.instance.data.dateCount + "���� / ���� : " + GameManager.instance.data.score;
        goldTxt.text = "gold : " + GameManager.instance.data.gold;

        /*dateTxt.text = GameManager.instance.save[2].dateCount + "���� / ���� : " + GameManager.instance.save[3].score;
        goldTxt.text = "gold : " + GameManager.instance.save[4].gold;*/
    }

    public void ViewInventory() //�κ��丮 Ȱ��ȭ
    {
        InventoryImg.gameObject.SetActive(true);
    }

    public void EscInventory() //�κ��丮 ������
    {
        InventoryImg.gameObject.SetActive(false);
    }

    public void GoHome() //Ȩ����
    {
        SceneManager.LoadScene(0);
    }

    public void ViewOrder()  //�ֹ�ȭ�� �丮ȭ�� ��ȯ �޼���
    {
        if(canMake)  //���� �� ���� ��
        {
            count++;
            if (count % 2 == 0)
            {
                cookView.SetActive(false);
                count = 0;

                /*for (int i = 0; i < fishImg.Length; i++)
                {
                    Destroy(fishImg[i].GetComponent<DragItem>());
                }*/
            }
            else
            {
                cookView.SetActive(true);
                /*for (int i = 0; i < fishImg.Length; i++)
                {
                    fishImg[i].AddComponent<DragItem>();
                }*/
            }
        }
    }

    public IEnumerator Create()
    {
        //2�� �� �մ� ����.
        yield return ws;
        GameObject customer = Instantiate(customerPrefab, customerTr,
                                                                Quaternion.identity, canvas.transform);
        customer.transform.localPosition = new Vector2(-400, -100);  //�մ� ��ġ
        customer.transform.SetSiblingIndex(1);  //2��° �ڽ�.
    }
}
