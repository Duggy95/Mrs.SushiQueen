using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutorialFishing : TutorialBase
{
    public GameObject fishObj;
    public GameObject fishCanvas;
    public GameObject fishContent;
    public Text full_Txt;
    public Text fishRun;
    public Text touchTxt;
    public GameObject fishInfo;
    public Text fishInfo_Txt;
    public Image fish_Img;
    FishData data;

    public int count = 0;
    bool isFishing;
    public bool fishCome;
    public bool fishFull;

    public override void Enter()
    {
        print("fishing tutorial");
    }

    public override void Execute(TutorialManager tutorialManager)
    {
        if(fishCome)
        {
            tutorialManager.SetNextTutorial();
        }
    }

    public override void Exit()
    {

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
            //LineRenderer fishLine = FishingRod.GetComponent<LineRenderer>();

            //Vector3 startPos = lineStartPos.transform.position; // 시작 지점
            //Vector3 endPos = Input.mousePosition; // 끝 지점

            // Line Renderer 속성 설정
            //fishLine.SetPosition(0, startPos); // 라인의 점들 설정
            //fishLine.SetPosition(1, endPos);
            Instantiate(fishObj, Input.mousePosition, Quaternion.identity);
            //transform.position = Input.mousePosition;
        }
    }

    public void Fish(FishData fishData)
    {
        Debug.Log("낚시 성공");
        Debug.Log(fishData);
        data = fishData;
        fishInfo.gameObject.SetActive(true);
        fishInfo_Txt.text = fishData.info.text;
        fish_Img.sprite = fishData.fishImg;
    }

    /*public void Run()
    {
        StartCoroutine(FishRun());
    }*/

    /*IEnumerator FishRun()
    {
        Debug.Log("낚시 실패");

        fishRun.gameObject.SetActive(true);
        isFishing = false;
        // 화면 누르면 텍스트 비활성화
        yield return new WaitForSeconds(2f);
        Debug.Log("다시");
        fishRun.gameObject.SetActive(false);
    }*/

    public void Get()
    {
        // 수족관에 이미지 추가
        Image[] _fishs = fishContent.gameObject.GetComponentsInChildren<Image>();

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
                    //GameManager.instance.inventory_Fishs[index].fish_Count = newValue.ToString();
                    _fishs[i].GetComponentInChildren<Text>().text = data.fishName + "   " + newValue.ToString() + "개";
                    Debug.Log("중복 종류 " + index + " changed to " + newValue);
                    //GameManager.instance.Save("f");
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

                    //GameManager.instance.inventory_Fishs.Add(new InventoryFish(data.fishName, "1"));
                    _fishs[i].GetComponentInChildren<Text>().text = data.fishName + "   " + "1개";
                    Debug.Log("안찼고 다른 종류");
                    //GameManager.instance.Save("f");
                    _slot.isEmpty = true;
                    isFull = false;
                    break;
                }
            }
        }
        if (isFull)
        {
            full_Txt.gameObject.SetActive(true);
            fishFull = true;
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

    public void Sell()
    {
        full_Txt.gameObject.SetActive(false);
        fishInfo.gameObject.SetActive(false);
        //int _gold = int.Parse(GameManager.instance.data.gold) + data.gold;
        //GameManager.instance.data.gold = _gold.ToString();
        //GameManager.instance.Save("s");
        //Debug.Log("골드 " + _gold);
        // 골드 ++
        //UIUpdate();
        isFishing = false;
    }
}
