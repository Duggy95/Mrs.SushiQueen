using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.FullSerializer;

public class FishingManager : MonoBehaviour
{
    public Canvas canvas;
    public Text dateTxt;
    public Text goldTxt;
    public GameObject configPanel;
    public GameObject InventoryImg;  // 인벤토리 이미지
    public GameObject FishContent; // 수족관
    public GameObject fishInfo; // 생선 정보 판넬
    public GameObject FishingRod; // 낚싯대 이미지
    public GameObject lineStartPos;
    public Image fish_Img;
    public Text fishInfo_Txt;
    public Text full_Txt;
    public GameObject fishObj;
    public Text fishRun;

    public bool isFishing = false;
    public bool useItem_white = false;  // 하얀 살 생선 확률 증가 아이템 사용
    public bool useItem_red = false;    // 붉은 살 생선 확률 증가 아이템 사용
    public bool useItem_rare = false;   // 레어 생선 확률 증가 아이템 사용
    bool config;

    FishData data;

    void Start()
    {
        InventoryImg.gameObject.SetActive(false);
        UIUpdate();
    }

    public void Fishing()
    {
        if (isFishing == false)
        {
            Debug.Log("낚시 시작");
            Debug.Log("위치" + Input.mousePosition);

            // 물고기 잡는 중으로 변경
            isFishing = true;
            // 물고기 도망 텍스트 비활성화
            fishRun.gameObject.SetActive(false);
            LineRenderer fishLine = FishingRod.GetComponent<LineRenderer>();

            Vector3 startPos = lineStartPos.transform.position; // 시작 지점
            Vector3 endPos = Input.mousePosition; // 끝 지점

            // Line Renderer 속성 설정
            fishLine.SetPosition(0, startPos); // 라인의 점들 설정
            fishLine.SetPosition(1, endPos);
            Instantiate(fishObj, Input.mousePosition, Quaternion.identity);
            //transform.position = Input.mousePosition;
        }
    }


    // 잡은 경우에 물고기 정보창 띄움
    public void Fish(FishData fishData)
    {
        Debug.Log("낚시 성공");
        Debug.Log(fishData);
        data = fishData;
        fishInfo.gameObject.SetActive(true);
        fishInfo_Txt.text = fishData.info.text;
        fish_Img.sprite = fishData.fishImg;
    }

    // 팔기 버튼을 누르면 정보창 닫고 다시 낚시 준비
    public void Sell()
    {
        Debug.Log("판매");

        full_Txt.gameObject.SetActive(false);
        fishInfo.gameObject.SetActive(false);
        //GameManager.instance.gold += data.gold;
        //GameManager.instance.SetGold();
        int _gold = int.Parse(GameManager.instance.data.gold) + data.gold;
        GameManager.instance.data.gold = _gold.ToString();
        GameManager.instance.Save("s");
        Debug.Log("골드 " + _gold);
        // 골드 ++
        UIUpdate();
        isFishing = false;
    }

    // 수족관으로 버튼 누르면 정보창 닫고 다시 낚시 준비
    public void Get()
    {
        Debug.Log("수족관으로");

        // 수족관에 이미지 추가
        Image[] _fishs = FishContent.gameObject.GetComponentsInChildren<Image>();
        Debug.Log("수족관 칸 수 : " + _fishs.Length);

        bool isFull = false;
        // 수족관의 스롯을 검사하여 비었으면 해당 정보 전달
        for (int i = 0; i < _fishs.Length; i++)
        {
            FishSlot _slot = _fishs[i].GetComponent<FishSlot>();
            if (_slot.isEmpty == false)
            {
                _fishs[i].sprite = data.fishImg;
                _slot.fish_ColorNum = data.color;
                _slot.fish_GradeNum = data.grade;
                _slot.fish_Name = data.fishName;

                InventoryFish _inventoryFish = new InventoryFish();
                _inventoryFish.fish_Name = data.fishName;
                GameManager.instance.inventory_Fishs.Add(_inventoryFish);

                //GameManager.instance.fishs = data.fishName;
                GameManager.instance.Save("f");
                _fishs[i].GetComponentInChildren<Text>().text = data.fishName;
                _slot.isEmpty = true;
                isFull = true;
                break;
            }
        }
        // 수족관이 가득 찬 경우 텍스트 띄움
        if (!isFull)
        {
            full_Txt.gameObject.SetActive(true);
        }
        // 아닌 경우 정보 패널 비활성화
        else
        {
            isFishing = false;
            fishInfo.gameObject.SetActive(false);
        }
    }

    public void Run()
    {
        StartCoroutine(FishRun());
    }

    // 물고기가 도망간 경우 도망 텍스트 띄우고 다시 낚시 준비
    IEnumerator FishRun()
    {
        Debug.Log("낚시 실패");

        fishRun.gameObject.SetActive(true);
        isFishing = false;
        // 화면 누르면 텍스트 비활성화
        yield return new WaitForSeconds(2f);
        Debug.Log("다시");
        fishRun.gameObject.SetActive(false);
    }

    void UIUpdate()
    {
        dateTxt.text = GameManager.instance.data.dateCount + "일차 / 평판 : " + GameManager.instance.data.score;
        goldTxt.text = "gold : " + GameManager.instance.data.gold;
    }

    public void ViewInventory()
    {
        // 인벤토리 활성화
        InventoryImg.gameObject.SetActive(true);
        // 인벤토리 순서를 제일 마지막으로
        InventoryImg.transform.SetAsLastSibling();
    }

    public void EscInventory()
    {
        InventoryImg.gameObject.SetActive(false);
    }

    public void ConfigBtn()
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

    public void GoCook()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
