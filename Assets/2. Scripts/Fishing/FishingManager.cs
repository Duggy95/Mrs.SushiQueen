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
    public Text atkTxt;
    public Text fishInfo_Txt;
    public Text full_Txt;
    public Text fishRun;
    public Text useWhiteItemTxt;
    public Text useRedItemTxt;
    public Text useRareItemTxt;
    public Text scoreTxt;
    public GameObject configPanel;
    public GameObject inventoryImg;  // 인벤토리 이미지
    public GameObject inventoryBtn;
    public GameObject fishInfoImg;
    public GameObject fishContent; // 수족관
    public GameObject fishInfo; // 생선 정보 판넬
    public GameObject fishingRod; // 낚싯대 이미지
    public GameObject lineStartPos;
    public GameObject useItemPanel;
    public GameObject fishObj;
    public Button fishingBtn;
    public Image fish_Img;
    public bool isFishing = false;
    public bool useItem_white = false;  // 하얀 살 생선 확률 증가 아이템 사용
    public bool useItem_red = false;    // 붉은 살 생선 확률 증가 아이템 사용
    public bool useItem_rare = false;   // 레어 생선 확률 증가 아이템 사용

    Vector3 fishInfoOriginPos;
    Vector3 fishInfoOriginScale;
    bool config;
    FishData data;

    void Start()
    {
        fishInfoOriginScale = Vector3.one;
        fishInfoOriginPos = fishInfoImg.transform.position;
        useItemPanel.gameObject.SetActive(false);
        useRareItemTxt.gameObject.SetActive(false);
        useRedItemTxt.gameObject.SetActive(false);
        useWhiteItemTxt.gameObject.SetActive(false);
        //GameManager.instance.GetLog();
        inventoryImg.gameObject.SetActive(false);
        UIUpdate();
    }

    public void Delete()
    {
        GameManager.instance.DeleteData();
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
            LineRenderer fishLine = fishingRod.GetComponent<LineRenderer>();

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
        fishInfoImg.gameObject.SetActive(true);
        fishInfoImg.transform.parent = fishInfo.transform;
        fishInfoImg.transform.position = fishInfoOriginPos;
        fishInfoImg.transform.localScale = fishInfoOriginScale;
        fishInfoImg.transform.SetSiblingIndex(0);
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

        GameManager.instance.todayData.gold += data.gold;
        //GameManager.instance.Save("d");
        Debug.Log("골드 " + _gold);
        // 골드 ++
        UIUpdate();
        isFishing = false;
    }

    // 수족관으로 버튼 누르면 정보창 닫고 다시 낚시 준비
    public void Get()
    {
        // 수족관에 이미지 추가
        Image[] _fishs = fishContent.gameObject.GetComponentsInChildren<Image>();

        bool isFull = true;
        bool isChange = false;

        for (int i = 0; i < _fishs.Length; i++)
        {
            if (_fishs[i].gameObject.name.Contains("Slot"))
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

                        GameManager.instance.todayFishInfos.Add(new TodayFishInfo(data.fishName, 1));

                        // 해당 인덱스(index)의 값 변경
                        GameManager.instance.inventory_Fishs[index].fish_Count = newValue.ToString();
                        _fishs[i].GetComponentInChildren<Text>().text = data.fishName + "   " + newValue.ToString() + " " + "마리";
                        Debug.Log("중복 종류 " + index + " changed to " + newValue);
                        fishInfoImg.transform.parent = inventoryBtn.transform;
                        StartCoroutine(Eff());
                        StartCoroutine(EffMove());
                        //GameManager.instance.Save("f");
                        isChange = true;
                        isFull = false;
                        break;
                    }
                }
            }
        }

        if (isChange == false)
        {
            for (int i = 0; i < _fishs.Length; i++)
            {
                if (_fishs[i].gameObject.name.Contains("Slot"))
                {
                    FishSlot _slot = _fishs[i].GetComponent<FishSlot>();
                    Image Img = null;
                    Image[] fishImgs = _fishs[i].GetComponentsInChildren<Image>();
                    foreach (Image fishImg in fishImgs)
                    {
                        if (fishImg.name.Contains("Img"))
                            Img = fishImg;
                    }

                    if (_slot.isEmpty == false)
                    {
                        Img.sprite = data.fishImg;
                        _slot.fish_ColorNum = data.color;
                        _slot.fish_GradeNum = data.grade;
                        _slot.fish_Name = data.fishName;

                        GameManager.instance.inventory_Fishs.Add(new InventoryFish(data.fishName, "1"));

                        GameManager.instance.todayFishInfos.Add(new TodayFishInfo(data.fishName, 1));

                        _fishs[i].GetComponentInChildren<Text>().text = data.fishName + "   " + "1 마리";
                        Debug.Log("안찼고 다른 종류");
                        //GameManager.instance.Save("f");
                        fishInfoImg.transform.parent = inventoryBtn.transform;
                        StartCoroutine(Eff());
                        StartCoroutine(EffMove());
                        _slot.isEmpty = true;
                        isFull = false;
                        break;
                    }
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
    }

    IEnumerator Eff()
    {
        Vector3 initialScale = new Vector3(1.1f, 1.1f, 1.1f);
        Vector3 targetScale = new Vector3(0.3f, 0.3f, 0.3f);
        float duration = 1f; // 크기 변화에 걸리는 시간

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // 시간 비율 계산
            fishInfoImg.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }
    }

    IEnumerator EffMove()
    {
        float elapsedTime = 0f;
        float duration = 1f; // 이동 시간 (초)

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            fishInfoImg.transform.position = Vector3.Lerp(fishInfoImg.transform.position, inventoryBtn.transform.position, elapsedTime / duration);

            if(elapsedTime / duration >= 1)
            {
                fishInfoImg.transform.position = inventoryBtn.transform.position;
                fishInfoImg.gameObject.SetActive(false);
            }
            yield return null;
        }
    }

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

    public void UIUpdate()
    {
        dateTxt.text = GameManager.instance.data.dateCount + "일차";
        scoreTxt.text = "평판 : " + GameManager.instance.data.score;
        goldTxt.text = "gold : " + GameManager.instance.data.gold;
        atkTxt.text = "공격력 : " + GameManager.instance.data.atk;
    }

    public void ViewInventory()
    {
        // 인벤토리 활성화
        inventoryImg.gameObject.SetActive(true);
        // 인벤토리 순서를 제일 마지막으로
        inventoryImg.transform.SetAsLastSibling();
    }

    public void EscInventory()
    {
        inventoryImg.gameObject.SetActive(false);
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
