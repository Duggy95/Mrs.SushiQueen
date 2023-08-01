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
    public GameObject fishingSV;
    public GameObject ShopSV;
    public GameObject SkillSV;
    public GameObject InventoryImg;
    public GameObject inventoryFullImg;
    public GameObject noMoneyTxt;
    public GameObject maxLevelTxt;
    public GameObject fullTxt;
    public GameObject receipt;
    public GameObject endSceneQuestion;
    public GameObject logOutQuestion;
    public GameObject deleteDataQuestion;
    public GameObject exitGameQuestion;
    public Image fishBtn;
    public Image shopBtn;
    public Image skillBtn;
    Color initColor = new Color(1, 1, 1, 1);
    Color selColor = new Color(1, 1, 0, 1);

    bool config;

    private void Awake()
    {
        fishBtn.color = initColor;
        shopBtn.color = initColor;
        skillBtn.color = initColor;
        receipt.gameObject.SetActive(false);
        fishingSV.gameObject.SetActive(true);
        ShopSV.gameObject.SetActive(false);
        SkillSV.gameObject.SetActive(false);
        maxLevelTxt.gameObject.SetActive(false);
        noMoneyTxt.gameObject.SetActive(false);
        fullTxt.gameObject.SetActive(false);
    }

    public void OnclickSkill()
    {
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
        GameManager.instance.nextStage = true;
        int _date = int.Parse(GameManager.instance.data.dateCount) + 1;
        GameManager.instance.data.dateCount = _date.ToString();
        SceneManager.LoadScene(0);
    }


    public void Delete()
    {
        GameManager.instance.DeleteData();
        UIUpdate();
    }


    private void Start()
    {
        UIUpdate();
    }

    /*public void UIUpdate()
    {
        dateTxt.text = GameManager.instance.data.dateCount + "일차";
        scoreTxt.text = "평판 : " + GameManager.instance.data.score;
        goldTxt.text = "gold : " + GameManager.instance.data.gold;
        atkTxt.text = "공격력 : " + GameManager.instance.data.atk;
    }*/

    public void UIUpdate()
    {
        dateTxt.text = int.Parse(GameManager.instance.data.dateCount).ToString("N0");
        scoreTxt.text = int.Parse(GameManager.instance.data.score).ToString("N0");
        goldTxt.text = int.Parse(GameManager.instance.data.gold).ToString("N0");
        atkTxt.text = GameManager.instance.data.atk;
    }

    public void EndSceneQuestionEsc()
    {
        inventoryFullImg.gameObject.SetActive(false);
        endSceneQuestion.gameObject.SetActive(false);
    }

    public void EndSceneQuestion()
    {
        inventoryFullImg.gameObject.SetActive(true);
        endSceneQuestion.gameObject.SetActive(true);
    }

    public void ExitGameQuestionEsc()
    {
        inventoryFullImg.gameObject.SetActive(false);
        exitGameQuestion.gameObject.SetActive(false);
    }

    public void ExitGameQuestion()
    {
        inventoryFullImg.gameObject.SetActive(true);
        exitGameQuestion.gameObject.SetActive(true);
    }

    public void DeleteDataQuestionEsc()
    {
        inventoryFullImg.gameObject.SetActive(false);
        deleteDataQuestion.gameObject.SetActive(false);
    }

    public void DeleteDataQuestion()
    {
        inventoryFullImg.gameObject.SetActive(true);
        deleteDataQuestion.gameObject.SetActive(true);
    }

    public void LogOutQuestionEsc()
    {
        inventoryFullImg.gameObject.SetActive(false);
        endSceneQuestion.gameObject.SetActive(false);
    }

    public void LogOutQuestion()
    {
        inventoryFullImg.gameObject.SetActive(true);
        logOutQuestion.gameObject.SetActive(true);
    }

    public void ViewReceipt()
    {
        inventoryFullImg.gameObject.SetActive(true);
        receipt.gameObject.SetActive(true);
    }

    public void EscReceipt()
    {
        inventoryFullImg.gameObject.SetActive(false);
        receipt.gameObject.SetActive(false);
    }

    public void ViewInventory()
    {
        // 인벤토리 활성화
        inventoryFullImg.gameObject.SetActive(true);
        InventoryImg.gameObject.SetActive(true);
    }

    public void EscInventory()
    {
        inventoryFullImg.gameObject.SetActive(false);
        InventoryImg.gameObject.SetActive(false);
    }

    public void ConfigBtn() //설정보여주기
    {
        if (!config)
        {
            inventoryFullImg.gameObject.SetActive(true);
            configPanel.SetActive(true);
            config = true;
        }
        else
        {
            inventoryFullImg.gameObject.SetActive(false);
            configPanel.SetActive(false);
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
    }*/
}
