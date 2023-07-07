using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class FishingManager : MonoBehaviour, IPointerClickHandler
{
    public GameObject hpBarPrefab;
    public Text dateTxt;
    public Text goldTxt;
    public GameObject InventoryImg;

    void Start()
    {
        UIUpdate();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 clickPos = eventData.position;
        print(clickPos);
        //var fishHpBar = Instantiate(hpBarPrefab, clickPos, new Quaternion(0, 0, 0, 0));
    }

    void Start()
    {
        
    }

    void UIUpdate()
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

    public void GoCook()
    {
        SceneManager.LoadScene(2);
    }
}
