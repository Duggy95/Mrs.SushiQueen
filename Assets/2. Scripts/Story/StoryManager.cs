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
        print("스토리 시작");
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
        //현재 튜토리얼의 Exit() 메소드 호출
        if (currentStory != null)
        {
            currentStory.Exit();
        }

        //마지막 튜토리얼을 진행했다면 CompleteAllTutorials() 메소드 호출
        if (currentIndex >= stories.Count - 1)
        {
            CompletedAllStories();
            return;
        }

        //다음 튜토리얼 과정을 currentTutorial로 등록
        currentIndex++;
        currentStory = stories[currentIndex];
        background.sprite = sprites[currentIndex];

        //새로 바뀐 튜토리얼의 Enter() 메소드 호출
        currentStory.Enter();
    }

    public void CompletedAllStories()
    {
        currentStory = null;

        //행동 양식이 여러 종류가 되었을 때 코드 추가 작성
        //현재는 씬 전환.
        Debug.Log("Complete All");
        SceneManager.LoadScene(4);
    }
}
