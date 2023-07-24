using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutorialFishing : TutorialBase
{
    public GameObject fishObj;
    public GameObject fishCanvas;
    public GameObject fishContent;
    public Text full_Txt;
    public Text fishRun;
    public Text touchTxt;
    public GameObject fishInfo;
    public Text fishInfo_Txt;
    public Image fish_Img;
    FishData data;

    bool isFishing;
    public bool fishCome;

    public override void Enter()
    {
        print("fishing tutorial");
    }

    public override void Execute(TutorialManager tutorialManager)
    {
        if(fishCome)
        {
            tutorialManager.SetNextTutorial();
        }
    }

    public override void Exit()
    {

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
            //LineRenderer fishLine = FishingRod.GetComponent<LineRenderer>();

            //Vector3 startPos = lineStartPos.transform.position; // ���� ����
            //Vector3 endPos = Input.mousePosition; // �� ����

            // Line Renderer �Ӽ� ����
            //fishLine.SetPosition(0, startPos); // ������ ���� ����
            //fishLine.SetPosition(1, endPos);
            Instantiate(fishObj, Input.mousePosition, Quaternion.identity);
            //transform.position = Input.mousePosition;
        }
    }

    public void Fish(FishData fishData)
    {
        Debug.Log("���� ����");
        Debug.Log(fishData);
        data = fishData;
        fishInfo.gameObject.SetActive(true);
        fishInfo_Txt.text = fishData.info.text;
        fish_Img.sprite = fishData.fishImg;
    }

    /*public void Run()
    {
        StartCoroutine(FishRun());
    }*/

    /*IEnumerator FishRun()
    {
        Debug.Log("���� ����");

        fishRun.gameObject.SetActive(true);
        isFishing = false;
        // ȭ�� ������ �ؽ�Ʈ ��Ȱ��ȭ
        yield return new WaitForSeconds(2f);
        Debug.Log("�ٽ�");
        fishRun.gameObject.SetActive(false);
    }*/

    public void Get()
    {
        Debug.Log("����������");

        // �������� �̹��� �߰�
        Image[] _fishs = fishContent.gameObject.GetComponentsInChildren<Image>();
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
                //GameManager.instance.inventory_Fishs.Add(_inventoryFish);

                //GameManager.instance.fishs = data.fishName;
                //GameManager.instance.Save("f");
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
}
