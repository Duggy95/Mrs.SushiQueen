using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get 
        {
            if (m_instance == null)
            {
                m_instance = FindAnyObjectByType<UIManager>();
            }
            return m_instance;
        }
    }
    static UIManager m_instance;

    public Camera homeMainCam;  // Ȩ���� ����ī�޶�
    public Camera homeStoryCam;  // Ȩ���� ���丮ī�޶�
    public Camera homeModeCam;  // Ȩ���� ��弱��ī�޶�
    public Camera orderCam;  // ������ �ֹ� ȭ��
    public Camera cookCam;  // ������ �丮ȭ��

    public Button homeBtn;  // Ȩ���� ���� ��ư
    public Button inventoryBtn;  // �κ��丮 ���� ��ư
    public Button AquariumBtn;  // ������ ���� ��ư
    public Button sellBtn;  // �Ǹ��ϱ� ��ư
    public Button skipBtn;  // ���丮 ��ŵ ��ư
    public Button startFishingBtn;  // ���þ����� ���� ��ư
    public Button startBusinessBtn;  // �������� ���� ��ư
    public Button stopFishingBtn;  // ���þ� ���� >> �������� ���� ��ư
    public Button stopBusinessBtn;  // ���� ���� >> ������� ���� ��ư
    public Button startManagementBtn;  // ������� ���� ��ư
    public Button endStageBtn;  // �������� ���� >> ���� ��¥�� ���� ��ư 
    public Button acceptBtn;  //  �ֹ� ���� ��ư
    public Button refuseBtn;  // �ֹ� ���� ��ư
    public Button riceBtn;  // �� ��ư
    public Button wasabiBtn;  // �ͻ�� ��ư
    public Button[] sushiImg;  // �ʹ� ��ư (�迭 �ƴϸ� enum?)
    public Text[] storyTxt;  // ���丮 �ؽ�Ʈ (���丮�� ������? �̸� �迭 �״��)
    public Text gameStartTxt;  // ����ȭ�鿡�� ���� ������ �ȳ��ϴ� �ؽ�Ʈ (ȿ�� �� ��)
    public Text dateTxt;  // ��¥�� ���������� ����� �ؽ�Ʈ
    public Text goldTxt;  // ���� �ݾ��� ��Ÿ�� �ؽ�Ʈ
    public Text fishingStartTxt;  // ����⸦ ������� �ȳ��ϴ� �ؽ�Ʈ (ȿ�� �� ��)
    public Text fishRunTxt;  // ����Ⱑ �������� �� ���� �ؽ�Ʈ;
    public Text getFishTxt;  // ����� ����� �� ���� �ؽ�Ʈ
    public Image getFishImg;  // ����� ����� �� ���� �̹���
    public Image clientImg;  // �մ� �̹���
    public Image[] fishImg;  // ����� �̹��� (�迭 �ƴϸ� enum?)
    public Scrollbar timer;  // Ÿ�̸�
    public Scrollbar fishHealth;  // ����� ü�¹�
    public Scrollbar clientHealth;  // �մ� ü�¹�
    
    private void Awake()
    {
        if (instance != this) // �̱���� �� �ڽ��� �ƴ϶�� ����
        {
            Destroy(gameObject);
        }
    }

    public void OnclickSkip()
    {

    }

    public void GoHomeScene()
    {
        SceneManager.LoadScene(0);
    }

    public void GoFishingScene()
    {
        SceneManager.LoadScene(1);
    }

    public void GoBusinessScene() 
    {
        SceneManager.LoadScene(2);
    }

    public void GoManagementScene()
    {
        SceneManager.LoadScene(3);
    }
}
