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

    public AudioClip BGM;  // �� �� �� �������
    public AudioClip buttonClick; // ��ư Ŭ�� ����
    public AudioClip goldSound;  // ��� �߰� ����

    [Header("Story")]
    public AudioClip storyText; // �ؽ�Ʈ ��� ����

    [Header("Cook")]
    public AudioClip orderSuccess; // �ֹ�������� �ʹ��� ���� ��
    public AudioClip orderFail; // �ֹ�������� �ʹ��� ���� ������ ��
    public AudioClip dropSound;  // ��� �� ����
    public AudioClip stampSound;  // ���� ���� �� ���� ����

    [Header("Fishing")]
    public AudioClip reelIn; // �� ���� �Ҹ�
    public AudioClip getFish; // ����� ����� ��
    public AudioClip swing; // ������ ������ �Ҹ�
    public AudioClip throwBobber; // �� �������� �Ҹ�
    public AudioClip fish;

    [Header("UpGrade")]
    public AudioClip levelUp;  // ������, ���� �� Ŭ�� ����

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
