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
    public GameObject noMoneyTxt;
    public GameObject maxLevelTxt;
    public GameObject fullTxt;
    public GameObject receipt;
    bool config;

    private void Awake()
    {
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
        fishingSV.gameObject.SetActive(false);
        ShopSV.gameObject.SetActive(false);
        SkillSV.gameObject.SetActive(true);
        maxLevelTxt.gameObject.SetActive(false);
        noMoneyTxt.gameObject.SetActive(false);
        fullTxt.gameObject.SetActive(false);
    }

    public void OnclickShop()
    {
        fishingSV.gameObject.SetActive(false);
        ShopSV.gameObject.SetActive(true);
        SkillSV.gameObject.SetActive(false);
        maxLevelTxt.gameObject.SetActive(false);
        noMoneyTxt.gameObject.SetActive(false);
        fullTxt.gameObject.SetActive(false);
    }

    public void OnclickFishing()
    {
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

    public void UIUpdate()
    {
        dateTxt.text = GameManager.instance.data.dateCount + "일차";
        scoreTxt.text = "평판 : " + GameManager.instance.data.score;
        goldTxt.text = "gold : " + GameManager.instance.data.gold;
        atkTxt.text = "공격력 : " + GameManager.instance.data.atk;
    }

    public void ViewReceipt()
    {
        receipt.gameObject.SetActive(true);
    }

    public void EscReceipt()
    {
        receipt.gameObject.SetActive(false);
    }

    public void ViewInventory()
    {
        // 인벤토리 활성화
        InventoryImg.gameObject.SetActive(true);
    }

    public void EscInventory()
    {
        InventoryImg.gameObject.SetActive(false);
    }

    public void ConfigBtn() //설정보여주기
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

    /*public void LogOut()
    {
        GPGSBinder.Inst.Logout();
    }*/
}
