using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FishingManager : MonoBehaviour
{
    public Canvas canvas;
    public Text dateTxt;  // x����
    public Text goldTxt;  // ���� ���
    public Text atkTxt;   // ���� ���ݷ�
    public Text fishInfo_Txt;  // ����� ���� �ؽ�Ʈ
    // �������� ��������� �˸��� �ؽ�Ʈ
    public Text useWhiteItemTxt;   
    public Text useRedItemTxt;
    public Text useRareItemTxt;
    public Text scoreTxt;  // ����
    public Text touchTxt;  // ��ġ�϶�� �˸��� �ؽ�Ʈ
    public GameObject full_Txt;  // ����á���� �˷��ִ� �ǳ�
    public GameObject fishRun;   // ����Ⱑ ���������� �˸��� �ǳ�
    public GameObject configPanel;   // ����â
    public GameObject inventoryImg;  // �κ��丮 �̹���
    public GameObject inventoryFullImg;   // �ٸ� ui���� ������ ���� �����̹���
    public GameObject blockFullImg;    // �ٸ� ui���� ������ ���� �����̹���
    public GameObject inventoryBtn;  // �κ��丮 ��ư
    public GameObject fishInfoImg;   // ����� ���� �̹���
    public GameObject fishContent; // ������
    public GameObject fishInfo; // ���� ���� �ǳ�
    public GameObject fishingRod; // ���˴� �̹���
    public GameObject lineStartPos;  // ���η����� ������
    public GameObject useItemPanel;   // ������ ��������� �˸��� �ؽ�Ʈ �ǳ�
    public GameObject fishObj;    // ����� 
    public GameObject endSceneQuestion;  // ���� ���� �ȳ�
    public GameObject logOutQuestion;   // �α� �ƿ� �ȳ�
    public GameObject deleteDataQuestion;  // ���� ���� �ȳ�
    public GameObject exitGameQuestion;  // ���� ���� �ȳ�
    public GameObject giveupQuestion;   // ���� ���� �ȳ�
    public GameObject endScenePanel;  // ���� ������ �̵����� �˸��� �ǳ�
    public Button fishingBtn;  // ����� ���� �� ��ġ ������ ����
    public Button giveupBtn;  // ���� ���� ��ư
    public Image fish_Img;   // ����� �̹���
    public bool isFishing = false;  // ���� ���������� ����
    public bool useItem_white = false;  // �Ͼ� �� ���� Ȯ�� ���� ������ ��� ����
    public bool useItem_red = false;    // ���� �� ���� Ȯ�� ���� ������ ��� ����
    public bool useItem_rare = false;   // ���� ���� Ȯ�� ���� ������ ��� ����

    Vector3 fishInfoOriginPos;   // ����� ����â������ �̹��� �� ��ġ
    Vector3 fishInfoOriginScale;  //  ����� ����â������ �̹��� �� ũ��
    bool config;  // ����â �������� ����
    FishData data;  // ����� ������ ��ũ���ͺ�
    AudioSource audioSource;  // ����� �ҽ�

    void Start()
    {
        Time.timeScale = 1;  // ���� ����
        audioSource = GetComponent<AudioSource>();
        // ����� ����â������ ����� �̹��� ũ��� ��ġ ����
        fishInfoOriginScale = Vector3.one;   
        fishInfoOriginPos = fishInfoImg.transform.position;
        // ���� �ǳڰ� �ؽ�Ʈ �ʱ� ����
        useItemPanel.gameObject.SetActive(false);
        useRareItemTxt.gameObject.SetActive(false);
        useRedItemTxt.gameObject.SetActive(false);
        useWhiteItemTxt.gameObject.SetActive(false);
        inventoryImg.gameObject.SetActive(false);
        UIUpdate();  // �� �̵��� ���ÿ� ���� ������Ʈ
    }

    // ������ �����ϰ� �ݿ� �� �ʱ� ������ �̵�
    public void Delete()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        GameManager.instance.DeleteData();
        UIUpdate();
        GameManager.instance.nextStage = false;
        SceneManager.LoadScene(0);
    }

    // ���� ���� ����
    public void Fishing()
    {
        // ���� ���� �ƴ϶�� ���� ����
        if (isFishing == false)
        {
            isFishing = true;
            // ������ ������ �Ҹ� ���
            audioSource.PlayOneShot(SoundManager.instance.swing, 1);
            // Ŭ�� ��ġ�� �Ű������� �ϴ� �ڷ�ƾ �Լ� ȣ��
            StartCoroutine(ThrowBobber(Input.mousePosition));
        }

        else
            return;
    }

    // �� ����
    IEnumerator ThrowBobber(Vector3 mousePos)
    {
        // ������ ���ư��� �ð� �ݿ�
        yield return new WaitForSeconds(1);
        // ����� ��� ������ ����
        // ����� ���� �ؽ�Ʈ ��Ȱ��ȭ
        fishRun.gameObject.SetActive(false);

        LineRenderer fishLine = fishingRod.GetComponent<LineRenderer>();

        Vector3 startPos = lineStartPos.transform.position; // ���� ����
        Vector3 endPos = Input.mousePosition; // �� ����

        // Line Renderer �Ӽ� ����
        fishLine.SetPosition(0, startPos); // ������ ���� ����
        fishLine.SetPosition(1, endPos);

        // Ŭ�� ��ġ�� ����� ����
        GameObject _fishObj = Instantiate(fishObj, mousePos, Quaternion.identity);
        _fishObj.transform.SetParent(canvas.transform);
        _fishObj.transform.SetSiblingIndex(1);  //2��° �ڽ�.
    }

    // ���� ��쿡 ����� ����â ���
    public void Fish(FishData fishData)
    {
        StartCoroutine(StopTouch());
        
        // ���� ����� ������ �޾Ƽ� ó��
        data = fishData;
        fishInfo.gameObject.SetActive(true);
        fishInfoImg.gameObject.SetActive(true);
        fishInfoImg.transform.parent = fishInfo.transform;
        fishInfoImg.transform.position = fishInfoOriginPos;
        fishInfoImg.transform.localScale = fishInfoOriginScale;
        fishInfoImg.transform.SetSiblingIndex(0);
        fishInfo_Txt.text = fishData.info.text;
        fish_Img.sprite = fishData.fishImg;
        audioSource.PlayOneShot(SoundManager.instance.fish, 1);
    }

    // ���� ���� �� 0.5�ʰ� ��ġ �Ұ�
    IEnumerator StopTouch()
    {
        inventoryFullImg.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        inventoryFullImg.SetActive(false);
    }

    // ���� ����
    public void GiveUp()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        // ���� �����ϵ��� ����
        isFishing = false;
        giveupQuestion.gameObject.SetActive(false);
    }

    // ���� ���� �ȳ�
    public void GiveUpQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        giveupQuestion.gameObject.SetActive(true);
    }

    public void GiveUpQuestionEsc() 
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        giveupQuestion.gameObject.SetActive(false);
    }

    // �ȱ� ��ư�� ������ ����â �ݰ� �ٽ� ���� �غ�
    public void Sell()
    {
        full_Txt.gameObject.SetActive(false);
        fishInfo.gameObject.SetActive(false);
        // ��� ���� �ݿ�
        int _gold = int.Parse(GameManager.instance.data.gold) + data.gold;
        GameManager.instance.data.gold = _gold.ToString();
        GameManager.instance.todayData.gold += data.gold;
        //Debug.Log("��� " + _gold);
        UIUpdate();
        audioSource.PlayOneShot(SoundManager.instance.levelUp, 1);
        isFishing = false;
    }

    // ���������� ��ư ������ ����â �ݰ� �ٽ� ���� �غ�
    public void Get()
    {
        // �������� �̹��� �߰�
        Image[] _fishs = fishContent.gameObject.GetComponentsInChildren<Image>();

        bool isFull = true;
        bool isChange = false;
        // ������ ���� �̹��� ����ŭ �ݺ��ϸ鼭
        for (int i = 0; i < _fishs.Length; i++)
        {
            // ���� �̹��� ������Ʈ�� �̸��� Slot�� �����ϰ�
            if (_fishs[i].gameObject.name.Contains("Slot"))
            {
                // �� �̹����� �ڽ� �ؽ�Ʈ�� ����� ������ �̸��� ���ٸ�
                if (_fishs[i].GetComponentInChildren<Text>().text.Contains(data.fishName))
                {
                    // ����� �̸��� ���� �ε��� ���ϱ�
                    string valueToFind = data.fishName;
                    int newValue = 1;

                    // Ư�� ��(valueToFind)�� �����ϴ� ù ��° ����� �ε����� ã��
                    int index = GameManager.instance.inventory_Fishs.FindIndex(fish => fish.fish_Name == valueToFind);

                    // ã�Ҵٸ�
                    if (index != -1)
                    {
                        // ������ �� ����� ���� 1 �÷���
                        newValue += int.Parse(GameManager.instance.inventory_Fishs[index].fish_Count);

                        // ������ ����� ��Ȯ���� �߰�
                        GameManager.instance.todayFishInfos.Add(new TodayFishInfo(data.fishName, 1));

                        // �ش� �ε���(index)�� �� ����
                        GameManager.instance.inventory_Fishs[index].fish_Count = newValue.ToString();
                        _fishs[i].GetComponentInChildren<Text>().text = data.fishName + "   " + newValue.ToString() + " " + "����";
                        //Debug.Log("�ߺ� ���� " + index + " changed to " + newValue);
                        fishInfoImg.transform.parent = inventoryBtn.transform;
                        StartCoroutine(EffScale());
                        StartCoroutine(EffMove());
                        isChange = true;
                        isFull = false;
                        break;
                    }
                }
            }
        }

        // ������ ���� �ߺ��� ����Ⱑ ���� ���
        if (isChange == false)
        {
            for (int i = 0; i < _fishs.Length; i++)
            {
                if (_fishs[i].gameObject.name.Contains("Slot"))
                {
                    FishSlot _slot = _fishs[i].GetComponent<FishSlot>();
                    Image Img = null;
                    Image[] fishImgs = _fishs[i].GetComponentsInChildren<Image>();
                    foreach (Image fishImg in fishImgs)
                    {
                        if (fishImg.name.Contains("Img"))
                            Img = fishImg;
                    }

                    if (_slot.isEmpty == false)  // ������ ����ִٸ�
                    {
                        // ����� ���� �ݿ�
                        Img.sprite = data.fishImg;
                        _slot.fish_ColorNum = data.color;
                        _slot.fish_GradeNum = data.grade;
                        _slot.fish_Name = data.fishName;

                        // �� ���� �ݿ�
                        GameManager.instance.inventory_Fishs.Add(new InventoryFish(data.fishName, "1"));
                        // ������ ���� �ݿ�
                        GameManager.instance.todayFishInfos.Add(new TodayFishInfo(data.fishName, 1));

                        _fishs[i].GetComponentInChildren<Text>().text = data.fishName + "   " + "1 ����";
                        //Debug.Log("��á�� �ٸ� ����");
                        fishInfoImg.transform.parent = inventoryBtn.transform;
                        StartCoroutine(EffScale());
                        StartCoroutine(EffMove());
                        _slot.isEmpty = true;
                        isFull = false;
                        break;
                    }
                }
            }
        }
        // �������� ������ ��� �ȳ����� ���
        if (isFull)
        {
            full_Txt.gameObject.SetActive(true);
        }
        // �ƴ϶�� �ٽ� ���� �����ϵ��� ����
        else
        {
            isFishing = false;
            fishInfo.gameObject.SetActive(false);
        }
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
    }

    IEnumerator EffScale()
    {
        // ���������� ���� �� ����� �̹����� ũ�� ��ȭ��Ŵ
        Vector3 initialScale = new Vector3(1.1f, 1.1f, 1.1f);
        Vector3 targetScale = new Vector3(0.3f, 0.3f, 0.3f);
        float duration = 1f; // ũ�� ��ȭ�� �ɸ��� �ð�

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // �ð� ���� ���
            fishInfoImg.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }
        audioSource.PlayOneShot(SoundManager.instance.getFish, 1);
    }

    IEnumerator EffMove()
    {
        // ���������� ���� �� ����� �̹����� ����â���� �κ��丮�� �̵���Ŵ

        float elapsedTime = 0f;
        float duration = 1f; // �̵� �ð� (��)

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            fishInfoImg.transform.position = Vector3.Lerp(fishInfoImg.transform.position, inventoryBtn.transform.position, elapsedTime / duration);

            if(elapsedTime / duration >= 1)
            {
                fishInfoImg.transform.position = inventoryBtn.transform.position;
                fishInfoImg.gameObject.SetActive(false);
            }
            yield return null;
        }
    }

    // ����� ����
    public void Run()
    {
        StartCoroutine(FishRun());
    }

    // ����Ⱑ ������ ��� ���� �ؽ�Ʈ ���� �ٽ� ���� �غ�
    IEnumerator FishRun()
    {
        fishRun.gameObject.SetActive(true);
        isFishing = false;
        // ȭ�� ������ �ؽ�Ʈ ��Ȱ��ȭ
        yield return new WaitForSeconds(2f);
        fishRun.gameObject.SetActive(false);
    }

    // ���� ������Ʈ
    public void UIUpdate()
    {
        dateTxt.text = int.Parse(GameManager.instance.data.dateCount).ToString("N0");
        scoreTxt.text = int.Parse(GameManager.instance.data.score).ToString("N0");
        goldTxt.text = int.Parse(GameManager.instance.data.gold).ToString("N0");
        atkTxt.text = GameManager.instance.data.atk;
    }

    // �κ��丮 ����
    public void ViewInventory()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        // �κ��丮 Ȱ��ȭ
        inventoryImg.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
        // �κ��丮 ������ ���� ����������
        inventoryImg.transform.SetAsLastSibling();
    }

    // �κ��丮 �ݱ�
    public void EscInventory()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        inventoryFullImg.gameObject.SetActive(false);
        inventoryImg.gameObject.SetActive(false);
    }

    // �� �̵� ���
    public void EndSceneQuestionEsc()
    {
        // Ÿ�ӽ������� �ٽ� 1����
        Time.timeScale = 1;
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        endSceneQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    // ���� ������ ���� �ȳ�
    public void EndSceneQuestion()
    {
        // ���ø� ������ �������� ���� �ǳ�
        // Ÿ�ӽ������� 0����
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        Time.timeScale = 0;
        endSceneQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    // ���� ���� ��� 
    public void ExitGameQuestionEsc()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        exitGameQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    // ���� ���� �ȳ�
    public void ExitGameQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        exitGameQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    // ���� ���� ���
    public void DeleteDataQuestionEsc()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        deleteDataQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    // ���� ���� �ȳ�
    public void DeleteDataQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        deleteDataQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    // �α׾ƿ� ���
    public void LogOutQuestionEsc()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        endSceneQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    // �α׾ƿ� �ȳ�
    public void LogOutQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        logOutQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void ConfigBtn()
    {
        // ����â Ŭ�� ���ο� ���� ���� �ݰ� ����
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

    // �丮������
    public void GoCook()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        SceneManager.LoadScene(2);
    }

    // ���� ����
    public void ExitGame()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        Application.Quit();
    }

    public void LogOut()
    {
        // �α׾ƿ��� �ϰ� �Ǹ� �ٽ� �ʱ�ȭ������ 
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        GPGSBinder.Inst.Logout();
        logOutQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
        GameManager.instance.loginSuccess = false;
        GameManager.instance.nextStage = false;
        SceneManager.LoadScene(0);
    }
}
