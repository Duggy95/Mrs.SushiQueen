using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CookManager : MonoBehaviour
{
    public CanvasGroup inventoryCanvas;
    public GameObject customerPrefab;  //�մ� ������
    public GameObject orderView;  //�ֹ�ȭ��
    public GameObject cookView;  //�丮ȭ��
    public GameObject configPanel;
    public GameObject readyBtn;
    public GameObject InventoryImg;  //�κ��丮
    public GameObject[] customers;
    public Text dateTxt;  //��¥ + ����
    public Text goldTxt;  //���
    public Text atkTxt;  //���
    public Text orderTxt;
    //public Image[] fishImg;  //�����̹���
    public bool canMake = false;
    public bool isReady = false;
    bool config = false;
    public Vector2 customerStartPos;

    //Transform fishContent;

    WaitForSeconds ws;
    Vector2 customerTr = Vector2.zero;
    int count = 0;

    void Start()
    {
        customerStartPos = new Vector2(-450, -100);  //�մ� ��ġ

        //���� ���� ȭ�� ����
        orderView.SetActive(true);
        cookView.SetActive(true);

        /*if(int.Parse(GameManager.instance.data.score) <= 600)
        {
            ws = new WaitForSeconds(3f);
        }
        else if(int.Parse(GameManager.instance.data.score) <= 900)
        {
            ws = new WaitForSeconds(2.5f);
        }
        else
        {
            ws = new WaitForSeconds(2f);
        }*/
        ws = new WaitForSeconds(2);

        UIUpdate();
    }

    void Update()
    {
        UIUpdate();
    }

    public void GoEndScene()  //�������
    {
        SceneManager.LoadScene(3);
    }

    public void Delete()
    {
        GameManager.instance.DeleteData();
        UIUpdate();
    }


    public void UIUpdate()
    {
        dateTxt.text = GameManager.instance.data.dateCount + "���� / ���� : " + GameManager.instance.data.score;
        goldTxt.text = "gold : " + GameManager.instance.data.gold;
        atkTxt.text = "���ݷ� : " + GameManager.instance.data.atk;
    }

    public void ViewInventory() //�κ��丮 Ȱ��ȭ
    {
        InventoryImg.gameObject.SetActive(true);
    }

    public void EscInventory() //�κ��丮 ������
    {
        InventoryImg.gameObject.SetActive(false);
    }

    public void ConfigBtn() //���������ֱ�
    {
        if(!config)
        {
            configPanel.SetActive(true);
            config = true;
        }
        else
        {
            configPanel.SetActive(false);
            config = false;
        }
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
            }
            else
            {
                cookView.SetActive(true);
            }
        }
    }

    public IEnumerator Create()
    {
        int random = Random.Range(0, customers.Length);
        //2�� �� �մ� ����.
        yield return ws;
        GameObject customer = Instantiate(customers[random], customerTr,
                                                                Quaternion.identity, orderView.transform);
        customer.transform.SetSiblingIndex(1);  //2��° �ڽ�.
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReadyBtn()
    {
        isReady = true;
        StartCoroutine(Create());
        readyBtn.SetActive(false);
        orderView.SetActive(true);
        cookView.SetActive(false);
        InventoryImg.gameObject.SetActive(false);
        inventoryCanvas.interactable = false;
    }

    public void Order(string txt)
    {
        orderTxt.text = txt;
    }

    /*public void LogOut()
    {
        GPGSBinder.Inst.Logout();
    }*/
}
