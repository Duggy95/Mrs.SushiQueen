using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private List<TutorialBase> tutorials;
    [SerializeField]
    private string nextSceneName = "";

    private TutorialBase currentTutorial = null;
    private int currentIndex = -1;

    public GameObject inventoryImg;
    public GameObject configPanel;
    public GameObject modeCanvas;
    public GameObject fishCanvas;
    public GameObject cookCanvas;
    public GameObject endCanvas;

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
        if(currentTutorial != null)
        {
            currentTutorial.Execute(this);
        }
    }

    public void SetNextTutorial()
    {
        //���� Ʃ�丮���� Exit() �޼ҵ� ȣ��
        if(currentTutorial != null)
        {
            currentTutorial.Exit();
        }

        //������ Ʃ�丮���� �����ߴٸ� CompleteAllTutorials() �޼ҵ� ȣ��
        if(currentIndex >= tutorials.Count-1)
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

        //�ൿ ����� ���� ������ �Ǿ��� �� �ڵ� �߰� �ۼ�
        //����� �� ��ȯ.
        Debug.Log("Complete All");

        if(!nextSceneName.Equals(""))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    public void ShowFishScene()
    {
        fishCanvas.SetActive(true);
        fishScene = true;
    }

    public void ShowOrderView()
    {
        cookCanvas.SetActive(true);
    }

    public void ShowEndScene()
    {
        endCanvas.SetActive(true);
    }

    public void ConfigBtn() //���������ֱ�
    {
        if (!config)
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

    public void ViewInventory() //�κ��丮 Ȱ��ȭ
    {
        inventoryImg.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
