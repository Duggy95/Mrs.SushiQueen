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

    public int itemCount;
    public int fishCount;

    public int dateCount;  //  날짜
    public int score;  // 점수
    public int gold;  // 골드

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
            SetGold();

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

    }

    public void SetGold()
    {
        gold = 0;
        PlayerPrefs.SetInt("GOLD", gold);
    }

    public void SetDate()
    {
        dateCount = 1;
        PlayerPrefs.SetInt("DATE", dateCount);
    }

    public void SetScore()
    {
        score = 0;
        PlayerPrefs.SetInt("SCORE", score);
    }

    public void SetItem()
    {
        itemCount = 3;
        PlayerPrefs.SetInt("ITEM", itemCount);
    }

    public void SetFish()
    {
        fishCount = 3;
        PlayerPrefs.SetInt("FISH", fishCount);
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }
}
