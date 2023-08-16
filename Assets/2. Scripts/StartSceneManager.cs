using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public string[] story;  //���丮 ����
    public Text startTxt;  //��ġ�ؼ� ���ӽ��� �ؽ�Ʈ
    public Text storyTxt;  //���丮 �ؽ�Ʈ
    public Text dateTxt;  //��¥ + ���� �ؽ�Ʈ
    public Text scoreTxt; // ����
    public Text goldTxt;  //��� �ؽ�Ʈ
    public Text atkTxt;  // ���ݷ�
    public Text touchTxt;  // ��ġ�Ͽ� ���� �����϶�� �ؽ�Ʈ
    public GameObject loginSuccessPanel;  // �α��� ������ ��µǴ� �ǳ�
    public GameObject inventoryImg; //�κ��丮 �̹���
    public GameObject inventoryFullImg;  // �κ��丮 ������ �� �ٸ� ��ư �۵� ���ϰ� ���� �̹���
    public GameObject _storyManager;  // ���丮 �Ŵ���
    public GameObject configPanel;  // ����â
    public GameObject mainObj;  // �ʱ� ȭ��
    public GameObject storyObj;  // ���丮 ȭ��
    public GameObject modeObj;  // ��� ���� ȭ��
    public GameObject loginObj;  // �α��� ��ư
    public GameObject fishingQuestion;  // �����Ϸ� �� ������ ���� �ǳ�
    public GameObject cookQuestion;  // �丮�Ϸ� �� ������ ���� �ǳ�
    public GameObject logOutQuestion; // �α׾ƿ� �ȳ�
    public GameObject deleteDataQuestion;  // ���� ���� �ȳ�
    public GameObject exitGameQuestion;  // ���� ���� �ȳ�
    public GameObject impossibleTxt;  // ����� ���� ��� �丮�� �̵� �Ұ� �ȳ�
    public GameObject startPanel;  // ���� ������ �˸��� �ǳ�
    public GameObject saveDataPanel;  // ���� ������ �˸��� �ǳ�
    public StoryManager storyManager;
    public CanvasGroup blackCanvas;

    AudioSource audioSource;
    bool config;

    private void Awake()
    {
        Time.timeScale = 1;
        audioSource = GetComponent<AudioSource>();
        saveDataPanel.gameObject.SetActive(false);
        startPanel.gameObject.SetActive(false);

        if (GameManager.instance != null)
        {
            // ���� ���� ��
            if (!GameManager.instance.nextStage) //
            {
                if (!GameManager.instance.loginSuccess)
                {
                    loginObj.gameObject.SetActive(true);
                }
                touchTxt.gameObject.SetActive(false);
                loginSuccessPanel.SetActive(false);
                blackCanvas.gameObject.SetActive(true);
                // ���� ���� ȿ��
                StartCoroutine(FadeAway());
                mainObj.gameObject.SetActive(true);
                storyObj.gameObject.SetActive(false);
                modeObj.gameObject.SetActive(false);
                _storyManager.gameObject.SetActive(false);
            }
            // ���� ���� �߿� ���� �� ���� ��
            else //
            {
                blackCanvas.gameObject.SetActive(false);
                mainObj.gameObject.SetActive(false);
                storyObj.gameObject.SetActive(false);
                modeObj.gameObject.SetActive(true);
                _storyManager.gameObject.SetActive(false);
                UIUpdate();
                // ���� ���� �� �Ϸ� ��Ȯ ���� �ʱ�ȭ
                GameManager.instance.Save("d");
                GameManager.instance.Save("i");
                GameManager.instance.Save("f");
                GameManager.instance.todayData = new TodayData();
                GameManager.instance.todayFishInfos.Clear();
            }
        }
    }

    private void Update()
    {
        // �α��� ���� �� �α��� ��ư ��Ȱ��ȭ, �α��� ���� �ǳڰ� ��ġ �ȳ� �ؽ�Ʈ Ȱ��ȭ
        if (GameManager.instance.loginSuccess)
        {
            loginObj.gameObject.SetActive(false);
            loginSuccessPanel.gameObject.SetActive(true);
            touchTxt.gameObject.SetActive(true);
        }
       
        else if (!GameManager.instance.loginSuccess)
        {
            loginObj.gameObject.SetActive(true);
            loginSuccessPanel.gameObject.SetActive(false);
            touchTxt.gameObject.SetActive(false);
        }
    }

    // �α���
    public void LoginBtn()
    {
        GameManager.instance.LogIn();
    }

    // ȭ�� ��ġ �� ���� �غ����
    public void GameReady()
    {
        print("login : " + GameManager.instance.loginSuccess);
        if (GameManager.instance.loginSuccess)
        {
            startPanel.gameObject.SetActive(true);
            StartCoroutine(GameStart());
        }
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1f);

        // �ű��� ���
        if (int.Parse(GameManager.instance.data.dateCount) <= 0)
        {
            mainObj.gameObject.SetActive(false);
            storyObj.gameObject.SetActive(true);
            _storyManager.gameObject.SetActive(true);
            modeObj.gameObject.SetActive(false);
        }
        else
        {
            // �ʱ⿣ ���� �ҷ����� ���� 5�� �� ����
            yield return new WaitForSeconds(5f);

            _storyManager.gameObject.SetActive(false);
            mainObj.gameObject.SetActive(false);
            storyObj.gameObject.SetActive(false);
            modeObj.gameObject.SetActive(true);
            UIUpdate();
        }
    }

    public void UIUpdate()
    {
        dateTxt.text = int.Parse(GameManager.instance.data.dateCount).ToString("N0");
        scoreTxt.text = int.Parse(GameManager.instance.data.score).ToString("N0");
        goldTxt.text = int.Parse(GameManager.instance.data.gold).ToString("N0");
        atkTxt.text = GameManager.instance.data.atk;
    }

    public void ViewInventory()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        // �κ��丮 Ȱ��ȭ
        inventoryImg.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
        print("�κ��丮 ������");
    }

    public void EscInventory()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        inventoryImg.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    // ���丮 ��ŵ
    public void OnClickSkip()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        if (int.Parse(GameManager.instance.data.dateCount) >= 1)
        {
            _storyManager.gameObject.SetActive(false);
            mainObj.gameObject.SetActive(false);
            storyObj.gameObject.SetActive(false);
            modeObj.gameObject.SetActive(true);
            UIUpdate();
        }
        else
        {
            SceneManager.LoadScene(4);
        }
    }

    public void FishingQuestionEsc()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        fishingQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void FishingQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        fishingQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    IEnumerator Impossible()
    {
        impossibleTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        impossibleTxt.gameObject.SetActive(false);
    }

    public void CookQuestionEsc()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        cookQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void CookQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        if (GameManager.instance.inventory_Fishs.Count <= 0)
        {
            StartCoroutine(Impossible());
        }
        else
        {
            cookQuestion.gameObject.SetActive(true);
            inventoryFullImg.gameObject.SetActive(true);
        }
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

        logOutQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void LogOutQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        logOutQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void GoFishing()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        SceneManager.LoadScene(1);
    }

    public void GoShop()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        SceneManager.LoadScene(2);
    }

    public void ConfigBtn()
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

    public void ExitGame()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        StartCoroutine(EixtReady());
    }

    IEnumerator EixtReady()
    {
        saveDataPanel.SetActive(true);
        exitGameQuestion.SetActive(false);

        yield return new WaitForSeconds(5);

        Application.Quit();
    }

    public void LogOut()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        GPGSBinder.Inst.Logout();
        logOutQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
        GameManager.instance.loginSuccess = false;
        GameManager.instance.nextStage = false;
        SceneManager.LoadScene(0);
    }

    public void Delete()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        GameManager.instance.DeleteData();
        UIUpdate();
        GameManager.instance.nextStage = false;
        SceneManager.LoadScene(0);
    }

    IEnumerator FadeAway()
    {
        blackCanvas.alpha = 1;
        blackCanvas.blocksRaycasts = true;

        yield return new WaitForSeconds(1);

        float duration = 1f;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float alpha = 1 - t;
            blackCanvas.alpha = alpha;
            yield return null;
        }

        blackCanvas.blocksRaycasts = false;
    }
}
