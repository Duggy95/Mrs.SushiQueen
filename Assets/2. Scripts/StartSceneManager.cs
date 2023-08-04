using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public GameObject _storyManager;
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

    AudioSource audioSource;
    bool config;
    bool isStart;

    private void Awake()
    {
        Time.timeScale = 1;
        audioSource = GetComponent<AudioSource>();
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
                blackCanvas.gameObject.SetActive(false);
                mainObj.gameObject.SetActive(false);
                storyObj.gameObject.SetActive(false);
                modeObj.gameObject.SetActive(true);
                _storyManager.gameObject.SetActive(false);
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
                _storyManager.gameObject.SetActive(false);
                mainObj.gameObject.SetActive(false);
                storyObj.gameObject.SetActive(true);
                modeObj.gameObject.SetActive(false);
            }
            else
            {
                isStart = false;
                _storyManager.gameObject.SetActive(false);
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
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        // 인벤토리 활성화
        inventoryImg.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
        print("인벤토리 열려라");
    }

    public void EscInventory()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        inventoryImg.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void OnClickSkip()
    {
        /*mainObj.gameObject.SetActive(false);
        storyObj.gameObject.SetActive(false);
        modeObj.gameObject.SetActive(true);
        UIUpdate();*/
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        SceneManager.LoadScene(4);
    }

    public void FishingQuestionEsc()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        fishingQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void FishingQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        fishingQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void CookQuestionEsc()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        cookQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void CookQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        cookQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void ExitGameQuestionEsc()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        exitGameQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void ExitGameQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        exitGameQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void DeleteDataQuestionEsc()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        deleteDataQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void DeleteDataQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        deleteDataQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void LogOutQuestionEsc()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        logOutQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void LogOutQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        logOutQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void GoFishing()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        SceneManager.LoadScene(1);
    }

    public void GoShop()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        SceneManager.LoadScene(2);
    }

    public void ConfigBtn()
    {
        if (!config)
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

            configPanel.SetActive(true);
            inventoryFullImg.gameObject.SetActive(true);
            config = true;
        }
        else
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

            configPanel.SetActive(false);
            inventoryFullImg.gameObject.SetActive(false);
            config = false;
        }
    }

    public void ExitGame()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        Application.Quit();
    }

    /*public void LogOut()
    {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        GPGSBinder.Inst.Logout();
        logOutQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    GameManager.instance.nextStage = false;
                 SceneManager.LoadScene(0);

    }*/

    public void Delete()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        GameManager.instance.DeleteData();
        UIUpdate();
        GameManager.instance.nextStage = false;
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
