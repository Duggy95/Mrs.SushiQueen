using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSceneCtrl : MonoBehaviour
{
    public GameObject fishingSV;
    public GameObject ShopSV;
    public GameObject SkillSV;
    public Text dateTxt;
    public Text goldTxt;
    public GameObject InventoryImg;
    public GameObject noMoneyTxt;

    private void Awake()
    {
        fishingSV.gameObject.SetActive(true);
        ShopSV.gameObject.SetActive(false);
        SkillSV.gameObject.SetActive(false);
        noMoneyTxt.gameObject.SetActive(false);
    }

    public void OnclickSkill()
    {
        fishingSV.gameObject.SetActive(false);
        ShopSV.gameObject.SetActive(false);
        SkillSV.gameObject.SetActive(true);
    }

    public void OnclickShop()
    {
        fishingSV.gameObject.SetActive(false);
        ShopSV.gameObject.SetActive(true);
        SkillSV.gameObject.SetActive(false);
    }

    public void OnclickFishing()
    {
        fishingSV.gameObject.SetActive(true);
        ShopSV.gameObject.SetActive(false);
        SkillSV.gameObject.SetActive(false);
    }

    public void NextStage()
    {
        GameManager.instance.nextStage = true;
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        UIUpdate();
    }

    public void UIUpdate()
    {
        dateTxt.text = GameManager.instance.dateCount + "일차 / 평판 : " + GameManager.instance.score;
        goldTxt.text = "gold : " + GameManager.instance.gold;
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

    public void GoHome()
    {
        SceneManager.LoadScene(0);
    }
}
