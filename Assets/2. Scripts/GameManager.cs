using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindAnyObjectByType<GameManager>();
            }
            return m_instance;
        }
    }
    static GameManager m_instance;

    public Text dateTxt;  // 날짜 + 점수 텍스트
    public Text goldTxt;  // 골드 보유 텍스트
    public int dateCount;  //  날짜
    public int score;  // 점수
    public int gold;  // 골드

    private void Awake()
    {
        if (instance != this) // 싱글톤된 게 자신이 아니라면 삭제
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject); // 씬전환이 돼도 삭제X

        // 골드 저장된 값이 없다면 초기 0 저장
        if (PlayerPrefs.HasKey("GOLD") == false)
        {
            gold = 0;
            PlayerPrefs.SetInt("GOLD", gold);
        }
        // 있다면 골드에 저장된 값 저장
        else
        {
            gold = PlayerPrefs.GetInt("GOLD", gold);
        }
        // 날짜 저장된 값이 없다면 초기 0 저장
        if (PlayerPrefs.HasKey("DATE") == false)
        {
            dateCount = 0;
            PlayerPrefs.SetInt("DATE", dateCount);
        }
        // 있다면 값 가져와서 저장
        else
        {
            dateCount = PlayerPrefs.GetInt("DATE", dateCount);
        }
        // 점수 저장된 값이 없다면 초기 0 저장
        if (PlayerPrefs.HasKey("SCORE") == false)
        {
            score = 0;
            PlayerPrefs.SetInt("SCORE", score);
        }
        // 있다면 점수 값 가져와 저장
        else
        {
            score = PlayerPrefs.GetInt("SCORE", score);
        }

        if(dateTxt == null)
            dateTxt = GameObject.Find("Time_Text").GetComponent<Text>();

        if (goldTxt == null)
            goldTxt = GameObject.Find("Gold_Text").GetComponent<Text>();

    }

    void Start()
    {
        dateTxt.text = dateCount + "일차 / 평판 : " + score;
        goldTxt.text = "gold : " + gold;
    }

    void Update()
    {
        UIUpdate();
    }

    public void UIUpdate()
    {
        dateTxt.text = dateCount + "일차 / 평판 : " + score;
        goldTxt.text = "gold : " + gold;
    }

    public void ViewInventory()
    {
        // 인벤토리 활성화
    }

    public void GoHome()
    {
        // 메인씬으로
        // 제일 초기화면 셋팅
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }
}
