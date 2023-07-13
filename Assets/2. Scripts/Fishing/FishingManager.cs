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
    FishData data;

    void Start()
    {
        InventoryImg.gameObject.SetActive(false);
        UIUpdate();
    }

    void Update()
    {
 
        // ��ġ�� �ϰ� ����� ��� ���� �ƴ϶��
        if (Input.GetMouseButtonDown(0) && isFishing == false)
        {
            Debug.Log("���� ����");
            Debug.Log("��ġ" + Input.mousePosition);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (!hit.collider.CompareTag("UI"))
                {
                    return;
                }
            }

            else
            {
                // ����� ��� ������ ����
                isFishing = true;
                // ����� ���� �ؽ�Ʈ ��Ȱ��ȭ
                fishRun.gameObject.SetActive(false);

                Instantiate(fishObj, Input.mousePosition, Quaternion.identity);
            }
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

        fishInfo.gameObject.SetActive(false);
        GameManager.instance.gold += data.gold;
        GameManager.instance.SetGold();
        // ��� ++
        UIUpdate();
        isFishing = false;
    }

    // ���������� ��ư ������ ����â �ݰ� �ٽ� ���� �غ�
    public void Get()
    {
        Debug.Log("����������");

        fishInfo.gameObject.SetActive(false);
        // �������� �̹��� �߰�
        Image[] _fishs = FishContent.GetComponentsInChildren<Image>();
        Debug.Log("������ ĭ �� : " + _fishs.Length);

        for (int i = 0; i < _fishs.Length; i++)
        {
            Slot _slot = _fishs[i].GetComponent<Slot>();
            if (_slot.isEmpty == false)
            {
                _fishs[i].sprite = data.fishImg;
                _slot.isEmpty = true;
                break;
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
        yield return new WaitForSeconds(2f);
        Debug.Log("�ٽ�");
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
