using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    [SerializeField]
    private List<StoryBase> stories;
    public GameObject modeView;
    public GameObject storyView;

    private StoryBase currentStory = null;
    public int currentIndex = -1;

    public Sprite[] sprites;
    public Image background;

    private void Start()
    {
        SetNextStory();
        print("���丮 ����");
    }

    private void Update()
    {
        if (currentStory != null)
        {
            currentStory.Execute(this);
        }
    }

    public void SetNextStory()
    {
        //���� Ʃ�丮���� Exit() �޼ҵ� ȣ��
        if (currentStory != null)
        {
            currentStory.Exit();
        }

        //������ Ʃ�丮���� �����ߴٸ� CompleteAllTutorials() �޼ҵ� ȣ��
        if (currentIndex >= stories.Count - 1)
        {
            CompletedAllStories();
            return;
        }

        //���� Ʃ�丮�� ������ currentTutorial�� ���
        currentIndex++;
        currentStory = stories[currentIndex];
        background.sprite = sprites[currentIndex];

        //���� �ٲ� Ʃ�丮���� Enter() �޼ҵ� ȣ��
        currentStory.Enter();
    }

    public void CompletedAllStories()
    {
        currentStory = null;

        //�ൿ ����� ���� ������ �Ǿ��� �� �ڵ� �߰� �ۼ�
        //����� �� ��ȯ.
        Debug.Log("Complete All");
        SceneManager.LoadScene(4);
    }
}
