using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class StartSceneCtrl : MonoBehaviour
{
    public Camera mainCam;  //타이틀화면
    public Camera storyCam;  //스토리화면
    public Camera modeCam;  //선택화면
    public Text StartTxt;  //터치해서 게임시작 텍스트
    public Text[] storytxt;  //스토리 텍스트
    public Text dateTxt;  //날짜 + 평판 텍스트
    public Text goldTxt;  //골드 텍스트
    public GameObject InventoryImg; //인벤토리 이미지

    int count = 0;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            if (!GameManager.instance.nextStage) //
            {
                mainCam.gameObject.SetActive(true);
                storyCam.gameObject.SetActive(false);
                modeCam.gameObject.SetActive(false);
            }

            else //
            {
                mainCam.gameObject.SetActive(false);
                storyCam.gameObject.SetActive(false);
                modeCam.gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (count == 0 && Input.GetMouseButtonDown(0))
        {
            count++;
            mainCam.gameObject.SetActive(false);
            storyCam.gameObject.SetActive(true);
            modeCam.gameObject.SetActive(false);
        }

        UIUpdate();
    }

    public void UIUpdate()
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

 /*   public void GoHome()
    {
        SceneManager.LoadScene(0);
    }*/

    // 터치해서 게임 시작하는 함수 구현할 것

    public void OnClickSkip()
    {
        mainCam.gameObject.SetActive(false);
        storyCam.gameObject.SetActive(false);
        modeCam.gameObject.SetActive(true);
    }

    public void GoFishing()
    {
        SceneManager.LoadScene(1);
    }

    public void GoShop()
    {
        SceneManager.LoadScene(2);
    }

    public void GoUpgrade()
    {
        SceneManager.LoadScene(3);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
