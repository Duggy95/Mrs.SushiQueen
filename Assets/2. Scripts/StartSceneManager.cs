using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class StartSceneManager : MonoBehaviour
{
    public GameObject configPanel;
    public GameObject mainObj;
    public GameObject storyObj;
    public GameObject modeObj;
    public Text startTxt;  //��ġ�ؼ� ���ӽ��� �ؽ�Ʈ
    public Text storyTxt;  //���丮 �ؽ�Ʈ
    public string[] story;  //���丮 ����
    public Text dateTxt;  //��¥ + ���� �ؽ�Ʈ
    public Text goldTxt;  //��� �ؽ�Ʈ
    public GameObject inventoryImg; //�κ��丮 �̹���
    public Image backGround;  //���丮 ��� �׸�
    public Sprite[] sprites;
    //public GameObject loginObj;
    int storyCount = 0;
    bool config;
    bool isStart;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            if (!GameManager.instance.nextStage) //
            {
                mainObj.gameObject.SetActive(true);
                storyObj.gameObject.SetActive(false);
                modeObj.gameObject.SetActive(false);
                isStart = true;
            }

            else //
            {
                mainObj.gameObject.SetActive(false);
                storyObj.gameObject.SetActive(false);
                modeObj.gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        /*if(GameManager.instance.onLogin == false)
        {
            loginObj.gameObject.SetActive(true);
            startTxt.gameObject.SetActive(false);
        }

        else if(GameManager.instance.onLogin)
        {
            loginObj.gameObject.SetActive(false);
            startTxt.gameObject.SetActive(true);
        }*/

        if (GameManager.instance.onLogin && isStart && Input.GetMouseButtonDown(0))
        {
            isStart = false;
            mainObj.gameObject.SetActive(false);
            storyObj.gameObject.SetActive(true);
            modeObj.gameObject.SetActive(false);
        }

        UIUpdate();
    }

    void UIUpdate()
    {
        dateTxt.text = GameManager.instance.data.dateCount + "���� / ���� : " + GameManager.instance.data.score;
        goldTxt.text = "gold : " + GameManager.instance.data.gold;
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

    public void OnClickSkip()
    {
        mainObj.gameObject.SetActive(false);
        storyObj.gameObject.SetActive(false);
        modeObj.gameObject.SetActive(true);
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

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Story()
    {
        storyTxt.text = story[storyCount];
        backGround.sprite = sprites[storyCount];
        storyCount++;
        if (storyCount == story.Length)
        {
            storyObj.gameObject.SetActive(false);
            modeObj.gameObject.SetActive(true);
        }
    }
}
