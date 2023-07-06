using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneCtrl : MonoBehaviour
{
    public Camera mainCam;
    public Camera storyCam;
    public Camera modeCam;
    public Text StartTxt;
    public Text[] storytxt;

    int count = 0;

    private void Awake()
    {
        mainCam.gameObject.SetActive(true);
        storyCam.gameObject.SetActive(false);
        modeCam.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(count == 0 && Input.GetMouseButtonDown(0))
        {
            count++;
            mainCam.gameObject.SetActive(false);
            storyCam.gameObject.SetActive(true);
            modeCam.gameObject.SetActive(false);
        }
    }

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
}
