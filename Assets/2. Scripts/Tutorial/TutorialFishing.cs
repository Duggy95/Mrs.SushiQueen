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

    bool isFishing;
    public bool fishCome;

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
        Debug.Log("수족관으로");

        // 수족관에 이미지 추가
        Image[] _fishs = fishContent.gameObject.GetComponentsInChildren<Image>();
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
                //GameManager.instance.inventory_Fishs.Add(_inventoryFish);

                //GameManager.instance.fishs = data.fishName;
                //GameManager.instance.Save("f");
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
}
