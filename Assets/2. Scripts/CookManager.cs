using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class CookManager : MonoBehaviour
{
    public GameObject orderView;
    public GameObject cookView;
    public Text dateTxt;
    public Text goldTxt;
    public GameObject InventoryImg;
    public RawImage[] fishImg;

    int count = 0;

    void Start()
    {
        //시작 세팅 = 주문화면 보이게
        orderView.SetActive(true);
        cookView.SetActive(false);
        UIUpdate();
    }

    void Update()
    {
    }

    public void GoEndScene()  //운영씬으로
    {
        SceneManager.LoadScene(3);
    }

    public void UIUpdate()  //텍스트 최신화
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
        //인벤토리 나가기
        InventoryImg.gameObject.SetActive(false);
    }

    public void GoHome() //홈으로
    {
        SceneManager.LoadScene(0);
    }

    public void ViewOrder()  //주문화면 요리화면 전환 메서드
    {
        count++;
        if (count % 2 == 0)
        {
            orderView.SetActive(true);
            cookView.SetActive(false);
            count = 0;

            for (int i = 0; i < fishImg.Length; i++)
            {
                Destroy(fishImg[i].GetComponent<DragItem>());
            }
        }
        else
        {
            cookView.SetActive(true);
            orderView.SetActive(false);

            for (int i = 0; i < fishImg.Length; i++)
            {
                fishImg[i].AddComponent<DragItem>();
            }
        }
    }
}
