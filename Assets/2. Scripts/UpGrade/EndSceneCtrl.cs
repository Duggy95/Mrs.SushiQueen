using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSceneCtrl : MonoBehaviour
{
    public Text dateTxt;
    public Text goldTxt;
    public Text atkTxt;
    public Text scoreTxt;
    public GameObject configPanel;
    public GameObject fishingSV;  // 낚시용품 스크롤뷰
    public GameObject ShopSV;  // 가게 업그레이드 스크롤뷰
    public GameObject SkillSV;  // 스킬 업그레이드 스크롤뷰
    public GameObject InventoryImg;
    public GameObject inventoryFullImg;
    public GameObject noMoneyTxt;  // 돈이 부족할 때
    public GameObject maxLevelTxt;  // 최대 레벨일 때
    public GameObject fullTxt;  // 인벤토리가 가득 찬 경우
    public GameObject receipt;  // 영수증
    public GameObject endSceneQuestion;
    public GameObject logOutQuestion;
    public GameObject deleteDataQuestion;
    public GameObject exitGameQuestion;
    // public GameObject admobQuestion;  // 광고
    public Image fishBtn;
    public Image shopBtn;
    public Image skillBtn;
    // public bool rewardAdSuccess;

    AudioSource audioSource;
    Color initColor = new Color(1, 1, 1, 1);
    Color selColor = new Color(1, 1, 0, 1);

    bool config;

    private void Awake()
    {
        Time.timeScale = 1;
        audioSource = GetComponent<AudioSource>();
        fishBtn.color = selColor;
        shopBtn.color = initColor;
        skillBtn.color = initColor;
        receipt.gameObject.SetActive(false);
        fishingSV.gameObject.SetActive(true);
        ShopSV.gameObject.SetActive(false);
        SkillSV.gameObject.SetActive(false);
        maxLevelTxt.gameObject.SetActive(false);
        noMoneyTxt.gameObject.SetActive(false);
        fullTxt.gameObject.SetActive(false);
        UIUpdate();
    }

    private void Start()
    {
        ViewReceipt();
    }

    /*public void Reward()
    {
        if (rewardAdSuccess)
        {
            GameManager.instance.nextStage = true;
            int _date = int.Parse(GameManager.instance.data.dateCount) + 1;
            int rewardGold = int.Parse(GameManager.instance.data.gold) + 30000;
            GameManager.instance.data.dateCount = _date.ToString();
            GameManager.instance.data.gold = rewardGold.ToString();
            SceneManager.LoadScene(0);
            rewardAdSuccess = false;
        }
    }*/

    public void OnclickSkill()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        fishBtn.color = initColor;
        shopBtn.color = initColor;
        skillBtn.color = selColor;
        fishingSV.gameObject.SetActive(false);
        ShopSV.gameObject.SetActive(false);
        SkillSV.gameObject.SetActive(true);
        maxLevelTxt.gameObject.SetActive(false);
        noMoneyTxt.gameObject.SetActive(false);
        fullTxt.gameObject.SetActive(false);
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
        maxLevelTxt.gameObject.SetActive(false);
        noMoneyTxt.gameObject.SetActive(false);
        fullTxt.gameObject.SetActive(false);
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
        maxLevelTxt.gameObject.SetActive(false);
        noMoneyTxt.gameObject.SetActive(false);
        fullTxt.gameObject.SetActive(false);
    }

    public void NextStage()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        GameManager.instance.nextStage = true;
        int _date = int.Parse(GameManager.instance.data.dateCount) + 1;
        GameManager.instance.data.dateCount = _date.ToString();
        SceneManager.LoadScene(0);
    }

    public void Delete()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        GameManager.instance.DeleteData();
        UIUpdate();
        GameManager.instance.nextStage = false;
        SceneManager.LoadScene(0);
    }

    public void UIUpdate()
    {
        dateTxt.text = int.Parse(GameManager.instance.data.dateCount).ToString("N0");
        scoreTxt.text = int.Parse(GameManager.instance.data.score).ToString("N0");
        goldTxt.text = int.Parse(GameManager.instance.data.gold).ToString("N0");
        atkTxt.text = GameManager.instance.data.atk;
    }

    /*public void AdmobQuestion()
    {
        admobQuestion.gameObject.SetActive(true);
    }*/

    public void EndSceneQuestionEsc()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        inventoryFullImg.gameObject.SetActive(false);
        endSceneQuestion.gameObject.SetActive(false);
    }

    public void EndSceneQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        inventoryFullImg.gameObject.SetActive(true);
        endSceneQuestion.gameObject.SetActive(true);
    }

    public void ExitGameQuestionEsc()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        inventoryFullImg.gameObject.SetActive(false);
        exitGameQuestion.gameObject.SetActive(false);
    }

    public void ExitGameQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        inventoryFullImg.gameObject.SetActive(true);
        exitGameQuestion.gameObject.SetActive(true);
    }

    public void DeleteDataQuestionEsc()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        inventoryFullImg.gameObject.SetActive(false);
        deleteDataQuestion.gameObject.SetActive(false);
    }

    public void DeleteDataQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        inventoryFullImg.gameObject.SetActive(true);
        deleteDataQuestion.gameObject.SetActive(true);
    }

    public void LogOutQuestionEsc()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        inventoryFullImg.gameObject.SetActive(false);
        endSceneQuestion.gameObject.SetActive(false);
    }

    public void LogOutQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        inventoryFullImg.gameObject.SetActive(true);
        logOutQuestion.gameObject.SetActive(true);
    }

    public void ViewReceipt()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        inventoryFullImg.gameObject.SetActive(true);
        receipt.gameObject.SetActive(true);
    }

    public void EscReceipt()
    {

        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        inventoryFullImg.gameObject.SetActive(false);
        receipt.gameObject.SetActive(false);
    }

    public void ViewInventory()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        // 인벤토리 활성화
        inventoryFullImg.gameObject.SetActive(true);
        InventoryImg.gameObject.SetActive(true);
    }

    public void EscInventory()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        inventoryFullImg.gameObject.SetActive(false);
        InventoryImg.gameObject.SetActive(false);
    }

    public void ConfigBtn() //설정보여주기
    {
        if (!config)
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

            inventoryFullImg.gameObject.SetActive(true);
            configPanel.SetActive(true);
            config = true;
        }
        else
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

            inventoryFullImg.gameObject.SetActive(false);
            configPanel.SetActive(false);
            config = false;
        }
    }

    public void ExitGame()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        Application.Quit();
    }

    public void LogOut()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        GPGSBinder.Inst.Logout();
        logOutQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
        GameManager.instance.loginSuccess = false;
        GameManager.instance.nextStage = false;
        SceneManager.LoadScene(0);
    }
}
