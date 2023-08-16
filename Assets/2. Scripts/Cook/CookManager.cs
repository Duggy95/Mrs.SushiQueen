using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CookManager : MonoBehaviour
{
    public CanvasGroup inventoryCanvas;  //�κ��丮
    public GameObject customerPrefab;  //�մ� ������
    public GameObject orderView;  //�ֹ�ȭ��
    public GameObject cookView;  //�丮ȭ��
    public GameObject configPanel;  //���� â
    public GameObject readyBtn;  //�غ�Ϸ� ��ư
    public GameObject InventoryImg;  //�κ��丮
    public GameObject inventoryFullImg;  //�κ��丮 ������ �ֺ������� �̹���
    public GameObject blockFullImg;  //������ �̹���
    public GameObject fishIconPrefab;  //���� ������ ������
    public GameObject[] customers;  //�մԵ� �迭
    public GameObject orderFishContent;  //�ֹ�â�� �����г�

    public Transform fishScroll;  //ȸ��ư ��ũ��
    NetaButton[] fishSlots;  //ȸ��ư ������� �ҷ���������
    public Transform dish;  //����
    public Text dateTxt;  //��¥ + ����
    public Text goldTxt;  //���
    public Text atkTxt;  //���
    public Text orderTxt;  //�ֹ� �ؽ�Ʈ
    public Text scoreTxt;  //���� �ؽ�Ʈ
    public Text priceTxt;  //������ ���� �ؽ�Ʈ
    public List<string> fishList = new List<string>();  //�巡�� ��ӿ��� �ߺ�Ȯ���� ���� ��������Ʈ
    public int fishBtnCount = 0;
    public bool canMake = false;  //����� �������� �ƴ���
    public bool isReady = false;  //�غ�Ϸ����� �ƴ���
    public bool isEnd = false;  //�������� �ƴ���
    public Vector2 customerStartPos;  //�մ� FadeIn ���� ����

    AudioSource audioSource;  //����� �ҽ�
    WaitForSeconds ws;  //�ڷ�ƾ �ð�
    Vector2 customerTr = Vector2.zero;  //�մ� ���� ��ġ
    int count = 0;
    bool config = false;

    [Header("Window")]
    public GameObject endSceneQuestion;  //���� �� ����
    public GameObject logOutQuestion;  //�α׾ƿ�
    public GameObject deleteDataQuestion;  //������ �����
    public GameObject exitGameQuestion;  //�����ϱ�
    public GameObject endScenePanel;  //���� �� �̵�
    public GameObject gameOverView;  //���ӿ��� â
    public GameObject restaurant;  //�Ĵ� �̹���
    public GameObject stamp;  //����
    public GameObject gameOverTxt;  //���ӿ��� �ؽ�Ʈ
    public GameObject restartBtn;  //����� ��ư

    void Start()
    {
        Time.timeScale = 1;  //���۽� timeScale �ʱ�ȭ
        audioSource = GetComponent<AudioSource>();
        customerStartPos = new Vector2(-450, -100);  //�մ� ��ġ

        //���� ���� ȭ�� ����
        orderView.SetActive(true);
        cookView.SetActive(true);
        
        ws = new WaitForSeconds(2);

        UIUpdate();  //UI �ֽ�ȭ
        priceTxt.text = "0";  //������ ���� �ؽ�Ʈ

        fishSlots = fishScroll.gameObject.GetComponentsInChildren<NetaButton>();  //������ư�� ������ �ִ� ��ҵ� ��������
    }

    public void GoEndScene()  //�������
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        SceneManager.LoadScene(3);
    }

    public void Delete()  //������ �����
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        GameManager.instance.DeleteData();
        UIUpdate();
        //ExitGame();
        GameManager.instance.nextStage = false;  //����ȭ�鿡�� ���丮+Ʃ�丮�� �����ϱ� ����.
        SceneManager.LoadScene(0);
    }

    public void GameOver()  //���� ����
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        GameManager.instance.DeleteData();
        GameManager.instance.nextStage = false;
        SceneManager.LoadScene(0);
    }

    public void UIUpdate()
    {
        //GameManager.instance.LogData();

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

    public void EndSceneQuestionEsc()  //���� �� ����������
    {
        Time.timeScale = 1;
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        endSceneQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);

    }

    public void EndSceneQuestion()  //���� �� ����
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        Time.timeScale = 0;
        endSceneQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void ExitGameQuestionEsc()  //�������� ���� ������
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        exitGameQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void ExitGameQuestion()  //�������� ����
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        exitGameQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void ExitGame()  //���������ϱ�
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        Application.Quit();
    }

    public void DeleteDataQuestionEsc()  //������ ����� ���� ������
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        deleteDataQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void DeleteDataQuestion()  //������ ����� ����
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        deleteDataQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void LogOutQuestionEsc()  //�α׾ƿ� ���� ������
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        endSceneQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void LogOutQuestion()  //�α׾ƿ� ����
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        logOutQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void LogOut()  //�α׾ƿ�
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        GPGSBinder.Inst.Logout();
        logOutQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
        GameManager.instance.loginSuccess = false;
        GameManager.instance.nextStage = false;
        SceneManager.LoadScene(0);
    }

    public void ConfigBtn() //���������ֱ�
    {
        if (!config)  //�������� �� Ű��
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

            configPanel.SetActive(true);
            inventoryFullImg.gameObject.SetActive(true);
            config = true;
        }
        else  //���������� ����
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

            configPanel.SetActive(false);
            inventoryFullImg.gameObject.SetActive(false);
            config = false;
        }
    }

    public void ViewOrder()  //�ֹ�ȭ�� �丮ȭ�� ��ȯ �޼���
    {
        if (canMake)  //���� �� ���� �� = �ֹ� �޾��� ���� ȭ����ȭ ����
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

    public void GoOrder()  //�ֹ� â���� ����
    {
        cookView.SetActive(false);
        dish.transform.SetParent(orderView.transform);
    }

    public IEnumerator Create()  //�մ� ���� �޼���
    {
        if (!isEnd)  //������ �ȳ����� ����
        {
            //������ �մ�
            int random = Random.Range(0, customers.Length);
            //2�� �� �մ� ����.
            yield return ws;
            GameObject customer = Instantiate(customers[random], customerTr,
                                                                    Quaternion.identity, orderView.transform);
            customer.transform.SetSiblingIndex(1);  //2��° �ڽ�.
        }
    }

    public void ReadyBtn()  //�غ�Ϸ� ��ư
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        isReady = true;  //�غ�Ϸ�
        StartCoroutine(Create());  //�մ� ����
        readyBtn.SetActive(false);
        orderView.SetActive(true);
        cookView.SetActive(false);
        InventoryImg.gameObject.SetActive(false);
        inventoryCanvas.interactable = false;  //�κ��丮 �ȴ����� ����
        dish.transform.SetParent(orderView.transform);  //���� �ֹ�ȭ�鿡�� ���̰� �ϱ�
        dish.transform.SetSiblingIndex(2);  //2��° �ڽ�.

        //�غ�Ϸ��ϸ� �� ĭ�� ������ư�� �����ϱ�
        for (int i = 0; i < fishSlots.Length; i++)
        {
            if (fishSlots[i].fishData == null)
            {
                Destroy(fishSlots[i].gameObject);
            }
        }
    }

    public void Order(string txt)  //�丮ȭ�鿡�� �ֹ����� ���̰Բ� �ϴ� �޼���
    {
        orderTxt.text = txt;
    }

    public IEnumerator GameOverCoroutine()  //���ӿ��� �ڷ�ƾ
    {
        //1�ʵ� �������� ȭ����� ������� �̹��� �Ʒ����� ���� õõ�� �ö���� �ϱ�
        yield return new WaitForSeconds(1);
        SoundManager.instance.audioSource.Stop();
        gameOverView.gameObject.SetActive(true);
        Vector2 initPos = new Vector2(0, -850);
        Vector2 targetPos = new Vector2(0, -95);
        float duration = 3f;
        float elapsedTime = 0f;
        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // �ð� ���� ���
            restaurant.transform.localPosition = Vector2.Lerp(initPos, targetPos, t);
            yield return null;
        }

        //���帶ũ ���̰� �ϱ�
        stamp.gameObject.SetActive(true);
        Vector3 initScale = new Vector3(1.2f, 1.2f, 1.2f);
        Vector3 targetScale = new Vector3(0.8f, 0.8f, 0.8f);
        duration = 0.5f;
        elapsedTime = 0f;
        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            stamp.transform.localScale = Vector3.Lerp(initScale, targetScale, t);
            print(elapsedTime);
            yield return null;
        }
        audioSource.PlayOneShot(SoundManager.instance.stampSound, 1);
        restartBtn.gameObject.SetActive(true);
        gameOverTxt.gameObject.SetActive(true);
        print("�ڷ�ƾ ȣ��");
    }

    private void OnDisable()
    {
        fishList.Clear();  //���� ����Ʈ Ŭ����.
    }
}
