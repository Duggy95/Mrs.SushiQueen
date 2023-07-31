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
    public GameObject fishIconPrefab;
    public GameObject[] customers;
    public GameObject orderFishContent;
    public GameObject endSceneQuestion;
    public GameObject logOutQuestion;
    public GameObject deleteDataQuestion;
    public GameObject exitGameQuestion;
    public Text dateTxt;  //��¥ + ����
    public Text goldTxt;  //���
    public Text atkTxt;  //���
    public Text orderTxt;
    public Text scoreTxt;
    public List<string> fishList;
    public int fishBtnCount = 0;
    //public Image[] fishImg;  //�����̹���
    public bool canMake = false;
    public bool isReady = false;
    bool config = false;
    public Vector2 customerStartPos;

    //Transform fishContent;

    WaitForSeconds ws;
    Vector2 customerTr = Vector2.zero;
    int count = 0;

    void Start()
    {
        customerStartPos = new Vector2(-450, -100);  //�մ� ��ġ

        //���� ���� ȭ�� ����
        orderView.SetActive(true);
        cookView.SetActive(true);
        fishList = new List<string>();

        /*if(int.Parse(GameManager.instance.data.score) <= 600)
        {
            ws = new WaitForSeconds(3f);
        }
        else if(int.Parse(GameManager.instance.data.score) <= 900)
        {
            ws = new WaitForSeconds(2.5f);
        }
        else
        {
            ws = new WaitForSeconds(2f);
        }*/
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
    }


    /*public void UIUpdate()
    {
        dateTxt.text = GameManager.instance.data.dateCount + "����";
        scoreTxt.text = "���� : " + GameManager.instance.data.score;
        goldTxt.text = "gold : " + GameManager.instance.data.gold;
        atkTxt.text = "���ݷ� : " + GameManager.instance.data.atk;
    }*/

    public void UIUpdate()
    {
        dateTxt.text = GameManager.instance.data.dateCount;
        scoreTxt.text = GameManager.instance.data.score;
        goldTxt.text = GameManager.instance.data.gold;
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
        endSceneQuestion.gameObject.SetActive(false);
    }

    public void EndSceneQuestion()
    {
        endSceneQuestion.gameObject.SetActive(true);
    }

    public void ExitGameQuestionEsc()
    {
        exitGameQuestion.gameObject.SetActive(false);
    }

    public void ExitGameQuestion()
    {
        exitGameQuestion.gameObject.SetActive(true);
    }

    public void DeleteDataQuestionEsc()
    {
        deleteDataQuestion.gameObject.SetActive(false);
    }

    public void DeleteDataQuestion()
    {
        deleteDataQuestion.gameObject.SetActive(true);
    }

    public void LogOutQuestionEsc()
    {
        endSceneQuestion.gameObject.SetActive(false);
    }

    public void LogOutQuestion()
    {
        logOutQuestion.gameObject.SetActive(true);
    }

    public void ConfigBtn() //���������ֱ�
    {
        if(!config)
        {
            configPanel.SetActive(true);
            config = true;
        }
        else
        {
            configPanel.SetActive(false);
            config = false;
        }
    }

    public void ViewOrder()  //�ֹ�ȭ�� �丮ȭ�� ��ȯ �޼���
    {
        if(canMake)  //���� �� ���� ��
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

    public IEnumerator Create()
    {
        int random = Random.Range(0, customers.Length);
        //2�� �� �մ� ����.
        yield return ws;
        GameObject customer = Instantiate(customers[random], customerTr,
                                                                Quaternion.identity, orderView.transform);
        customer.transform.SetSiblingIndex(1);  //2��° �ڽ�.
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

        /*for(int i = 0; i < fishBtnCount; i++)
        {
            GameObject fishIcon = Instantiate(fishIconPrefab, orderFishContent.transform);
            fishIcon.GetComponent<Image>.sprite = 
        }*/
    }

    public void Order(string txt)
    {
        orderTxt.text = txt;
    }

    /*public void LogOut()
    {
        GPGSBinder.Inst.Logout();
    }*/
}
