using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get 
        {
            if (m_instance == null)
            {
                m_instance = FindAnyObjectByType<UIManager>();
            }
            return m_instance;
        }
    }
    static UIManager m_instance;

    public Camera homeMainCam;  // 홈씬의 메인카메라
    public Camera homeStoryCam;  // 홈씬의 스토리카메라
    public Camera homeModeCam;  // 홈씬의 모드선택카메라
    public Camera orderCam;  // 장사씬의 주문 화면
    public Camera cookCam;  // 장사씬의 요리화면

    public Button homeBtn;  // 홈으로 가는 버튼
    public Button inventoryBtn;  // 인벤토리 여는 버튼
    public Button AquariumBtn;  // 수족관 여는 버튼
    public Button sellBtn;  // 판매하기 버튼
    public Button skipBtn;  // 스토리 스킵 버튼
    public Button startFishingBtn;  // 낚시씬으로 가는 버튼
    public Button startBusinessBtn;  // 장사씬으로 가는 버튼
    public Button stopFishingBtn;  // 낚시씬 종료 >> 장사씬으로 가는 버튼
    public Button stopBusinessBtn;  // 장사씬 종료 >> 운영씬으로 가는 버튼
    public Button startManagementBtn;  // 운영씬으로 가는 버튼
    public Button endStageBtn;  // 스테이지 종료 >> 다음 날짜로 가는 버튼 
    public Button acceptBtn;  //  주문 수락 버튼
    public Button refuseBtn;  // 주문 거절 버튼
    public Button riceBtn;  // 밥 버튼
    public Button wasabiBtn;  // 와사비 버튼
    public Button[] sushiImg;  // 초밥 버튼 (배열 아니면 enum?)
    public Text[] storyTxt;  // 스토리 텍스트 (스토리가 여러장? 이면 배열 그대로)
    public Text gameStartTxt;  // 메인화면에서 게임 시작을 안내하는 텍스트 (효과 줄 것)
    public Text dateTxt;  // 날짜와 평판점수를 기록할 텍스트
    public Text goldTxt;  // 소지 금액을 나타낵 텍스트
    public Text fishingStartTxt;  // 물고기를 잡으라고 안내하는 텍스트 (효과 줄 것)
    public Text fishRunTxt;  // 물고기가 도망갔을 때 나올 텍스트;
    public Text getFishTxt;  // 물고기 잡았을 때 나올 텍스트
    public Image getFishImg;  // 물고기 잡았을 때 나올 이미지
    public Image clientImg;  // 손님 이미지
    public Image[] fishImg;  // 물고기 이미지 (배열 아니면 enum?)
    public Scrollbar timer;  // 타이머
    public Scrollbar fishHealth;  // 물고기 체력바
    public Scrollbar clientHealth;  // 손님 체력바
    
    private void Awake()
    {
        if (instance != this) // 싱글톤된 게 자신이 아니라면 삭제
        {
            Destroy(gameObject);
        }
    }

    public void OnclickSkip()
    {

    }

    public void GoHomeScene()
    {
        SceneManager.LoadScene(0);
    }

    public void GoFishingScene()
    {
        SceneManager.LoadScene(1);
    }

    public void GoBusinessScene() 
    {
        SceneManager.LoadScene(2);
    }

    public void GoManagementScene()
    {
        SceneManager.LoadScene(3);
    }
}
