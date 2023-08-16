using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindAnyObjectByType<SoundManager>();
            }
            return m_instance;
        }
    }

    static SoundManager m_instance;

    public AudioClip BGM;  // 각 씬 별 배경음악
    public AudioClip buttonClick; // 버튼 클릭 사운드
    public AudioClip goldSound;  // 골드 추가 사운드

    [Header("Story")]
    public AudioClip storyText; // 텍스트 출력 사운드

    [Header("Cook")]
    public AudioClip orderSuccess; // 주문받은대로 초밥을 줬을 때
    public AudioClip orderFail; // 주문받은대로 초밥을 주지 못했을 때
    public AudioClip dropSound;  // 드랍 후 사운드
    public AudioClip stampSound;  // 게임 오버 시 도장 사운드

    [Header("Fishing")]
    public AudioClip reelIn; // 릴 감는 소리
    public AudioClip getFish; // 물고기 잡았을 때
    public AudioClip swing; // 낚싯줄 던지는 소리
    public AudioClip throwBobber; // 찌 떨어지는 소리
    public AudioClip fish;

    [Header("UpGrade")]
    public AudioClip levelUp;  // 레벨업, 구매 등 클릭 사운드

    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = BGM;
        audioSource.mute = false;
        audioSource.loop = true;
        audioSource.volume = 0.75f;
        audioSource.Play();
    }
}
