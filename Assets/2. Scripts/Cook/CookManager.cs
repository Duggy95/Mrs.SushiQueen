using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CookManager : MonoBehaviour
{
    public CanvasGroup inventoryCanvas;
    public GameObject customerPrefab;  //손님 프리팹
    public GameObject orderView;  //주문화면
    public GameObject cookView;  //요리화면
    public GameObject configPanel;
    public GameObject readyBtn;
    public GameObject InventoryImg;  //인벤토리
    public GameObject inventoryFullImg;
    public GameObject fishIconPrefab;
    public GameObject[] customers;
    public GameObject orderFishContent;
    public GameObject endSceneQuestion;
    public GameObject logOutQuestion;
    public GameObject deleteDataQuestion;
    public GameObject exitGameQuestion;
    public Text dateTxt;  //날짜 + 평판
    public Text goldTxt;  //골드
    public Text atkTxt;  //골드
    public Text orderTxt;
    public Text scoreTxt;
    public List<string> fishList;
    public int fishBtnCount = 0;
    //public Image[] fishImg;  //생선이미지
    public bool canMake = false;
    public bool isReady = false;
    bool config = false;
    public Vector2 customerStartPos;

    //Transform fishContent;

    WaitForSeconds ws;
    Vector2 customerTr = Vector2.zero;
    int count = 0;

    void Start()
    {
        customerStartPos = new Vector2(-450, -100);  //손님 위치

        //시작 세팅 화면 세팅
        orderView.SetActive(true);
        cookView.SetActive(true);
        fishList = new List<string>();

        /*if(int.Parse(GameManager.instance.data.score) <= 600)
        {
            ws = new WaitForSeconds(3f);
        }
        else if(int.Parse(GameManager.instance.data.score) <= 900)
        {
            ws = new WaitForSeconds(2.5f);
        }
        else
        {
            ws = new WaitForSeconds(2f);
        }*/
        ws = new WaitForSeconds(2);

        UIUpdate();
    }

    void Update()
    {
        UIUpdate();
    }

    public void GoEndScene()  //운영씬으로
    {
        SceneManager.LoadScene(3);
    }

    public void Delete()
    {
        GameManager.instance.DeleteData();
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
        dateTxt.text = GameManager.instance.data.dateCount;
        scoreTxt.text = GameManager.instance.data.score;
        goldTxt.text = GameManager.instance.data.gold;
        atkTxt.text = GameManager.instance.data.atk;
    }

    public void ViewInventory() //인벤토리 활성화
    {
        inventoryFullImg.gameObject.SetActive(true);
        InventoryImg.gameObject.SetActive(true);
    }

    public void EscInventory() //인벤토리 나가기
    {
        inventoryFullImg.gameObject.SetActive(false);
        logOutQuestion.gameObject.SetActive(false);
    }

    public void EndSceneQuestionEsc()
    {
        endSceneQuestion.gameObject.SetActive(false);
    }

    public void EndSceneQuestion()
    {
        endSceneQuestion.gameObject.SetActive(true);
    }

    public void ExitGameQuestionEsc()
    {
        exitGameQuestion.gameObject.SetActive(false);
    }

    public void ExitGameQuestion()
    {
        exitGameQuestion.gameObject.SetActive(true);
    }

    public void DeleteDataQuestionEsc()
    {
        deleteDataQuestion.gameObject.SetActive(false);
    }

    public void DeleteDataQuestion()
    {
        deleteDataQuestion.gameObject.SetActive(true);
    }

    public void LogOutQuestionEsc()
    {
        endSceneQuestion.gameObject.SetActive(false);
    }

    public void LogOutQuestion()
    {
        logOutQuestion.gameObject.SetActive(true);
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
        int random = Random.Range(0, customers.Length);
        //2초 후 손님 생성.
        yield return ws;
        GameObject customer = Instantiate(customers[random], customerTr,
                                                                Quaternion.identity, orderView.transform);
        customer.transform.SetSiblingIndex(1);  //2번째 자식.
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReadyBtn()
    {
        isReady = true;
        StartCoroutine(Create());
        readyBtn.SetActive(false);
        orderView.SetActive(true);
        cookView.SetActive(false);
        InventoryImg.gameObject.SetActive(false);
        inventoryCanvas.interactable = false;

        /*for(int i = 0; i < fishBtnCount; i++)
        {
            GameObject fishIcon = Instantiate(fishIconPrefab, orderFishContent.transform);
            fishIcon.GetComponent<Image>.sprite = 
        }*/
    }

    public void Order(string txt)
    {
        orderTxt.text = txt;
    }

    /*public void LogOut()
    {
        GPGSBinder.Inst.Logout();
    }*/
}
