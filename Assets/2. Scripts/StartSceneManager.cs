using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public GameObject configPanel;
    public GameObject mainObj;
    public GameObject storyObj;
    public GameObject modeObj;
    public Text startTxt;  //터치해서 게임시작 텍스트
    public Text storyTxt;  //스토리 텍스트
    public string[] story;  //스토리 내용
    public Text dateTxt;  //날짜 + 평판 텍스트
    public Text scoreTxt;
    public Text goldTxt;  //골드 텍스트
    public Text atkTxt;
    public GameObject inventoryImg; //인벤토리 이미지
    public GameObject inventoryFullImg;
    public Image backGround;  //스토리 배경 그림
    public Sprite[] sprites;
    public GameObject loginObj;
    public GameObject fishingQuestion;
    public GameObject cookQuestion;
    public GameObject logOutQuestion;
    public GameObject deleteDataQuestion;
    public GameObject exitGameQuestion;

    int storyCount = 0;
    bool config;
    bool isStart;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            //GameManager.instance.GetLog();

            /*if(GPGSBinder.Inst.LoginS())
                loginObj.gameObject.SetActive(false);*/

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
                GameManager.instance.Save("d");
                GameManager.instance.Save("i");
                GameManager.instance.Save("f");
                GameManager.instance.todayData = new TodayData();
                GameManager.instance.todayFishInfos.Clear();
                /*GameManager.instance.viewInventory = false;
                GameManager.instance.viewReceipt = false;*/
                UIUpdate();
            }
        }
    }

    private void Update()
    {
        /*if (GPGSBinder.Inst.LoginS())
            loginObj.gameObject.SetActive(false);

        if (GPGSBinder.Inst.LoginS() && isStart && Input.GetMouseButtonDown(0))
        {
            isStart = false;
            mainObj.gameObject.SetActive(false);
            storyObj.gameObject.SetActive(true);
            modeObj.gameObject.SetActive(false);
        }        */

        if (isStart && Input.GetMouseButtonDown(0))
        {
            isStart = false;
            mainObj.gameObject.SetActive(false);
            storyObj.gameObject.SetActive(true);
            modeObj.gameObject.SetActive(false);
        }
    }

    /*    public void UIUpdate()
        {
            dateTxt.text = GameManager.instance.data.dateCount + "일차";
            scoreTxt.text = "평판 : " + GameManager.instance.data.score;
            goldTxt.text = "gold : " + GameManager.instance.data.gold;
            atkTxt.text = "공격력 : " + GameManager.instance.data.atk;
        }*/

    public void UIUpdate()
    {
        dateTxt.text = GameManager.instance.data.dateCount;
        scoreTxt.text = GameManager.instance.data.score;
        goldTxt.text = GameManager.instance.data.gold;
        atkTxt.text = GameManager.instance.data.atk;
    }

    public void ViewInventory()
    {
        // 인벤토리 활성화
        inventoryImg.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);

        print("인벤토리 열려라");
    }

    public void EscInventory()
    {
        inventoryImg.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void OnClickSkip()
    {
        mainObj.gameObject.SetActive(false);
        storyObj.gameObject.SetActive(false);
        modeObj.gameObject.SetActive(true);
        UIUpdate();
    }

    public void FishingQuestionEsc()
    {
        fishingQuestion.gameObject.SetActive(false);
    }

    public void FishingQuestion()
    {
        /*if (GameManager.instance.viewInventory)
            return;*/

        fishingQuestion.gameObject.SetActive(true);
    }

    public void CookQuestionEsc()
    {
        cookQuestion.gameObject.SetActive(false);
    }

    public void CookQuestion()
    {
        cookQuestion.gameObject.SetActive(true);
    }

    public void ExitGameQuestionEsc()
    {
        exitGameQuestion.gameObject.SetActive(false);
    }

    public void ExitGameQuestion()
    {
        exitGameQuestion.gameObject.SetActive(true);
    }

    public void DeleteDataQuestionEsc()
    {
        deleteDataQuestion.gameObject.SetActive(false);
    }

    public void DeleteDataQuestion()
    {
        deleteDataQuestion.gameObject.SetActive(true);
    }

    public void LogOutQuestionEsc()
    {
        logOutQuestion.gameObject.SetActive(false);
    }

    public void LogOutQuestion()
    {
        logOutQuestion.gameObject.SetActive(true);
    }

    public void GoFishing()
    {
        SceneManager.LoadScene(1);
    }

    public void GoShop()
    {
        SceneManager.LoadScene(2);
    }

    /*public void GoUpgrade()
    {
        SceneManager.LoadScene(3);
    }*/

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

   /* public void LogOut()
    {
        GPGSBinder.Inst.Logout();
    }*/

    public void Delete()
    {
        GameManager.instance.DeleteData();
        UIUpdate();
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
