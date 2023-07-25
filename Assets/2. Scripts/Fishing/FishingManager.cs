using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public Button FishingBtn;

    public bool isFishing = false;
    public bool useItem_white = false;  // �Ͼ� �� ���� Ȯ�� ���� ������ ���
    public bool useItem_red = false;    // ���� �� ���� Ȯ�� ���� ������ ���
    public bool useItem_rare = false;   // ���� ���� Ȯ�� ���� ������ ���
    bool config;

    FishData data;

    void Start()
    {
        //GameManager.instance.GetLog();
        InventoryImg.gameObject.SetActive(false);
        UIUpdate();
    }

    public void Fishing()
    {
        if (isFishing == false)
        {
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

        else
            return;
    }


    // ���� ��쿡 ����� ����â ���
    public void Fish(FishData fishData)
    {
        data = fishData;
        fishInfo.gameObject.SetActive(true);
        fishInfo_Txt.text = fishData.info.text;
        fish_Img.sprite = fishData.fishImg;
    }

    // �ȱ� ��ư�� ������ ����â �ݰ� �ٽ� ���� �غ�
    public void Sell()
    {
        full_Txt.gameObject.SetActive(false);
        fishInfo.gameObject.SetActive(false);
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
        // �������� �̹��� �߰�
        Image[] _fishs = FishContent.gameObject.GetComponentsInChildren<Image>();

        bool isFull = true;
        bool isChange = false;

        for (int i = 0; i < _fishs.Length; i++)
        {
            if (_fishs[i].GetComponentInChildren<Text>().text.Contains(data.fishName))
            {
                string valueToFind = data.fishName;
                int newValue = 1;

                // Ư�� ��(valueToFind)�� �����ϴ� ù ��° ����� �ε����� ã��
                int index = GameManager.instance.inventory_Fishs.FindIndex(fish => fish.fish_Name == valueToFind);

                if (index != -1)
                {
                    newValue += int.Parse(GameManager.instance.inventory_Fishs[index].fish_Count);

                    // �ش� �ε���(index)�� �� ����
                    GameManager.instance.inventory_Fishs[index].fish_Count = newValue.ToString();
                    _fishs[i].GetComponentInChildren<Text>().text = data.fishName + "   " + newValue.ToString() + "��";
                    Debug.Log("�ߺ� ���� " + index + " changed to " + newValue);
                    GameManager.instance.Save("f");
                    isChange = true;
                    isFull = false;
                    break;
                }
            }
        }
        if (isChange == false)
        {
            for (int i = 0; i < _fishs.Length; i++)
            {
                FishSlot _slot = _fishs[i].GetComponent<FishSlot>();

                if (_slot.isEmpty == false)
                {
                    _fishs[i].sprite = data.fishImg;
                    _slot.fish_ColorNum = data.color;
                    _slot.fish_GradeNum = data.grade;
                    _slot.fish_Name = data.fishName;

                    GameManager.instance.inventory_Fishs.Add(new InventoryFish(data.fishName, "1"));
                    _fishs[i].GetComponentInChildren<Text>().text = data.fishName + "   " + "1��";
                    Debug.Log("��á�� �ٸ� ����");
                    GameManager.instance.Save("f");
                    _slot.isEmpty = true;
                    isFull = false;
                    break;
                }
            }
        }
        if (isFull)
        {
            full_Txt.gameObject.SetActive(true);
        }
        else
        {
            isFishing = false;
            fishInfo.gameObject.SetActive(false);
        }

        /* bool isChange = false;
         for (int j = 0; j < GameManager.instance.inventory_Fishs.Count; j++)
         {
             if (GameManager.instance.inventory_Fishs[j].fish_Name == data.fishName)
             {
                 int count = int.Parse(GameManager.instance.inventory_Fishs[j].fish_Count);
                 count++;
                 isChange = true;
                 GameManager.instance.inventory_Fishs[j].fish_Count = count.ToString();
                 _fishs[i].GetComponentInChildren<Text>().text = data.fishName + "   " + count.ToString() + "��";

                 break;
             }
         }

         if (isChange == false)
         {
             GameManager.instance.inventory_Fishs.Add(new InventoryFish(data.fishName, "1"));
             _fishs[i].GetComponentInChildren<Text>().text = data.fishName + "   " + "1��";
         }
         GameManager.instance.Save("f");
         _slot.isEmpty = true;
         isFull = false;
         break;*/
    }


    /*else
    {
        for (int x = 0; x<_fishs.Length; x++)
        {
            if (_fishs[x].GetComponentInChildren<Text>().text == data.fishName)
            {
                string valueToFind = data.fishName;
    int newValue = 1;

    // Ư�� ��(valueToFind)�� �����ϴ� ù ��° ����� �ε����� ã��
    int index = GameManager.instance.inventory_Fishs.FindIndex(fish => fish.fish_Name == valueToFind);

                if (index != -1)
                {
                    newValue += int.Parse(GameManager.instance.inventory_Fishs[index].fish_Count);

    // �ش� �ε���(index)�� �� ����
    GameManager.instance.inventory_Fishs[index].fish_Count = newValue.ToString();
                    _fishs[x].GetComponentInChildren<Text>().text = data.fishName + "   " + newValue.ToString() + "��";
                    Debug.Log("�� á�� ���� ���� " + index + " changed to " + newValue);
                    GameManager.instance.Save("f");
                    isFull = false;
                    break;
                }
            }


                            for (int j = 0; j < GameManager.instance.inventory_Fishs.Count; j++)
    {
    if (GameManager.instance.inventory_Fishs[j].fish_Name == data.fishName)
    {
    int count = int.Parse(GameManager.instance.inventory_Fishs[j].fish_Count);
    count++;
    GameManager.instance.inventory_Fishs[j].fish_Count = count.ToString();
    _fishs[i].GetComponentInChildren<Text>().text = data.fishName + "   " + count.ToString() + "��";

    break;
    }
    }
        }
    }
    }*/

    // �������� ���� �� ��� �ؽ�Ʈ ���
    /*        if (isFull)
            {
                full_Txt.gameObject.SetActive(true);
            }
            // �ƴ� ��� ���� �г� ��Ȱ��ȭ
            else
    {
        isFishing = false;
        fishInfo.gameObject.SetActive(false);
    }
        }*/

    public void Run()
    {
        StartCoroutine(FishRun());
    }

    // ����Ⱑ ������ ��� ���� �ؽ�Ʈ ���� �ٽ� ���� �غ�
    IEnumerator FishRun()
    {
        fishRun.gameObject.SetActive(true);
        isFishing = false;
        // ȭ�� ������ �ؽ�Ʈ ��Ȱ��ȭ
        yield return new WaitForSeconds(2f);
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

    /*public void LogOut()
    {
        GPGSBinder.Inst.Logout();
    }*/
}
