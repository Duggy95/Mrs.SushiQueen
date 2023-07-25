using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public Button FishingBtn;

    public bool isFishing = false;
    public bool useItem_white = false;  // 하얀 살 생선 확률 증가 아이템 사용
    public bool useItem_red = false;    // 붉은 살 생선 확률 증가 아이템 사용
    public bool useItem_rare = false;   // 레어 생선 확률 증가 아이템 사용
    bool config;

    FishData data;

    void Start()
    {
        //GameManager.instance.GetLog();
        InventoryImg.gameObject.SetActive(false);
        UIUpdate();
    }

    public void Fishing()
    {
        if (isFishing == false)
        {
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

        else
            return;
    }


    // 잡은 경우에 물고기 정보창 띄움
    public void Fish(FishData fishData)
    {
        data = fishData;
        fishInfo.gameObject.SetActive(true);
        fishInfo_Txt.text = fishData.info.text;
        fish_Img.sprite = fishData.fishImg;
    }

    // 팔기 버튼을 누르면 정보창 닫고 다시 낚시 준비
    public void Sell()
    {
        full_Txt.gameObject.SetActive(false);
        fishInfo.gameObject.SetActive(false);
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
        // 수족관에 이미지 추가
        Image[] _fishs = FishContent.gameObject.GetComponentsInChildren<Image>();

        bool isFull = true;
        bool isChange = false;

        for (int i = 0; i < _fishs.Length; i++)
        {
            if (_fishs[i].GetComponentInChildren<Text>().text.Contains(data.fishName))
            {
                string valueToFind = data.fishName;
                int newValue = 1;

                // 특정 값(valueToFind)을 만족하는 첫 번째 요소의 인덱스를 찾기
                int index = GameManager.instance.inventory_Fishs.FindIndex(fish => fish.fish_Name == valueToFind);

                if (index != -1)
                {
                    newValue += int.Parse(GameManager.instance.inventory_Fishs[index].fish_Count);

                    // 해당 인덱스(index)의 값 변경
                    GameManager.instance.inventory_Fishs[index].fish_Count = newValue.ToString();
                    _fishs[i].GetComponentInChildren<Text>().text = data.fishName + "   " + newValue.ToString() + "개";
                    Debug.Log("중복 종류 " + index + " changed to " + newValue);
                    GameManager.instance.Save("f");
                    isChange = true;
                    isFull = false;
                    break;
                }
            }
        }
        if (isChange == false)
        {
            for (int i = 0; i < _fishs.Length; i++)
            {
                FishSlot _slot = _fishs[i].GetComponent<FishSlot>();

                if (_slot.isEmpty == false)
                {
                    _fishs[i].sprite = data.fishImg;
                    _slot.fish_ColorNum = data.color;
                    _slot.fish_GradeNum = data.grade;
                    _slot.fish_Name = data.fishName;

                    GameManager.instance.inventory_Fishs.Add(new InventoryFish(data.fishName, "1"));
                    _fishs[i].GetComponentInChildren<Text>().text = data.fishName + "   " + "1개";
                    Debug.Log("안찼고 다른 종류");
                    GameManager.instance.Save("f");
                    _slot.isEmpty = true;
                    isFull = false;
                    break;
                }
            }
        }
        if (isFull)
        {
            full_Txt.gameObject.SetActive(true);
        }
        else
        {
            isFishing = false;
            fishInfo.gameObject.SetActive(false);
        }

        /* bool isChange = false;
         for (int j = 0; j < GameManager.instance.inventory_Fishs.Count; j++)
         {
             if (GameManager.instance.inventory_Fishs[j].fish_Name == data.fishName)
             {
                 int count = int.Parse(GameManager.instance.inventory_Fishs[j].fish_Count);
                 count++;
                 isChange = true;
                 GameManager.instance.inventory_Fishs[j].fish_Count = count.ToString();
                 _fishs[i].GetComponentInChildren<Text>().text = data.fishName + "   " + count.ToString() + "개";

                 break;
             }
         }

         if (isChange == false)
         {
             GameManager.instance.inventory_Fishs.Add(new InventoryFish(data.fishName, "1"));
             _fishs[i].GetComponentInChildren<Text>().text = data.fishName + "   " + "1개";
         }
         GameManager.instance.Save("f");
         _slot.isEmpty = true;
         isFull = false;
         break;*/
    }


    /*else
    {
        for (int x = 0; x<_fishs.Length; x++)
        {
            if (_fishs[x].GetComponentInChildren<Text>().text == data.fishName)
            {
                string valueToFind = data.fishName;
    int newValue = 1;

    // 특정 값(valueToFind)을 만족하는 첫 번째 요소의 인덱스를 찾기
    int index = GameManager.instance.inventory_Fishs.FindIndex(fish => fish.fish_Name == valueToFind);

                if (index != -1)
                {
                    newValue += int.Parse(GameManager.instance.inventory_Fishs[index].fish_Count);

    // 해당 인덱스(index)의 값 변경
    GameManager.instance.inventory_Fishs[index].fish_Count = newValue.ToString();
                    _fishs[x].GetComponentInChildren<Text>().text = data.fishName + "   " + newValue.ToString() + "개";
                    Debug.Log("다 찼고 같은 종류 " + index + " changed to " + newValue);
                    GameManager.instance.Save("f");
                    isFull = false;
                    break;
                }
            }


                            for (int j = 0; j < GameManager.instance.inventory_Fishs.Count; j++)
    {
    if (GameManager.instance.inventory_Fishs[j].fish_Name == data.fishName)
    {
    int count = int.Parse(GameManager.instance.inventory_Fishs[j].fish_Count);
    count++;
    GameManager.instance.inventory_Fishs[j].fish_Count = count.ToString();
    _fishs[i].GetComponentInChildren<Text>().text = data.fishName + "   " + count.ToString() + "개";

    break;
    }
    }
        }
    }
    }*/

    // 수족관이 가득 찬 경우 텍스트 띄움
    /*        if (isFull)
            {
                full_Txt.gameObject.SetActive(true);
            }
            // 아닌 경우 정보 패널 비활성화
            else
    {
        isFishing = false;
        fishInfo.gameObject.SetActive(false);
    }
        }*/

    public void Run()
    {
        StartCoroutine(FishRun());
    }

    // 물고기가 도망간 경우 도망 텍스트 띄우고 다시 낚시 준비
    IEnumerator FishRun()
    {
        fishRun.gameObject.SetActive(true);
        isFishing = false;
        // 화면 누르면 텍스트 비활성화
        yield return new WaitForSeconds(2f);
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

    /*public void LogOut()
    {
        GPGSBinder.Inst.Logout();
    }*/
}
