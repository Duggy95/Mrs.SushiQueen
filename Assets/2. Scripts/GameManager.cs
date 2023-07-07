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

    public int dateCount;  //  ��¥
    public int score;  // ����
    public int gold;  // ���
    public bool nextStage;

    private void Awake()
    {
        if (instance != this) // �̱���� �� �ڽ��� �ƴ϶�� ����
        {
            Destroy(gameObject);
        }

        else
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        // ��� ����� ���� ���ٸ� �ʱ� 0 ����
        if (PlayerPrefs.HasKey("GOLD") == false)
        {
            gold = 0;
            PlayerPrefs.SetInt("GOLD", gold);
        }
        // �ִٸ� ��忡 ����� �� ����
        else
        {
            gold = PlayerPrefs.GetInt("GOLD", gold);
        }
        // ��¥ ����� ���� ���ٸ� �ʱ� 0 ����
        if (PlayerPrefs.HasKey("DATE") == false)
        {
            dateCount = 0;
            PlayerPrefs.SetInt("DATE", dateCount);
        }
        // �ִٸ� �� �����ͼ� ����
        else
        {
            dateCount = PlayerPrefs.GetInt("DATE", dateCount);
        }
        // ���� ����� ���� ���ٸ� �ʱ� 0 ����
        if (PlayerPrefs.HasKey("SCORE") == false)
        {
            score = 0;
            PlayerPrefs.SetInt("SCORE", score);
        }
        // �ִٸ� ���� �� ������ ����
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
