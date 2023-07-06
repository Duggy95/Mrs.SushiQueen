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

    public Text dateTxt;  // ��¥ + ���� �ؽ�Ʈ
    public Text goldTxt;  // ��� ���� �ؽ�Ʈ
    public int dateCount;  //  ��¥
    public int score;  // ����
    public int gold;  // ���

    private void Awake()
    {
        if (instance != this) // �̱���� �� �ڽ��� �ƴ϶�� ����
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject); // ����ȯ�� �ŵ� ����X

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

        if(dateTxt == null)
            dateTxt = GameObject.Find("Time_Text").GetComponent<Text>();

        if (goldTxt == null)
            goldTxt = GameObject.Find("Gold_Text").GetComponent<Text>();

    }

    void Start()
    {
        dateTxt.text = dateCount + "���� / ���� : " + score;
        goldTxt.text = "gold : " + gold;
    }

    void Update()
    {
        UIUpdate();
    }

    public void UIUpdate()
    {
        dateTxt.text = dateCount + "���� / ���� : " + score;
        goldTxt.text = "gold : " + gold;
    }

    public void ViewInventory()
    {
        // �κ��丮 Ȱ��ȭ
    }

    public void GoHome()
    {
        // ���ξ�����
        // ���� �ʱ�ȭ�� ����
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }
}
