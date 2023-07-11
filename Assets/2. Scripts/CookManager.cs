using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class CookManager : MonoBehaviour
{
    public GameObject orderView;
    public GameObject cookView;
    public Text dateTxt;
    public Text goldTxt;
    public GameObject InventoryImg;
    public RawImage[] fishImg;

    int count = 0;

    void Start()
    {
        //���� ���� = �ֹ�ȭ�� ���̰�
        orderView.SetActive(true);
        cookView.SetActive(false);
        UIUpdate();
    }

    void Update()
    {
    }

    public void GoEndScene()  //�������
    {
        SceneManager.LoadScene(3);
    }

    public void UIUpdate()  //�ؽ�Ʈ �ֽ�ȭ
    {
        dateTxt.text = GameManager.instance.dateCount + "���� / ���� : " + GameManager.instance.score;
        goldTxt.text = "gold : " + GameManager.instance.gold;
    }

    public void ViewInventory()
    {
        // �κ��丮 Ȱ��ȭ
        InventoryImg.gameObject.SetActive(true);
    }

    public void EscInventory()
    {
        //�κ��丮 ������
        InventoryImg.gameObject.SetActive(false);
    }

    public void GoHome() //Ȩ����
    {
        SceneManager.LoadScene(0);
    }

    public void ViewOrder()  //�ֹ�ȭ�� �丮ȭ�� ��ȯ �޼���
    {
        count++;
        if (count % 2 == 0)
        {
            orderView.SetActive(true);
            cookView.SetActive(false);
            count = 0;

            for (int i = 0; i < fishImg.Length; i++)
            {
                Destroy(fishImg[i].GetComponent<DragItem>());
            }
        }
        else
        {
            cookView.SetActive(true);
            orderView.SetActive(false);

            for (int i = 0; i < fishImg.Length; i++)
            {
                fishImg[i].AddComponent<DragItem>();
            }
        }
    }
}
