using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CookManager : MonoBehaviour
{
    public CanvasGroup inventoryCanvas;
    public GameObject customerPrefab;  //�մ� ������
    public GameObject orderView;  //�ֹ�ȭ��
    public GameObject cookView;  //�丮ȭ��
    public GameObject configPanel;
    public GameObject readyBtn;
    public GameObject InventoryImg;  //�κ��丮
    public GameObject inventoryFullImg;
    public GameObject blockFullImg;
    public GameObject fishIconPrefab;
    public GameObject[] customers;
    public GameObject orderFishContent;
    public GameObject endSceneQuestion;
    public GameObject logOutQuestion;
    public GameObject deleteDataQuestion;
    public GameObject exitGameQuestion;
    public GameObject endScenePanel;
    public GameObject endGameView;
    public Text dateTxt;  //��¥ + ����
    public Text goldTxt;  //���
    public Text atkTxt;  //���
    public Text orderTxt;
    public Text scoreTxt;
    public List<string> fishList = new List<string>();
    public int fishBtnCount = 0;
    public bool canMake = false;
    public bool isReady = false;
    public bool isEnd = false;
    public Vector2 customerStartPos;

    WaitForSeconds ws;
    Vector2 customerTr = Vector2.zero;
    int count = 0;
    bool config = false;

    void Start()
    {
        Time.timeScale = 1;
        customerStartPos = new Vector2(-450, -100);  //�մ� ��ġ

        //���� ���� ȭ�� ����
        orderView.SetActive(true);
        cookView.SetActive(true);
        
        ws = new WaitForSeconds(2);

        UIUpdate();
    }

    void Update()
    {
        UIUpdate();
    }

    public void GoEndScene()  //�������
    {
        SceneManager.LoadScene(3);
    }

    public void Delete()
    {
        GameManager.instance.DeleteData();
        UIUpdate();
        ExitGame();
    }

    public void GameOver()
    {
        GameManager.instance.DeleteData();
        SceneManager.LoadScene(0);
    }

    public void UIUpdate()
    {
        dateTxt.text = int.Parse(GameManager.instance.data.dateCount).ToString("N0");
        scoreTxt.text = int.Parse(GameManager.instance.data.score).ToString("N0");
        goldTxt.text = int.Parse(GameManager.instance.data.gold).ToString("N0");
        atkTxt.text = GameManager.instance.data.atk;
    }

    public void ViewInventory() //�κ��丮 Ȱ��ȭ
    {
        inventoryFullImg.gameObject.SetActive(true);
        InventoryImg.gameObject.SetActive(true);
    }

    public void EscInventory() //�κ��丮 ������
    {
        inventoryFullImg.gameObject.SetActive(false);
        logOutQuestion.gameObject.SetActive(false);
    }

    public void EndSceneQuestionEsc()
    {
        Time.timeScale = 1;
        endSceneQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);

    }

    public void EndSceneQuestion()
    {
        Time.timeScale = 0;
        endSceneQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void ExitGameQuestionEsc()
    {
        exitGameQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void ExitGameQuestion()
    {
        exitGameQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void DeleteDataQuestionEsc()
    {
        deleteDataQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void DeleteDataQuestion()
    {
        deleteDataQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void LogOutQuestionEsc()
    {
        endSceneQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void LogOutQuestion()
    {
        logOutQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void ConfigBtn() //���������ֱ�
    {
        if (!config)
        {
            configPanel.SetActive(true);
            inventoryFullImg.gameObject.SetActive(true);
            config = true;
        }
        else
        {
            configPanel.SetActive(false);
            inventoryFullImg.gameObject.SetActive(false);
            config = false;
        }
    }

    public void ViewOrder()  //�ֹ�ȭ�� �丮ȭ�� ��ȯ �޼���
    {
        if (canMake)  //���� �� ���� ��
        {
            count++;
            if (count % 2 == 0)
            {
                cookView.SetActive(false);
                count = 0;
            }
            else
            {
                cookView.SetActive(true);
            }
        }
    }

    public void GoOrder()
    {
        cookView.SetActive(false);
    }

    public IEnumerator Create()
    {
        if (!isEnd)
        {
            int random = Random.Range(0, customers.Length);
            //2�� �� �մ� ����.
            yield return ws;
            GameObject customer = Instantiate(customers[random], customerTr,
                                                                    Quaternion.identity, orderView.transform);
            customer.transform.SetSiblingIndex(1);  //2��° �ڽ�.
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReadyBtn()
    {
        isReady = true;
        StartCoroutine(Create());
        readyBtn.SetActive(false);
        orderView.SetActive(true);
        cookView.SetActive(false);
        InventoryImg.gameObject.SetActive(false);
        inventoryCanvas.interactable = false;
    }

    public void Order(string txt)
    {
        orderTxt.text = txt;
    }

    /*public void LogOut()
    {
        GPGSBinder.Inst.Logout();
            logOutQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
            ExitGame();
    }*/

    private void OnDisable()
    {
        fishList.Clear();
    }
}
