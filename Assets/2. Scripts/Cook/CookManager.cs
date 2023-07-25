using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CookManager : MonoBehaviour
{
    public GameObject customerPrefab;  //손님 프리팹
    public GameObject orderView;  //주문화면
    public GameObject cookView;  //요리화면
    public GameObject configPanel;
    public Text dateTxt;  //날짜 + 평판
    public Text goldTxt;  //골드
    public GameObject InventoryImg;  //인벤토리
    public Image[] fishImg;  //생선이미지
    public bool canMake = false;
    bool config = false;

    WaitForSeconds ws;

    Vector2 customerTr = Vector2.zero;
    int count = 0;

    void Start()
    {
        //시작 세팅 = 주문화면 보이게
        orderView.SetActive(true);
        cookView.SetActive(false);
        ws = new WaitForSeconds(2f);
        UIUpdate();

        StartCoroutine(Create());
    }

    void Update()
    {
        UIUpdate();
    }

    public void GoEndScene()  //운영씬으로
    {
        SceneManager.LoadScene(3);
    }

    public void UIUpdate()
    {
        dateTxt.text = GameManager.instance.data.dateCount + "일차 / 평판 : " + GameManager.instance.data.score;
        goldTxt.text = "gold : " + GameManager.instance.data.gold;
    }

    public void ViewInventory() //인벤토리 활성화
    {
        InventoryImg.gameObject.SetActive(true);
    }

    public void EscInventory() //인벤토리 나가기
    {
        InventoryImg.gameObject.SetActive(false);
    }

    public void ConfigBtn() //설정보여주기
    {
        if(!config)
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

    public void ViewOrder()  //주문화면 요리화면 전환 메서드
    {
        if(canMake)  //만들 수 있을 때
        {
            count++;
            if (count % 2 == 0)
            {
                cookView.SetActive(false);
                count = 0;
            }
            else
            {
                cookView.SetActive(true);
            }
        }
    }

    public IEnumerator Create()
    {
        //2초 후 손님 생성.
        yield return ws;
        GameObject customer = Instantiate(customerPrefab, customerTr,
                                                                Quaternion.identity, orderView.transform);
        customer.transform.localPosition = new Vector2(-400, -100);  //손님 위치
        customer.transform.SetSiblingIndex(1);  //2번째 자식.
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
