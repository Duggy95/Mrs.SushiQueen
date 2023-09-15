using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FishingManager : MonoBehaviour
{
    public Canvas canvas;
    public Text dateTxt;  // x일차
    public Text goldTxt;  // 보유 골드
    public Text atkTxt;   // 현재 공격력
    public Text fishInfo_Txt;  // 물고기 정보 텍스트
    // 아이템을 사용했음을 알리는 텍스트
    public Text useWhiteItemTxt;   
    public Text useRedItemTxt;
    public Text useRareItemTxt;
    public Text scoreTxt;  // 평점
    public Text touchTxt;  // 터치하라고 알리는 텍스트
    public GameObject full_Txt;  // 가득찼음을 알려주는 판넬
    public GameObject fishRun;   // 물고기가 도망갔음을 알리는 판넬
    public GameObject configPanel;   // 설정창
    public GameObject inventoryImg;  // 인벤토리 이미지
    public GameObject inventoryFullImg;   // 다른 ui들을 가리기 위한 투명이미지
    public GameObject blockFullImg;    // 다른 ui들을 가리기 위한 투명이미지
    public GameObject inventoryBtn;  // 인벤토리 버튼
    public GameObject fishInfoImg;   // 물고기 정보 이미지
    public GameObject fishContent; // 수족관
    public GameObject fishInfo; // 생선 정보 판넬
    public GameObject fishingRod; // 낚싯대 이미지
    public GameObject lineStartPos;  // 라인렌더러 시작점
    public GameObject useItemPanel;   // 아이템 사용했음을 알리는 텍스트 판넬
    public GameObject fishObj;    // 물고기 
    public GameObject endSceneQuestion;  // 게임 종료 안내
    public GameObject logOutQuestion;   // 로그 아웃 안내
    public GameObject deleteDataQuestion;  // 정보 삭제 안내
    public GameObject exitGameQuestion;  // 게임 종료 안내
    public GameObject giveupQuestion;   // 낚시 포기 안내
    public GameObject endScenePanel;  // 다음 씬으로 이동함을 알리는 판넬
    public Button fishingBtn;  // 물고기 생성 시 터치 가능한 범위
    public Button giveupBtn;  // 낚시 포기 버튼
    public Image fish_Img;   // 물고기 이미지
    public bool isFishing = false;  // 낚시 진행중인지 여부
    public bool useItem_white = false;  // 하얀 살 생선 확률 증가 아이템 사용 여부
    public bool useItem_red = false;    // 붉은 살 생선 확률 증가 아이템 사용 여부
    public bool useItem_rare = false;   // 레어 생선 확률 증가 아이템 사용 여부

    Vector3 fishInfoOriginPos;   // 물고기 정보창에서의 이미지 본 위치
    Vector3 fishInfoOriginScale;  //  물고기 정보창에서의 이미지 본 크기
    bool config;  // 설정창 열었는지 여부
    FishData data;  // 물고기 데이터 스크립터블
    AudioSource audioSource;  // 오디오 소스

    void Start()
    {
        Time.timeScale = 1;  // 게임 동작
        audioSource = GetComponent<AudioSource>();
        // 물고기 정보창에서의 물고기 이미지 크기와 위치 설정
        fishInfoOriginScale = Vector3.one;   
        fishInfoOriginPos = fishInfoImg.transform.position;
        // 각종 판넬과 텍스트 초기 설정
        useItemPanel.gameObject.SetActive(false);
        useRareItemTxt.gameObject.SetActive(false);
        useRedItemTxt.gameObject.SetActive(false);
        useWhiteItemTxt.gameObject.SetActive(false);
        inventoryImg.gameObject.SetActive(false);
        UIUpdate();  // 씬 이동과 동시에 정보 업데이트
    }

    // 정보를 삭제하고 반영 후 초기 씬으로 이동
    public void Delete()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        GameManager.instance.DeleteData();
        UIUpdate();
        GameManager.instance.nextStage = false;
        SceneManager.LoadScene(0);
    }

    // 낚시 가능 여부
    public void Fishing()
    {
        // 낚시 중이 아니라면 낚시 가능
        if (isFishing == false)
        {
            isFishing = true;
            // 낚싯줄 날리는 소리 출력
            audioSource.PlayOneShot(SoundManager.instance.swing, 1);
            // 클릭 위치를 매개변수로 하는 코루틴 함수 호출
            StartCoroutine(ThrowBobber(Input.mousePosition));
        }

        else
            return;
    }

    // 찌 생성
    IEnumerator ThrowBobber(Vector3 mousePos)
    {
        // 낚싯줄 날아가는 시간 반영
        yield return new WaitForSeconds(1);
        // 물고기 잡는 중으로 변경
        // 물고기 도망 텍스트 비활성화
        fishRun.gameObject.SetActive(false);

        LineRenderer fishLine = fishingRod.GetComponent<LineRenderer>();

        Vector3 startPos = lineStartPos.transform.position; // 시작 지점
        Vector3 endPos = Input.mousePosition; // 끝 지점

        // Line Renderer 속성 설정
        fishLine.SetPosition(0, startPos); // 라인의 점들 설정
        fishLine.SetPosition(1, endPos);

        // 클릭 위치에 물고기 생성
        GameObject _fishObj = Instantiate(fishObj, mousePos, Quaternion.identity);
        _fishObj.transform.SetParent(canvas.transform);
        _fishObj.transform.SetSiblingIndex(1);  //2번째 자식.
    }

    // 잡은 경우에 물고기 정보창 띄움
    public void Fish(FishData fishData)
    {
        StartCoroutine(StopTouch());
        
        // 잡은 물고기 데이터 받아서 처리
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

    // 낚시 성공 시 0.5초간 터치 불가
    IEnumerator StopTouch()
    {
        inventoryFullImg.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        inventoryFullImg.SetActive(false);
    }

    // 낚시 포기
    public void GiveUp()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        // 낚시 가능하도록 변경
        isFishing = false;
        giveupQuestion.gameObject.SetActive(false);
    }

    // 낚시 포기 안내
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

    // 팔기 버튼을 누르면 정보창 닫고 다시 낚시 준비
    public void Sell()
    {
        full_Txt.gameObject.SetActive(false);
        fishInfo.gameObject.SetActive(false);
        // 골드 정보 반영
        int _gold = int.Parse(GameManager.instance.data.gold) + data.gold;
        GameManager.instance.data.gold = _gold.ToString();
        GameManager.instance.todayData.gold += data.gold;
        //Debug.Log("골드 " + _gold);
        UIUpdate();
        audioSource.PlayOneShot(SoundManager.instance.levelUp, 1);
        isFishing = false;
    }

    // 수족관으로 버튼 누르면 정보창 닫고 다시 낚시 준비
    public void Get()
    {
        // 수족관에 이미지 추가
        Image[] _fishs = fishContent.gameObject.GetComponentsInChildren<Image>();

        bool isFull = true;
        bool isChange = false;
        // 수족관 내의 이미지 수만큼 반복하면서
        for (int i = 0; i < _fishs.Length; i++)
        {
            // 만약 이미지 컴포넌트의 이름이 Slot을 포함하고
            if (_fishs[i].gameObject.name.Contains("Slot"))
            {
                // 그 이미지의 자식 텍스트가 물고기 정보의 이름과 같다면
                if (_fishs[i].GetComponentInChildren<Text>().text.Contains(data.fishName))
                {
                    // 물고기 이름을 토대로 인덱스 구하기
                    string valueToFind = data.fishName;
                    int newValue = 1;

                    // 특정 값(valueToFind)을 만족하는 첫 번째 요소의 인덱스를 찾기
                    int index = GameManager.instance.inventory_Fishs.FindIndex(fish => fish.fish_Name == valueToFind);

                    // 찾았다면
                    if (index != -1)
                    {
                        // 수족관 내 물고기 수를 1 올려줌
                        newValue += int.Parse(GameManager.instance.inventory_Fishs[index].fish_Count);

                        // 오늘의 물고기 수확에도 추가
                        GameManager.instance.todayFishInfos.Add(new TodayFishInfo(data.fishName, 1));

                        // 해당 인덱스(index)의 값 변경
                        GameManager.instance.inventory_Fishs[index].fish_Count = newValue.ToString();
                        _fishs[i].GetComponentInChildren<Text>().text = data.fishName + "   " + newValue.ToString() + " " + "마리";
                        //Debug.Log("중복 종류 " + index + " changed to " + newValue);
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

        // 수족관 내에 중복된 물고기가 없는 경우
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

                    if (_slot.isEmpty == false)  // 슬롯이 비어있다면
                    {
                        // 물고기 정보 반영
                        Img.sprite = data.fishImg;
                        _slot.fish_ColorNum = data.color;
                        _slot.fish_GradeNum = data.grade;
                        _slot.fish_Name = data.fishName;

                        // 총 수량 반영
                        GameManager.instance.inventory_Fishs.Add(new InventoryFish(data.fishName, "1"));
                        // 오늘의 수량 반영
                        GameManager.instance.todayFishInfos.Add(new TodayFishInfo(data.fishName, 1));

                        _fishs[i].GetComponentInChildren<Text>().text = data.fishName + "   " + "1 마리";
                        //Debug.Log("안찼고 다른 종류");
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
        // 수족관이 가득찬 경우 안내문자 출력
        if (isFull)
        {
            full_Txt.gameObject.SetActive(true);
        }
        // 아니라면 다시 낚시 가능하도록 변경
        else
        {
            isFishing = false;
            fishInfo.gameObject.SetActive(false);
        }
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
    }

    IEnumerator EffScale()
    {
        // 수족관으로 보낼 때 물고기 이미지의 크기 변화시킴
        Vector3 initialScale = new Vector3(1.1f, 1.1f, 1.1f);
        Vector3 targetScale = new Vector3(0.3f, 0.3f, 0.3f);
        float duration = 1f; // 크기 변화에 걸리는 시간

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // 시간 비율 계산
            fishInfoImg.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }
        audioSource.PlayOneShot(SoundManager.instance.getFish, 1);
    }

    IEnumerator EffMove()
    {
        // 수족관으로 보낼 때 물고기 이미지를 정보창에서 인벤토리로 이동시킴

        float elapsedTime = 0f;
        float duration = 1f; // 이동 시간 (초)

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

    // 물고기 도망
    public void Run()
    {
        StartCoroutine(FishRun());
    }

    // 물고기가 도망간 경우 도망 텍스트 띄우고 다시 낚시 준비
    IEnumerator FishRun()
    {
        fishRun.gameObject.SetActive(true);
        isFishing = false;
        // 화면 누르면 텍스트 비활성화
        yield return new WaitForSeconds(2f);
        fishRun.gameObject.SetActive(false);
    }

    // 정보 업데이트
    public void UIUpdate()
    {
        dateTxt.text = int.Parse(GameManager.instance.data.dateCount).ToString("N0");
        scoreTxt.text = int.Parse(GameManager.instance.data.score).ToString("N0");
        goldTxt.text = int.Parse(GameManager.instance.data.gold).ToString("N0");
        atkTxt.text = GameManager.instance.data.atk;
    }

    // 인벤토리 열기
    public void ViewInventory()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        // 인벤토리 활성화
        inventoryImg.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
        // 인벤토리 순서를 제일 마지막으로
        inventoryImg.transform.SetAsLastSibling();
    }

    // 인벤토리 닫기
    public void EscInventory()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        inventoryFullImg.gameObject.SetActive(false);
        inventoryImg.gameObject.SetActive(false);
    }

    // 씬 이동 취소
    public void EndSceneQuestionEsc()
    {
        // 타임스케일을 다시 1으로
        Time.timeScale = 1;
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        endSceneQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    // 다음 씬으로 갈지 안내
    public void EndSceneQuestion()
    {
        // 낚시를 종료할 것인지를 묻는 판넬
        // 타임스케일을 0으로
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        Time.timeScale = 0;
        endSceneQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    // 게임 종료 취소 
    public void ExitGameQuestionEsc()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        exitGameQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    // 게임 종료 안내
    public void ExitGameQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        exitGameQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    // 정보 삭제 취소
    public void DeleteDataQuestionEsc()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        deleteDataQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    // 정보 삭제 안내
    public void DeleteDataQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        deleteDataQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    // 로그아웃 취소
    public void LogOutQuestionEsc()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        endSceneQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
    }

    // 로그아웃 안내
    public void LogOutQuestion()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        logOutQuestion.gameObject.SetActive(true);
        inventoryFullImg.gameObject.SetActive(true);
    }

    public void ConfigBtn()
    {
        // 설정창 클릭 여부에 따라 열고 닫고 실행
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

    // 요리씬으로
    public void GoCook()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        SceneManager.LoadScene(2);
    }

    // 게임 종료
    public void ExitGame()
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        Application.Quit();
    }

    public void LogOut()
    {
        // 로그아웃을 하게 되면 다시 초기화면으로 
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        GPGSBinder.Inst.Logout();
        logOutQuestion.gameObject.SetActive(false);
        inventoryFullImg.gameObject.SetActive(false);
        GameManager.instance.loginSuccess = false;
        GameManager.instance.nextStage = false;
        SceneManager.LoadScene(0);
    }
}
