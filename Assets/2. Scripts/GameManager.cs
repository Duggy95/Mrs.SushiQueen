using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public int itemCount = 3;
    public int fishCount = 3;

    public int dateCount = 1;  //  날짜
    public int score = 0;  // 점수
    public int gold = 0;  // 골드
    public int atk = 1;

    public List<string> items = new List<string>();
    public List<string> fishs = new List<string>();

    public bool nextStage;

    private void Awake()
    {
        if (instance != this) // 싱글톤된 게 자신이 아니라면 삭제
        {
            Destroy(gameObject);
        }

        else
            DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("GOLD"))
        {
            gold = PlayerPrefs.GetInt("GOLD");
        }
        else
            SetGold();

        if (PlayerPrefs.HasKey("DATE"))
        {
            dateCount = PlayerPrefs.GetInt("DATE");
        }
        else
            SetDate();

        if (PlayerPrefs.HasKey("SCORE"))
        {
            score = PlayerPrefs.GetInt("SCORE");
        }
        else
            SetScore();

        if (PlayerPrefs.HasKey("ITEM"))
        {
            itemCount = PlayerPrefs.GetInt("ITEM");
        }
        else
            SetItem();

        if (PlayerPrefs.HasKey("FISH"))
        {
            fishCount = PlayerPrefs.GetInt("FISH");
        }
        else
            SetFish();

        if (PlayerPrefs.HasKey("ATK"))
        {
            atk = PlayerPrefs.GetInt("ATK");
        }
        else
            SetAtk();

    }

    public void SetGold()
    {
        PlayerPrefs.SetInt("GOLD", gold);
    }

    public void SetDate()
    {
        PlayerPrefs.SetInt("DATE", dateCount);
    }

    public void SetScore()
    {
        PlayerPrefs.SetInt("SCORE", score);
    }

    public void SetItem()
    {
        PlayerPrefs.SetInt("ITEM", itemCount);
    }

    public void SetFish()
    {
        PlayerPrefs.SetInt("FISH", fishCount);
    }

    public void SetAtk()
    {
        PlayerPrefs.SetInt("ATK", atk);
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }
}
