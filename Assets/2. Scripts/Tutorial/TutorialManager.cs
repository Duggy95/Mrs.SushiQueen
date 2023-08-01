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
    public GameObject skipQuestion;

    bool isInventory = false;
    bool config;
    public bool fishScene;

    void Start()
    {
        SetNextTutorial();

        modeCanvas.SetActive(true);
        fishCanvas.SetActive(false);
        cookCanvas.SetActive(false);
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
        //현재 튜토리얼의 Exit() 메소드 호출
        if (currentTutorial != null)
        {
            currentTutorial.Exit();
        }

        //마지막 튜토리얼을 진행했다면 CompleteAllTutorials() 메소드 호출
        if (currentIndex >= tutorials.Count - 1)
        {
            CompletedAllTutorials();
            return;
        }

        //다음 튜토리얼 과정을 currentTutorial로 등록
        currentIndex++;
        currentTutorial = tutorials[currentIndex];

        //새로 바뀐 튜토리얼의 Enter() 메소드 호출
        currentTutorial.Enter();
    }

    public void CompletedAllTutorials()
    {
        currentTutorial = null;

        //행동 양식이 여러 종류가 되었을 때 코드 추가 작성
        //현재는 씬 전환.
        Debug.Log("Complete All");

        print("씬 이동");
        SceneManager.LoadScene(0);
        GameManager.instance.nextStage = true;
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

    public void ShowOrderView()
    {
        cookCanvas.SetActive(true);
    }

    public void ShowEndScene()
    {
        endCanvas.SetActive(true);
    }

    public void ConfigBtn() //설정보여주기
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

    public void InventoryBtn() //인벤토리 활성화, 비활성화
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

    public void ExitGame()
    {
        Application.Quit();
    }
}
