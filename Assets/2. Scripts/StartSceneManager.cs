using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class StartSceneManager : MonoBehaviour
{
    public Camera mainCam;  //Ÿ��Ʋȭ��
    public Camera storyCam;  //���丮ȭ��
    public Camera modeCam;  //����ȭ��
    public Text startTxt;  //��ġ�ؼ� ���ӽ��� �ؽ�Ʈ
    public Text storyTxt;  //���丮 �ؽ�Ʈ
    public string[] story;  //���丮 ����
    public Text dateTxt;  //��¥ + ���� �ؽ�Ʈ
    public Text goldTxt;  //��� �ؽ�Ʈ
    public GameObject inventoryImg; //�κ��丮 �̹���

    bool isStart;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            if (!GameManager.instance.nextStage) //
            {
                mainCam.gameObject.SetActive(true);
                storyCam.gameObject.SetActive(false);
                modeCam.gameObject.SetActive(false);
                isStart = true;
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
        if (isStart && Input.GetMouseButtonDown(0))
        {
            isStart = false;
            mainCam.gameObject.SetActive(false);
            storyCam.gameObject.SetActive(true);
            modeCam.gameObject.SetActive(false);
        }

        UIUpdate();
    }

    void UIUpdate()
    {
        dateTxt.text = GameManager.instance.save.dateCount + "���� / ���� : " + GameManager.instance.save.score;
        goldTxt.text = "gold : " + GameManager.instance.save.gold;
    }

    public void ViewInventory()
    {
        // �κ��丮 Ȱ��ȭ
        inventoryImg.gameObject.SetActive(true);
    }

    public void EscInventory()
    {
        inventoryImg.gameObject.SetActive(false);
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
