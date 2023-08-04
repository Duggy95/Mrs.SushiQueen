using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private List<TutorialBase> tutorials;
    /*[SerializeField]
    private string nextSceneName = "Start";*/

    private TutorialBase currentTutorial = null;
    private int currentIndex = -1;

    public GameObject inventoryImg;
    public GameObject configPanel;
    public GameObject modeCanvas;
    public GameObject fishCanvas;
    public GameObject cookCanvas;
    public GameObject endCanvas;
    public GameObject fishingQuestion;
    public GameObject cookingQuestion;
    public GameObject endingQuestion;
    public GameObject skipQuestion;
    public GameObject exitQuestion;
    public GameObject nextDayQuestion;
    public GameObject noMoneyTxt;
    public GameObject inventoryFullImg;
    public CanvasGroup inventoryBtn;
    AudioSource audioSource;

    bool isInventory = false;
    bool config;
    public bool fishScene;

    void Start()
    {
        SetNextTutorial();
        print(tutorials.Count);
        modeCanvas.SetActive(true);
        fishCanvas.SetActive(false);
        cookCanvas.SetActive(false);
        endCanvas.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (currentTutorial != null)
        {
            currentTutorial.Execute(this);
        }
    }

    public void SetNextTutorial()
    {
        //���� Ʃ�丮���� Exit() �޼ҵ� ȣ��
        if (currentTutorial != null)
        {
            currentTutorial.Exit();
        }

        //������ Ʃ�丮���� �����ߴٸ� CompleteAllTutorials() �޼ҵ� ȣ��
        if (currentIndex >= tutorials.Count - 1)
        {
            CompletedAllTutorials();
            return;
        }

        //���� Ʃ�丮�� ������ currentTutorial�� ���
        currentIndex++;
        currentTutorial = tutorials[currentIndex];
        if(currentIndex >= 13 && currentIndex <= 26)
        {
            inventoryBtn.interactable = false;
        }
        else if(currentIndex >= 27)
        {
            inventoryBtn.interactable= true;
        }

        //���� �ٲ� Ʃ�丮���� Enter() �޼ҵ� ȣ��
        currentTutorial.Enter();
    }

    public void CompletedAllTutorials()
    {
        currentTutorial = null;
        tutorials.Clear();
        //�ൿ ����� ���� ������ �Ǿ��� �� �ڵ� �߰� �ۼ�
        //����� �� ��ȯ.
        Debug.Log("Complete All");

        print("�� �̵�");
        GameManager.instance.nextStage = true;
        SceneManager.LoadScene(0);
        Destroy(this);
    }

    public void ShowFishScene()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        fishCanvas.SetActive(true);
        fishScene = true;
    }

    public void FishingQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        fishingQuestion.gameObject.SetActive(true);
    }

    public void EscFishingQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        fishingQuestion.gameObject.SetActive(false);
    }

    public void ShowCookScene()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        cookCanvas.SetActive(true);
    }

    public void CookingQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        cookingQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void EscCookingQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        cookingQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void ShowEndScene()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        endCanvas.SetActive(true);
    }

    public void EndingQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        endingQuestion.gameObject.SetActive(true);
    }

    public void EscEndingQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        endingQuestion.gameObject.SetActive(false);
    }

    public void ConfigBtn() //���������ֱ�
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        if (!config)
        {
            configPanel.SetActive(true);
            config = true;
            Time.timeScale = 0;
        }
        else
        {
            configPanel.SetActive(false);
            config = false;
            Time.timeScale = 1;
        }
    }

    public void ExitQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        exitQuestion.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        Application.Quit();
    }

    public void EscExitQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        exitQuestion.gameObject.SetActive(false);
    }

    public void SkipBtn()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        skipQuestion.SetActive(true);
    }

    public void CloseSkipBtn()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        skipQuestion.SetActive(false);
    }

    public void GoSkipBtn()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        CompletedAllTutorials();
    }

    public void NextDayBtn()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        CompletedAllTutorials();
    }

    public void NextDayQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        nextDayQuestion.gameObject.SetActive(true);
    }

    public void EscNextDayQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        nextDayQuestion.gameObject.SetActive(false);
    }

    public void InventoryBtn() //�κ��丮 Ȱ��ȭ, ��Ȱ��ȭ
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        if (!isInventory)
        {
            inventoryImg.gameObject.SetActive(true);
            inventoryFullImg.gameObject.SetActive(true);
            isInventory = true;
        }
        else
        {
            inventoryImg.gameObject.SetActive(false);
            inventoryFullImg.gameObject.SetActive(false);
            isInventory = false;
        }
    }

    public void NoMoneyBtn()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
        StartCoroutine(NoMoney());
    }

    public IEnumerator NoMoney()
    {
        noMoneyTxt.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        noMoneyTxt.gameObject.SetActive(false);
    }
}
