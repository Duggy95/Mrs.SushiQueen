using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class StartSceneCtrl : MonoBehaviour
{
    public Camera mainCam;  //Ÿ��Ʋȭ��
    public Camera storyCam;  //���丮ȭ��
    public Camera modeCam;  //����ȭ��
    public Text StartTxt;  //��ġ�ؼ� ���ӽ��� �ؽ�Ʈ
    public Text[] storytxt;  //���丮 �ؽ�Ʈ
    public Text dateTxt;  //��¥ + ���� �ؽ�Ʈ
    public Text goldTxt;  //��� �ؽ�Ʈ
    public GameObject InventoryImg; //�κ��丮 �̹���

    int count = 0;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            if (!GameManager.instance.nextStage) //
            {
                mainCam.gameObject.SetActive(true);
                storyCam.gameObject.SetActive(false);
                modeCam.gameObject.SetActive(false);
            }

            else //
            {
                mainCam.gameObject.SetActive(false);
                storyCam.gameObject.SetActive(false);
                modeCam.gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (count == 0 && Input.GetMouseButtonDown(0))
        {
            count++;
            mainCam.gameObject.SetActive(false);
            storyCam.gameObject.SetActive(true);
            modeCam.gameObject.SetActive(false);
        }

        UIUpdate();
    }

    public void UIUpdate()
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

 /*   public void GoHome()
    {
        SceneManager.LoadScene(0);
    }*/

    // ��ġ�ؼ� ���� �����ϴ� �Լ� ������ ��

    public void OnClickSkip()
    {
        mainCam.gameObject.SetActive(false);
        storyCam.gameObject.SetActive(false);
        modeCam.gameObject.SetActive(true);
    }

    public void GoFishing()
    {
        SceneManager.LoadScene(1);
    }

    public void GoShop()
    {
        SceneManager.LoadScene(2);
    }

    public void GoUpgrade()
    {
        SceneManager.LoadScene(3);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
