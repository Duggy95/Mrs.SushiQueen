using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.FullSerializer;

public class FishingManager : MonoBehaviour
{
    public Canvas canvas;
    public Text dateTxt;
    public Text goldTxt;
    public GameObject configPanel;
    public GameObject InventoryImg;  // �κ��丮 �̹���
    public GameObject FishContent; // ������
    public GameObject fishInfo; // ���� ���� �ǳ�
    public GameObject FishingRod; // ���˴� �̹���
    public GameObject lineStartPos;
    public Image fish_Img;
    public Text fishInfo_Txt;
    public Text full_Txt;
    public GameObject fishObj;
    public Text fishRun;

    public bool isFishing = false;
    public bool useItem_white = false;  // �Ͼ� �� ���� Ȯ�� ���� ������ ���
    public bool useItem_red = false;    // ���� �� ���� Ȯ�� ���� ������ ���
    public bool useItem_rare = false;   // ���� ���� Ȯ�� ���� ������ ���
    bool config;

    FishData data;

    void Start()
    {
        InventoryImg.gameObject.SetActive(false);
        UIUpdate();
    }

    public void Fishing()
    {
        if (isFishing == false)
        {
            Debug.Log("���� ����");
            Debug.Log("��ġ" + Input.mousePosition);

            // ����� ��� ������ ����
            isFishing = true;
            // ����� ���� �ؽ�Ʈ ��Ȱ��ȭ
            fishRun.gameObject.SetActive(false);
            LineRenderer fishLine = FishingRod.GetComponent<LineRenderer>();

            Vector3 startPos = lineStartPos.transform.position; // ���� ����
            Vector3 endPos = Input.mousePosition; // �� ����

            // Line Renderer �Ӽ� ����
            fishLine.SetPosition(0, startPos); // ������ ���� ����
            fishLine.SetPosition(1, endPos);
            Instantiate(fishObj, Input.mousePosition, Quaternion.identity);
            //transform.position = Input.mousePosition;
        }
    }


    // ���� ��쿡 ����� ����â ���
    public void Fish(FishData fishData)
    {
        Debug.Log("���� ����");
        Debug.Log(fishData);
        data = fishData;
        fishInfo.gameObject.SetActive(true);
        fishInfo_Txt.text = fishData.info.text;
        fish_Img.sprite = fishData.fishImg;
    }

    // �ȱ� ��ư�� ������ ����â �ݰ� �ٽ� ���� �غ�
    public void Sell()
    {
        Debug.Log("�Ǹ�");

        full_Txt.gameObject.SetActive(false);
        fishInfo.gameObject.SetActive(false);
        //GameManager.instance.gold += data.gold;
        //GameManager.instance.SetGold();
        int _gold = int.Parse(GameManager.instance.data.gold) + data.gold;
        GameManager.instance.data.gold = _gold.ToString();
        GameManager.instance.Save("s");
        Debug.Log("��� " + _gold);
        // ��� ++
        UIUpdate();
        isFishing = false;
    }

    // ���������� ��ư ������ ����â �ݰ� �ٽ� ���� �غ�
    public void Get()
    {
        Debug.Log("����������");

        // �������� �̹��� �߰�
        Image[] _fishs = FishContent.gameObject.GetComponentsInChildren<Image>();
        Debug.Log("������ ĭ �� : " + _fishs.Length);

        bool isFull = false;
        // �������� ������ �˻��Ͽ� ������� �ش� ���� ����
        for (int i = 0; i < _fishs.Length; i++)
        {
            FishSlot _slot = _fishs[i].GetComponent<FishSlot>();
            if (_slot.isEmpty == false)
            {
                _fishs[i].sprite = data.fishImg;
                _slot.fish_ColorNum = data.color;
                _slot.fish_GradeNum = data.grade;
                _slot.fish_Name = data.fishName;

                InventoryFish _inventoryFish = new InventoryFish();
                _inventoryFish.fish_Name = data.fishName;
                GameManager.instance.inventory_Fishs.Add(_inventoryFish);

                //GameManager.instance.fishs = data.fishName;
                GameManager.instance.Save("f");
                _fishs[i].GetComponentInChildren<Text>().text = data.fishName;
                _slot.isEmpty = true;
                isFull = true;
                break;
            }
        }
        // �������� ���� �� ��� �ؽ�Ʈ ���
        if (!isFull)
        {
            full_Txt.gameObject.SetActive(true);
        }
        // �ƴ� ��� ���� �г� ��Ȱ��ȭ
        else
        {
            isFishing = false;
            fishInfo.gameObject.SetActive(false);
        }
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
        yield return new WaitForSeconds(2f);
        Debug.Log("�ٽ�");
        fishRun.gameObject.SetActive(false);
    }

    void UIUpdate()
    {
        dateTxt.text = GameManager.instance.data.dateCount + "���� / ���� : " + GameManager.instance.data.score;
        goldTxt.text = "gold : " + GameManager.instance.data.gold;
    }

    public void ViewInventory()
    {
        // �κ��丮 Ȱ��ȭ
        InventoryImg.gameObject.SetActive(true);
        // �κ��丮 ������ ���� ����������
        InventoryImg.transform.SetAsLastSibling();
    }

    public void EscInventory()
    {
        InventoryImg.gameObject.SetActive(false);
    }

    public void ConfigBtn()
    {
        if (!config)
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

    public void GoCook()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
