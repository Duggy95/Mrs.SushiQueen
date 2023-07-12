using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class FishingManager : MonoBehaviour //, IPointerClickHandler
{
    public Canvas canvas;
    public Text dateTxt;
    public Text goldTxt;
    public GameObject InventoryImg;  // �κ��丮 �̹���
    public GameObject fishInfo; // ���� ���� �ǳ�
    public GameObject fishObj;
    public Text fishRun;
    public Sprite[] fishImg;  // ���� �̹��� �迭
    public bool isFishing = false;

    void Start()
    {
        UIUpdate();
    }

    /*public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 clickPos = eventData.position;
        print(clickPos);
        //var fishHpBar = Instantiate(hpBarPrefab, clickPos, new Quaternion(0, 0, 0, 0));
    }*/

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
    public void Fish()
    {
        Debug.Log("���� ����");

        fishInfo.gameObject.SetActive(true);
    }

    // �ȱ� ��ư�� ������ ����â �ݰ� �ٽ� ���� �غ�
    public void Sell()
    {
        Debug.Log("�Ǹ�");

        fishInfo.gameObject.SetActive(false);
        // ��� ++
        // UIUpdate();
        isFishing = false;
    }

    // ���������� ��ư ������ ����â �ݰ� �ٽ� ���� �غ�
    public void Get()
    {
        Debug.Log("����������");

        fishInfo.gameObject.SetActive(false);
        // �������� �̹��� �߰�
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
