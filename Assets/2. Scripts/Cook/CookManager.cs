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
    public Transform dish;
    public Text dateTxt;  //��¥ + ����
    public Text goldTxt;  //���
    public Text atkTxt;  //���
    public Text orderTxt;
    public Text scoreTxt;
    public Text priceTxt;
    public List<string> fishList = new List<string>();
    public int fishBtnCount = 0;
    public bool canMake = false;
    public bool isReady = false;
    public bool isEnd = false;
    public Vector2 customerStartPos;

    AudioSource audioSource;
    WaitForSeconds ws;
    Vector2 customerTr = Vector2.zero;
    int count = 0;
    bool config = false;

    void Start()
    {
        Time.timeScale = 1;
        audioSource = GetComponent<AudioSource>();
        customerStartPos = new Vector2(-450, -100);  //�մ� ��ġ

        //���� ���� ȭ�� ����
        orderView.SetActive(true);
        cookView.SetActive(true);
        
        ws = new WaitForSeconds(2);

        UIUpdate();
        priceTxt.text = "0";
    }

    void Update()
    {
        UIUpdate();
    }

    public void GoEndScene()  //�������
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        SceneManager.LoadScene(3);
    }

    public void Delete()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        GameManager.instance.DeleteData();
        UIUpdate();
        //ExitGame();
        GameManager.instance.nextStage = false;
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        GameManager.instance.DeleteData();
        GameManager.instance.nextStage = false;
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
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        inventoryFullImg.gameObject.SetActive(true);
        InventoryImg.gameObject.SetActive(true);
    }

    public void EscInventory() //�κ��丮 ������
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        inventoryFullImg.gameObject.SetActive(false);
        logOutQuestion.gameObject.SetActive(false);
    }

    public void EndSceneQuestionEsc()
    {
        Time.timeScale = 1;
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        endSceneQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);

    }

    public void EndSceneQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        Time.timeScale = 0;
        endSceneQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void ExitGameQuestionEsc()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        exitGameQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void ExitGameQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        exitGameQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void DeleteDataQuestionEsc()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        deleteDataQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void DeleteDataQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        deleteDataQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void LogOutQuestionEsc()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        endSceneQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void LogOutQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        logOutQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void ConfigBtn() //���������ֱ�
    {
        if (!config)
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

            configPanel.SetActive(true);
            inventoryFullImg.gameObject.SetActive(true);
            config = true;
        }
        else
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

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
                audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

                cookView.SetActive(false);
                dish.transform.SetParent(orderView.transform);
                count = 0;
            }
            else
            {
                audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

                cookView.SetActive(true);
                dish.transform.SetParent(cookView.transform);
                dish.transform.SetSiblingIndex(1);  //2��° �ڽ�.
            }
        }
    }

    public void GoOrder()
    {
        cookView.SetActive(false);
        dish.transform.SetParent(orderView.transform);
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
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        Application.Quit();
    }

    public void ReadyBtn()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        isReady = true;
        StartCoroutine(Create());
        readyBtn.SetActive(false);
        orderView.SetActive(true);
        cookView.SetActive(false);
        InventoryImg.gameObject.SetActive(false);
        inventoryCanvas.interactable = false;
        dish.transform.SetParent(orderView.transform);
        dish.transform.SetSiblingIndex(2);  //2��° �ڽ�.
    }

    public void Order(string txt)
    {
        orderTxt.text = txt;
    }

    public void LogOut()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        GPGSBinder.Inst.Logout();
        logOutQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
        GameManager.instance.nextStage = false;
        SceneManager.LoadScene(0);
    }

    private void OnDisable()
    {
        fishList.Clear();
    }
}
