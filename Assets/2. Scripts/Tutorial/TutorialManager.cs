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
        fishCanvas.SetActive(true);
        fishScene = true;
    }

    public void FishingQuestion()
    {
        fishingQuestion.gameObject.SetActive(true);
    }

    public void EscFishingQuestion()
    {
        fishingQuestion.gameObject.SetActive(false);
    }

    public void ShowCookScene()
    {
        cookCanvas.SetActive(true);
    }

    public void CookingQuestion()
    {
        cookingQuestion.gameObject.SetActive(true);
    }

    public void EscCookingQuestion()
    {
        cookingQuestion.gameObject.SetActive(false);
    }

    public void ShowEndScene()
    {
        endCanvas.SetActive(true);
    }

    public void EndingQuestion()
    {
        endingQuestion.gameObject.SetActive(true);
    }

    public void EscEndingQuestion()
    {
        endingQuestion.gameObject.SetActive(false);
    }

    public void ConfigBtn() //���������ֱ�
    {
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
        exitQuestion.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void EscExitQuestion()
    {
        exitQuestion.gameObject.SetActive(false);
    }

    public void SkipBtn()
    {
        skipQuestion.SetActive(true);
    }

    public void CloseSkipBtn()
    {
        skipQuestion.SetActive(false);
    }

    public void GoSkipBtn()
    {
        CompletedAllTutorials();
    }

    public void NextDayBtn()
    {
        CompletedAllTutorials();
    }

    public void NextDayQuestion()
    {
        nextDayQuestion.gameObject.SetActive(true);
    }

    public void EscNextDayQuestion()
    {
        nextDayQuestion.gameObject.SetActive(false);
    }

    public void InventoryBtn() //�κ��丮 Ȱ��ȭ, ��Ȱ��ȭ
    {
        if (!isInventory)
        {
            inventoryImg.gameObject.SetActive(true);
            isInventory = true;
        }
        else
        {
            inventoryImg.gameObject.SetActive(false);
            isInventory = false;
        }
    }

    public void NoMoneyBtn()
    {
        StartCoroutine(NoMoney());
    }

    public IEnumerator NoMoney()
    {
        noMoneyTxt.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        noMoneyTxt.gameObject.SetActive(false);
    }
}
