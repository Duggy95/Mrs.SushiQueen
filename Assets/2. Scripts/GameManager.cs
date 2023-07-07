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

    public int dateCount = 5;  //  ��¥
    public int score = 50;  // ����
    public int gold = 500;  // ���
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
