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
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }
}
