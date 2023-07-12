using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class FishingManager : MonoBehaviour //, IPointerClickHandler
{
    public Canvas canvas;
    public Text dateTxt;
    public Text goldTxt;
    public GameObject InventoryImg;  // 인벤토리 이미지
    public GameObject fishInfo; // 생선 정보 판넬
    public GameObject fishObj;
    public Text fishRun;
    public Sprite[] fishImg;  // 생선 이미지 배열
    public bool isFishing = false;

    void Start()
    {
        UIUpdate();
    }

    /*public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 clickPos = eventData.position;
        print(clickPos);
        //var fishHpBar = Instantiate(hpBarPrefab, clickPos, new Quaternion(0, 0, 0, 0));
    }*/

    void Update()
    {
        // 터치를 하고 물고기 잡는 중이 아니라면
        if (Input.GetMouseButtonDown(0) && isFishing == false)
        {
            Debug.Log("낚시 시작");
            Debug.Log("위치" + Input.mousePosition);

            // 물고기 잡는 중으로 변경
            isFishing = true;
            // 물고기 도망 텍스트 비활성화
            fishRun.gameObject.SetActive(false);

            Instantiate(fishObj, Input.mousePosition, Quaternion.identity);
        }
    }


    // 잡은 경우에 물고기 정보창 띄움
    public void Fish()
    {
        Debug.Log("낚시 성공");

        fishInfo.gameObject.SetActive(true);
    }

    // 팔기 버튼을 누르면 정보창 닫고 다시 낚시 준비
    public void Sell()
    {
        Debug.Log("판매");

        fishInfo.gameObject.SetActive(false);
        // 골드 ++
        // UIUpdate();
        isFishing = false;
    }

    // 수족관으로 버튼 누르면 정보창 닫고 다시 낚시 준비
    public void Get()
    {
        Debug.Log("수족관으로");

        fishInfo.gameObject.SetActive(false);
        // 수족관에 이미지 추가
        isFishing = false;
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
        yield return Input.GetMouseButtonDown(0);
        fishRun.gameObject.SetActive(false);
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
