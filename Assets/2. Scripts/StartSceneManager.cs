using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public string[] story;  //스토리 내용
    public Text startTxt;  //터치해서 게임시작 텍스트
    public Text storyTxt;  //스토리 텍스트
    public Text dateTxt;  //날짜 + 평판 텍스트
    public Text scoreTxt; // 평판
    public Text goldTxt;  //골드 텍스트
    public Text atkTxt;  // 공격력
    public Text touchTxt;  // 터치하여 게임 시작하라는 텍스트
    public GameObject loginSuccessPanel;  // 로그인 성공시 출력되는 판넬
    public GameObject inventoryImg; //인벤토리 이미지
    public GameObject inventoryFullImg;  // 인벤토리 열었을 때 다른 버튼 작동 못하게 막는 이미지
    public GameObject _storyManager;  // 스토리 매니저
    public GameObject configPanel;  // 설정창
    public GameObject mainObj;  // 초기 화면
    public GameObject storyObj;  // 스토리 화면
    public GameObject modeObj;  // 모드 선택 화면
    public GameObject loginObj;  // 로그인 버튼
    public GameObject fishingQuestion;  // 낚시하러 갈 것인지 묻는 판넬
    public GameObject cookQuestion;  // 요리하러 갈 것인지 묻는 판넬
    public GameObject logOutQuestion; // 로그아웃 안내
    public GameObject deleteDataQuestion;  // 정보 삭제 안내
    public GameObject exitGameQuestion;  // 게임 종료 안내
    public GameObject impossibleTxt;  // 물고기 없는 경우 요리씬 이동 불가 안내
    public GameObject startPanel;  // 게임 시작을 알리는 판넬
    public GameObject saveDataPanel;  // 정보 저장을 알리는 판넬
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
            // 게임 시작 시
            if (!GameManager.instance.nextStage) //
            {
                if (!GameManager.instance.loginSuccess)
                {
                    loginObj.gameObject.SetActive(true);
                }
                touchTxt.gameObject.SetActive(false);
                loginSuccessPanel.SetActive(false);
                blackCanvas.gameObject.SetActive(true);
                // 게임 입장 효과
                StartCoroutine(FadeAway());
                mainObj.gameObject.SetActive(true);
                storyObj.gameObject.SetActive(false);
                modeObj.gameObject.SetActive(false);
                _storyManager.gameObject.SetActive(false);
            }
            // 게임 진행 중에 현재 씬 입장 시
            else //
            {
                blackCanvas.gameObject.SetActive(false);
                mainObj.gameObject.SetActive(false);
                storyObj.gameObject.SetActive(false);
                modeObj.gameObject.SetActive(true);
                _storyManager.gameObject.SetActive(false);
                UIUpdate();
                // 정보 저장 및 하루 수확 정보 초기화
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
        // 로그인 성공 시 로그인 버튼 비활성화, 로그인 성공 판넬과 터치 안내 텍스트 활성화
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

    // 로그인
    public void LoginBtn()
    {
        GameManager.instance.LogIn();
    }

    // 화면 터치 시 게임 준비시작
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

        // 신규일 경우
        if (int.Parse(GameManager.instance.data.dateCount) <= 0)
        {
            mainObj.gameObject.SetActive(false);
            storyObj.gameObject.SetActive(true);
            _storyManager.gameObject.SetActive(true);
            modeObj.gameObject.SetActive(false);
        }
        else
        {
            // 초기엔 정보 불러오기 위해 5초 뒤 시작
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
        // 인벤토리 활성화
        inventoryImg.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
        print("인벤토리 열려라");
    }

    public void EscInventory()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        inventoryImg.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    // 스토리 스킵
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
