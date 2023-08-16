using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CookManager : MonoBehaviour
{
    public CanvasGroup inventoryCanvas;  //인벤토리
    public GameObject customerPrefab;  //손님 프리팹
    public GameObject orderView;  //주문화면
    public GameObject cookView;  //요리화면
    public GameObject configPanel;  //설정 창
    public GameObject readyBtn;  //준비완료 버튼
    public GameObject InventoryImg;  //인벤토리
    public GameObject inventoryFullImg;  //인벤토리 켰을때 주변가리는 이미지
    public GameObject blockFullImg;  //가리는 이미지
    public GameObject fishIconPrefab;  //생선 아이콘 프리팹
    public GameObject[] customers;  //손님들 배열
    public GameObject orderFishContent;  //주문창에 생선패널

    public Transform fishScroll;  //회버튼 스크롤
    NetaButton[] fishSlots;  //회버튼 구성요소 불러오기위함
    public Transform dish;  //접시
    public Text dateTxt;  //날짜 + 평판
    public Text goldTxt;  //골드
    public Text atkTxt;  //골드
    public Text orderTxt;  //주문 텍스트
    public Text scoreTxt;  //점수 텍스트
    public Text priceTxt;  //포스기 가격 텍스트
    public List<string> fishList = new List<string>();  //드래그 드롭에서 중복확인을 위한 생선리스트
    public int fishBtnCount = 0;
    public bool canMake = false;  //만들기 가능한지 아닌지
    public bool isReady = false;  //준비완료인지 아닌지
    public bool isEnd = false;  //끝났는지 아닌지
    public Vector2 customerStartPos;  //손님 FadeIn 시작 포즈

    AudioSource audioSource;  //오디오 소스
    WaitForSeconds ws;  //코루틴 시간
    Vector2 customerTr = Vector2.zero;  //손님 생성 위치
    int count = 0;
    bool config = false;

    [Header("Window")]
    public GameObject endSceneQuestion;  //다음 씬 질문
    public GameObject logOutQuestion;  //로그아웃
    public GameObject deleteDataQuestion;  //데이터 지우기
    public GameObject exitGameQuestion;  //종료하기
    public GameObject endScenePanel;  //다음 씬 이동
    public GameObject gameOverView;  //게임오버 창
    public GameObject restaurant;  //식당 이미지
    public GameObject stamp;  //도장
    public GameObject gameOverTxt;  //게임오버 텍스트
    public GameObject restartBtn;  //재시작 버튼

    void Start()
    {
        Time.timeScale = 1;  //시작시 timeScale 초기화
        audioSource = GetComponent<AudioSource>();
        customerStartPos = new Vector2(-450, -100);  //손님 위치

        //시작 세팅 화면 세팅
        orderView.SetActive(true);
        cookView.SetActive(true);
        
        ws = new WaitForSeconds(2);

        UIUpdate();  //UI 최신화
        priceTxt.text = "0";  //포스기 가격 텍스트

        fishSlots = fishScroll.gameObject.GetComponentsInChildren<NetaButton>();  //생선버튼이 가지고 있는 요소들 가져오기
    }

    public void GoEndScene()  //운영씬으로
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        SceneManager.LoadScene(3);
    }

    public void Delete()  //데이터 지우기
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        GameManager.instance.DeleteData();
        UIUpdate();
        //ExitGame();
        GameManager.instance.nextStage = false;  //시작화면에서 스토리+튜토리얼 보게하기 위함.
        SceneManager.LoadScene(0);
    }

    public void GameOver()  //게임 오버
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

    public void ViewInventory() //인벤토리 활성화
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        inventoryFullImg.gameObject.SetActive(true);
        InventoryImg.gameObject.SetActive(true);
    }

    public void EscInventory() //인벤토리 나가기
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        inventoryFullImg.gameObject.SetActive(false);
        logOutQuestion.gameObject.SetActive(false);
    }

    public void EndSceneQuestionEsc()  //다음 씬 질문나가기
    {
        Time.timeScale = 1;
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        endSceneQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);

    }

    public void EndSceneQuestion()  //다음 씬 질문
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        Time.timeScale = 0;
        endSceneQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void ExitGameQuestionEsc()  //게임종료 질문 나가기
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        exitGameQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void ExitGameQuestion()  //게임종료 질문
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        exitGameQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void ExitGame()  //게임종료하기
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        Application.Quit();
    }

    public void DeleteDataQuestionEsc()  //데이터 지우기 질문 나가기
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        deleteDataQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void DeleteDataQuestion()  //데이터 지우기 질문
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        deleteDataQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void LogOutQuestionEsc()  //로그아웃 질문 나가기
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        endSceneQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    public void LogOutQuestion()  //로그아웃 질문
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        logOutQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void LogOut()  //로그아웃
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        GPGSBinder.Inst.Logout();
        logOutQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
        GameManager.instance.loginSuccess = false;
        GameManager.instance.nextStage = false;
        SceneManager.LoadScene(0);
    }

    public void ConfigBtn() //설정보여주기
    {
        if (!config)  //꺼져있을 때 키기
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

            configPanel.SetActive(true);
            inventoryFullImg.gameObject.SetActive(true);
            config = true;
        }
        else  //켜져있을때 끄기
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

            configPanel.SetActive(false);
            inventoryFullImg.gameObject.SetActive(false);
            config = false;
        }
    }

    public void ViewOrder()  //주문화면 요리화면 전환 메서드
    {
        if (canMake)  //만들 수 있을 때 = 주문 받았을 때만 화면전화 가능
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
                dish.transform.SetSiblingIndex(1);  //2번째 자식.
            }
        }
    }

    public void GoOrder()  //주문 창으로 가기
    {
        cookView.SetActive(false);
        dish.transform.SetParent(orderView.transform);
    }

    public IEnumerator Create()  //손님 생성 메서드
    {
        if (!isEnd)  //게임이 안끝났을 때만
        {
            //랜덤한 손님
            int random = Random.Range(0, customers.Length);
            //2초 후 손님 생성.
            yield return ws;
            GameObject customer = Instantiate(customers[random], customerTr,
                                                                    Quaternion.identity, orderView.transform);
            customer.transform.SetSiblingIndex(1);  //2번째 자식.
        }
    }

    public void ReadyBtn()  //준비완료 버튼
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        isReady = true;  //준비완료
        StartCoroutine(Create());  //손님 생성
        readyBtn.SetActive(false);
        orderView.SetActive(true);
        cookView.SetActive(false);
        InventoryImg.gameObject.SetActive(false);
        inventoryCanvas.interactable = false;  //인벤토리 안눌리게 끄기
        dish.transform.SetParent(orderView.transform);  //접시 주문화면에서 보이게 하기
        dish.transform.SetSiblingIndex(2);  //2번째 자식.

        //준비완료하면 빈 칸인 생선버튼들 삭제하기
        for (int i = 0; i < fishSlots.Length; i++)
        {
            if (fishSlots[i].fishData == null)
            {
                Destroy(fishSlots[i].gameObject);
            }
        }
    }

    public void Order(string txt)  //요리화면에서 주문내용 보이게끔 하는 메서드
    {
        orderTxt.text = txt;
    }

    public IEnumerator GameOverCoroutine()  //게임오버 코루틴
    {
        //1초뒤 게임종료 화면띄우고 레스토랑 이미지 아래에서 위로 천천히 올라오게 하기
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
            float t = Mathf.Clamp01(elapsedTime / duration); // 시간 비율 계산
            restaurant.transform.localPosition = Vector2.Lerp(initPos, targetPos, t);
            yield return null;
        }

        //도장마크 보이게 하기
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
        print("코루틴 호출");
    }

    private void OnDisable()
    {
        fishList.Clear();  //생선 리스트 클리어.
    }
}
