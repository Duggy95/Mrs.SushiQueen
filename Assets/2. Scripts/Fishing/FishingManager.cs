using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class FishingManager : MonoBehaviour
{
    public Canvas canvas;
    public Text dateTxt;
    public Text goldTxt;
    public GameObject InventoryImg;  // �κ��丮 �̹���
    public GameObject FishContent; // ������
    public GameObject fishInfo; // ���� ���� �ǳ�
    public Image fish_Img;
    public Text fishInfo_Txt;
    public GameObject fishObj;
    public Text fishRun;
    public bool isFishing = false;

    int maxCount = 0; // �������� �ִ�� ���� �� �ִ� ����� ��

    void Start()
    {
        UIUpdate();
    }

    void Update()
    {
        // ��ġ�� �ϰ� ����� ��� ���� �ƴ϶��
        if (Input.GetMouseButtonDown(0) && isFishing == false)
        {
            Debug.Log("���� ����");
            Debug.Log("��ġ" + Input.mousePosition);

            // ����� ��� ������ ����
            isFishing = true;
            // ����� ���� �ؽ�Ʈ ��Ȱ��ȭ
            fishRun.gameObject.SetActive(false);

            Instantiate(fishObj, Input.mousePosition, Quaternion.identity);
        }
    }


    // ���� ��쿡 ����� ����â ���
    public void Fish(FishData fishData)
    {
        Debug.Log("���� ����");
        Debug.Log(fishData);

        fishInfo.gameObject.SetActive(true);
        fishInfo_Txt.text = fishData.info.text;
        fish_Img.sprite = fishData.fishImg;
    }

    // �ȱ� ��ư�� ������ ����â �ݰ� �ٽ� ���� �غ�
    public void Sell(FishData fishData)
    {
        Debug.Log("�Ǹ�");

        fishInfo.gameObject.SetActive(false);
        //GameManager.instance.gold += fishData.gold;
        // ��� ++
        //UIUpdate();
        isFishing = false;
    }

    // ���������� ��ư ������ ����â �ݰ� �ٽ� ���� �غ�
    public void Get(FishData fishData)
    {
        Debug.Log("����������");

        fishInfo.gameObject.SetActive(false);
        // �������� �̹��� �߰�
        Image[] fishImgs = FishContent.GetComponents<Image>();
        for (int i = 0; i < fishImgs.Length; i++)
        {
            Slot _slot = fishImgs[i].GetComponent<Slot>();
            if (_slot.isEmpty == false)
            {
                fishImgs[i].sprite = fishData.fishImg;
                _slot.isEmpty = true;
            }
        }
        isFishing = false;
    }

    public void Run()
    {
        StartCoroutine(FishRun());
    }

    // ����Ⱑ ������ ��� ���� �ؽ�Ʈ ���� �ٽ� ���� �غ�
    IEnumerator FishRun()
    {
        Debug.Log("���� ����");

        fishRun.gameObject.SetActive(true);
        isFishing = false;
        // ȭ�� ������ �ؽ�Ʈ ��Ȱ��ȭ
        yield return Input.GetMouseButtonDown(0);
        fishRun.gameObject.SetActive(false);
    }

    void UIUpdate()
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
        InventoryImg.gameObject.SetActive(false);
    }

    public void GoHome()
    {
        SceneManager.LoadScene(0);
    }

    public void GoCook()
    {
        SceneManager.LoadScene(2);
    }
}
