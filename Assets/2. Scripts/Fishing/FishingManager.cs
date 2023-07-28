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
    public Text atkTxt;
    public Text fishInfo_Txt;
    public Text full_Txt;
    public Text fishRun;
    public Text useWhiteItemTxt;
    public Text useRedItemTxt;
    public Text useRareItemTxt;
    public Text scoreTxt;
    public GameObject configPanel;
    public GameObject inventoryImg;  // �κ��丮 �̹���
    public GameObject inventoryBtn;
    public GameObject fishInfoImg;
    public GameObject fishContent; // ������
    public GameObject fishInfo; // ���� ���� �ǳ�
    public GameObject fishingRod; // ���˴� �̹���
    public GameObject lineStartPos;
    public GameObject useItemPanel;
    public GameObject fishObj;
    public Button fishingBtn;
    public Image fish_Img;
    public bool isFishing = false;
    public bool useItem_white = false;  // �Ͼ� �� ���� Ȯ�� ���� ������ ���
    public bool useItem_red = false;    // ���� �� ���� Ȯ�� ���� ������ ���
    public bool useItem_rare = false;   // ���� ���� Ȯ�� ���� ������ ���

    Vector3 fishInfoOriginPos;
    Vector3 fishInfoOriginScale;
    bool config;
    FishData data;

    void Start()
    {
        fishInfoOriginScale = Vector3.one;
        fishInfoOriginPos = fishInfoImg.transform.position;
        useItemPanel.gameObject.SetActive(false);
        useRareItemTxt.gameObject.SetActive(false);
        useRedItemTxt.gameObject.SetActive(false);
        useWhiteItemTxt.gameObject.SetActive(false);
        //GameManager.instance.GetLog();
        inventoryImg.gameObject.SetActive(false);
        UIUpdate();
    }

    public void Delete()
    {
        GameManager.instance.DeleteData();
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
            LineRenderer fishLine = fishingRod.GetComponent<LineRenderer>();

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
        fishInfoImg.gameObject.SetActive(true);
        fishInfoImg.transform.parent = fishInfo.transform;
        fishInfoImg.transform.position = fishInfoOriginPos;
        fishInfoImg.transform.localScale = fishInfoOriginScale;
        fishInfoImg.transform.SetSiblingIndex(0);
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

        GameManager.instance.todayData.gold += data.gold;
        //GameManager.instance.Save("d");
        Debug.Log("��� " + _gold);
        // ��� ++
        UIUpdate();
        isFishing = false;
    }

    // ���������� ��ư ������ ����â �ݰ� �ٽ� ���� �غ�
    public void Get()
    {
        // �������� �̹��� �߰�
        Image[] _fishs = fishContent.gameObject.GetComponentsInChildren<Image>();

        bool isFull = true;
        bool isChange = false;

        for (int i = 0; i < _fishs.Length; i++)
        {
            if (_fishs[i].gameObject.name.Contains("Slot"))
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

                        GameManager.instance.todayFishInfos.Add(new TodayFishInfo(data.fishName, 1));

                        // �ش� �ε���(index)�� �� ����
                        GameManager.instance.inventory_Fishs[index].fish_Count = newValue.ToString();
                        _fishs[i].GetComponentInChildren<Text>().text = data.fishName + "   " + newValue.ToString() + " " + "����";
                        Debug.Log("�ߺ� ���� " + index + " changed to " + newValue);
                        fishInfoImg.transform.parent = inventoryBtn.transform;
                        StartCoroutine(Eff());
                        StartCoroutine(EffMove());
                        //GameManager.instance.Save("f");
                        isChange = true;
                        isFull = false;
                        break;
                    }
                }
            }
        }

        if (isChange == false)
        {
            for (int i = 0; i < _fishs.Length; i++)
            {
                if (_fishs[i].gameObject.name.Contains("Slot"))
                {
                    FishSlot _slot = _fishs[i].GetComponent<FishSlot>();
                    Image Img = null;
                    Image[] fishImgs = _fishs[i].GetComponentsInChildren<Image>();
                    foreach (Image fishImg in fishImgs)
                    {
                        if (fishImg.name.Contains("Img"))
                            Img = fishImg;
                    }

                    if (_slot.isEmpty == false)
                    {
                        Img.sprite = data.fishImg;
                        _slot.fish_ColorNum = data.color;
                        _slot.fish_GradeNum = data.grade;
                        _slot.fish_Name = data.fishName;

                        GameManager.instance.inventory_Fishs.Add(new InventoryFish(data.fishName, "1"));

                        GameManager.instance.todayFishInfos.Add(new TodayFishInfo(data.fishName, 1));

                        _fishs[i].GetComponentInChildren<Text>().text = data.fishName + "   " + "1 ����";
                        Debug.Log("��á�� �ٸ� ����");
                        //GameManager.instance.Save("f");
                        fishInfoImg.transform.parent = inventoryBtn.transform;
                        StartCoroutine(Eff());
                        StartCoroutine(EffMove());
                        _slot.isEmpty = true;
                        isFull = false;
                        break;
                    }
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
    }

    IEnumerator Eff()
    {
        Vector3 initialScale = new Vector3(1.1f, 1.1f, 1.1f);
        Vector3 targetScale = new Vector3(0.3f, 0.3f, 0.3f);
        float duration = 1f; // ũ�� ��ȭ�� �ɸ��� �ð�

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // �ð� ���� ���
            fishInfoImg.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }
    }

    IEnumerator EffMove()
    {
        float elapsedTime = 0f;
        float duration = 1f; // �̵� �ð� (��)

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            fishInfoImg.transform.position = Vector3.Lerp(fishInfoImg.transform.position, inventoryBtn.transform.position, elapsedTime / duration);

            if(elapsedTime / duration >= 1)
            {
                fishInfoImg.transform.position = inventoryBtn.transform.position;
                fishInfoImg.gameObject.SetActive(false);
            }
            yield return null;
        }
    }

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

    public void UIUpdate()
    {
        dateTxt.text = GameManager.instance.data.dateCount + "����";
        scoreTxt.text = "���� : " + GameManager.instance.data.score;
        goldTxt.text = "gold : " + GameManager.instance.data.gold;
        atkTxt.text = "���ݷ� : " + GameManager.instance.data.atk;
    }

    public void ViewInventory()
    {
        // �κ��丮 Ȱ��ȭ
        inventoryImg.gameObject.SetActive(true);
        // �κ��丮 ������ ���� ����������
        inventoryImg.transform.SetAsLastSibling();
    }

    public void EscInventory()
    {
        inventoryImg.gameObject.SetActive(false);
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
