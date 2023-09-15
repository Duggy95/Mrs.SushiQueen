using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private List<TutorialBase> tutorials;

    public GameObject inventoryImg;
    public GameObject modeCanvas;
    public GameObject fishCanvas;
    public GameObject cookCanvas;
    public GameObject endCanvas;
    public GameObject fishingQuestion;
    public GameObject cookingQuestion;
    public GameObject endingQuestion;
    public GameObject skipQuestion;
    public GameObject exitQuestion;
    public GameObject nextDayQuestion;
    public GameObject noMoneyTxt;
    public GameObject inventoryFullImg;
    public GameObject bonusQuestion;
    public GameObject fishingSV;
    public GameObject ShopSV;
    public GameObject SkillSV;
    public GameObject useItemPanel;
    public CanvasGroup inventoryBtn;
    public Image fishBtn;
    public Image shopBtn;
    public Image skillBtn;
    public Image itemSlotImg;
    public Text itemSlotTxt;
    public Text useWhiteItemTxt;
    public bool fishScene;

    Text[] bonusTxt;
    AudioSource audioSource;
    TutorialBase currentTutorial = null;
    Color initColor = new Color(1, 1, 1, 1);
    Color selColor = new Color(1, 1, 0, 1);

    int currentIndex = -1;
    bool isSkip;
    bool isInventory = false;
    bool config;

    void Start()
    {
        // GameManager.instance.LogData();

        fishBtn.color = selColor;
        shopBtn.color = initColor;
        skillBtn.color = initColor;

        SetNextTutorial();
        //print(tutorials.Count);
        modeCanvas.SetActive(true);
        fishCanvas.SetActive(false);
        cookCanvas.SetActive(false);
        endCanvas.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        bonusTxt = bonusQuestion.GetComponentsInChildren<Text>();
    }

    void Update()
    {
        if (currentTutorial != null)
        {
            currentTutorial.Execute(this);
        }
    }

    public void SetNextTutorial()
    {
        //현재 튜토리얼의 Exit() 메소드 호출
        if (currentTutorial != null)
        {
            currentTutorial.Exit();
        }

        //마지막 튜토리얼을 진행했다면 CompleteAllTutorials() 메소드 호출
        if (currentIndex >= tutorials.Count - 1)
        {
            //CompletedAllTutorials();
            return;
        }

        //다음 튜토리얼 과정을 currentTutorial로 등록
        currentIndex++;
        currentTutorial = tutorials[currentIndex];
        if (currentIndex >= 16 && currentIndex <= 30)
        {
            inventoryBtn.interactable = false;
        }
        else if (currentIndex >= 31)
        {
            inventoryBtn.interactable = true;
        }

        //새로 바뀐 튜토리얼의 Enter() 메소드 호출
        currentTutorial.Enter();
    }

    public void CompletedAllTutorials()
    {
        currentTutorial = null;
        tutorials.Clear();
        //행동 양식이 여러 종류가 되었을 때 코드 추가 작성
        //현재는 씬 전환.
        //Debug.Log("Complete All");

        if (isSkip)
        {
            GameManager.instance.data.dateCount = "1";
            GameManager.instance.data.gold = "300000";
        }
        else
        {
            GameManager.instance.data.dateCount = "1";
            GameManager.instance.data.gold = "500000";
        }

       // print("씬 이동");
        GameManager.instance.nextStage = true;
        SceneManager.LoadScene(0);
    }

    public void ShowFishScene()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        fishCanvas.SetActive(true);
        fishScene = true;
    }

    public void FishingQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        fishingQuestion.gameObject.SetActive(true);
    }

    public void EscFishingQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        fishingQuestion.gameObject.SetActive(false);
    }

    public void ShowCookScene()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        cookCanvas.SetActive(true);
    }

    public void CookingQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        cookingQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void EscCookingQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        cookingQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void ShowEndScene()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        endCanvas.SetActive(true);
    }

    public void EndingQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        endingQuestion.gameObject.SetActive(true);
    }

    public void EscEndingQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        endingQuestion.gameObject.SetActive(false);
    }

    /*public void ConfigBtn() //설정보여주기
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        if (!config)
        {
            configPanel.SetActive(true);
            config = true;
            Time.timeScale = 0;
        }
        else
        {
            configPanel.SetActive(false);
            config = false;
            Time.timeScale = 1;
        }
    }*/

    public void ExitQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        exitQuestion.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        Application.Quit();
    }

    public void EscExitQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        exitQuestion.gameObject.SetActive(false);
    }

    public void SkipBtn()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        skipQuestion.SetActive(true);
        bonusTxt[0].text = "시작지원금 300,000원이 \n" + "지급되었습니다.";
    }

    public void CloseSkipBtn()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        skipQuestion.SetActive(false);
    }

    public void GoSkipBtn()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        //CompletedAllTutorials();
        bonusQuestion.SetActive(true);
        isSkip = true;
    }

    public void NextDayBtn()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        //CompletedAllTutorials();
        bonusQuestion.SetActive(true);
        bonusTxt[0].text = "시작지원금 500,000원이 \n" + "지급되었습니다.";
    }

    public void NextDayQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        nextDayQuestion.gameObject.SetActive(true);
    }

    public void EscNextDayQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        nextDayQuestion.gameObject.SetActive(false);
    }

    public void BonusYesBtn()
    {
        CompletedAllTutorials();
    }

    public void InventoryBtn() //인벤토리 활성화, 비활성화
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        if (!isInventory)
        {
            inventoryImg.gameObject.SetActive(true);
            inventoryFullImg.gameObject.SetActive(true);
            isInventory = true;
        }
        else
        {
            inventoryImg.gameObject.SetActive(false);
            inventoryFullImg.gameObject.SetActive(false);
            isInventory = false;
        }
    }

    public void NoMoneyBtn()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        StartCoroutine(NoMoney());
    }

    public IEnumerator NoMoney()
    {
        noMoneyTxt.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        noMoneyTxt.gameObject.SetActive(false);
    }

    public void OnclickSkill()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        fishBtn.color = initColor;
        shopBtn.color = initColor;
        skillBtn.color = selColor;
        fishingSV.gameObject.SetActive(false);
        ShopSV.gameObject.SetActive(false);
        SkillSV.gameObject.SetActive(true);
        noMoneyTxt.gameObject.SetActive(false);
    }

    public void OnclickShop()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        fishBtn.color = initColor;
        shopBtn.color = selColor;
        skillBtn.color = initColor;
        fishingSV.gameObject.SetActive(false);
        ShopSV.gameObject.SetActive(true);
        SkillSV.gameObject.SetActive(false);
        noMoneyTxt.gameObject.SetActive(false);
    }

    public void OnclickFishing()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        fishBtn.color = selColor;
        shopBtn.color = initColor;
        skillBtn.color = initColor;
        fishingSV.gameObject.SetActive(true);
        ShopSV.gameObject.SetActive(false);
        SkillSV.gameObject.SetActive(false);
        noMoneyTxt.gameObject.SetActive(false);
    }

    public void UseItem()
    {
        itemSlotImg.sprite = null;
        itemSlotTxt.text = null;
        inventoryImg.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
        useItemPanel.gameObject.SetActive(true);
        useWhiteItemTxt.gameObject.SetActive(true);
    }
}
