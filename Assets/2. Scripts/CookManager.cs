using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class CookManager : MonoBehaviour
{
    public static CookManager instance;
    public GameObject customerPrefab;  //손님 프리팹
    public GameObject orderView;  //주문화면
    public GameObject cookView;  //요리화면
    public GameObject timer;  //손님 타이머
    public GameObject yesBtn;  //수락 버튼
    public GameObject noBtn;  //거절 버튼
    public Text dateTxt;  //날짜 + 평판
    public Text goldTxt;  //골드
    public Text orderTxt;  //주문 텍스트
    public GameObject InventoryImg;  //인벤토리
    public RawImage[] fishImg;  //생선이미지
    public bool isCustomer = false;

    Vector2 customerTr = Vector2.zero;
    int count = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
    }

    void Start()
    {
        //시작 세팅 = 주문화면 보이게
        orderView.SetActive(true);
        cookView.SetActive(false);
        UIUpdate();

        Create();
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

    public void ViewInventory() //인벤토리 활성화
    {
        InventoryImg.gameObject.SetActive(true);
    }

    public void EscInventory() //인벤토리 나가기
    {
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
            for (int i = 0; i < fishImg.Length; i++)
            {
                fishImg[i].AddComponent<DragItem>();
            }
        }
    }

    public void ShowTimer()  //손님 타이머 활성화
    {
        timer.SetActive(true);
        yesBtn.SetActive(false);
        noBtn.SetActive(false);
    }

    public void NoBtn() //거절 버튼.
    {
        orderTxt.text = "님 실망임.";
        yesBtn.SetActive(false);
        noBtn.SetActive(false);
        print("님 평판 깎임");
        isCustomer = false;
    }

    public void Create()
    {
        GameObject customer = Instantiate(customerPrefab, customerTr,
                                                                Quaternion.identity, GameObject.Find("OrderCanvas").transform);
        customer.transform.localPosition = new Vector2(-400, -100);
        customer.transform.SetSiblingIndex(1);
    }
}
