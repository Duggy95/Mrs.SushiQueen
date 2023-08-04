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
    //public Image backGround;  //스토리 배경 그림
    //public Sprite[] sprites;
    public GameObject loginObj;
    public GameObject fishingQuestion;
    public GameObject cookQuestion;
    public GameObject logOutQuestion;
    public GameObject deleteDataQuestion;
    public GameObject exitGameQuestion;
    public StoryManager storyManager;
    public CanvasGroup blackCanvas;

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
                StartCoroutine(FadeAway());
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
        }*/

        if (isStart && Input.GetMouseButtonDown(0))
        {
            if(GameManager.instance.data.dateCount == "1")
            {
                isStart = false;
                mainObj.gameObject.SetActive(false);
                storyObj.gameObject.SetActive(true);
                modeObj.gameObject.SetActive(false);
            }
            else
            {
                isStart = false;
                mainObj.gameObject.SetActive(false);
                storyObj.gameObject.SetActive(false);
                modeObj.gameObject.SetActive(true);
                UIUpdate();
            }
        }
    }

    public void UIUpdate()
    {
        dateTxt.text = int.Parse(GameManager.instance.data.dateCount).ToString("N0");
        scoreTxt.text = int.Parse(GameManager.instance.data.score).ToString("N0");
        goldTxt.text = int.Parse(GameManager.instance.data.gold).ToString("N0");
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
        /*mainObj.gameObject.SetActive(false);
        storyObj.gameObject.SetActive(false);
        modeObj.gameObject.SetActive(true);
        UIUpdate();*/

        SceneManager.LoadScene(4);
    }

    public void FishingQuestionEsc()
    {
        fishingQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void FishingQuestion()
    {
        fishingQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void CookQuestionEsc()
    {
        cookQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void CookQuestion()
    {
        cookQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void ExitGameQuestionEsc()
    {
        exitGameQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void ExitGameQuestion()
    {
        exitGameQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void DeleteDataQuestionEsc()
    {
        deleteDataQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void DeleteDataQuestion()
    {
        deleteDataQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void LogOutQuestionEsc()
    {
        logOutQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void LogOutQuestion()
    {
        logOutQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void GoFishing()
    {
        SceneManager.LoadScene(1);
    }

    public void GoShop()
    {
        SceneManager.LoadScene(2);
    }

    public void ConfigBtn()
    {
        if (!config)
        {
            configPanel.SetActive(true);
            inventoryFullImg.gameObject.SetActive(true);
            config = true;
        }
        else
        {
            configPanel.SetActive(false);
            inventoryFullImg.gameObject.SetActive(false);
            config = false;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    /*public void LogOut()
    {
        GPGSBinder.Inst.Logout();
        logOutQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
            ExitGame();
    }*/

    public void Delete()
    {
        GameManager.instance.DeleteData();
        UIUpdate();
        SceneManager.LoadScene(0);
    }

    IEnumerator FadeAway()
    {
        blackCanvas.alpha = 1;
        blackCanvas.blocksRaycasts = true;

        yield return new WaitForSeconds(1);

        float duration = 1f;
        float elapsedTime = 0f;
        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float alpha = 1 - t;
            blackCanvas.alpha = alpha;
            yield return null;
        }

        blackCanvas.blocksRaycasts = false;
    }
}
