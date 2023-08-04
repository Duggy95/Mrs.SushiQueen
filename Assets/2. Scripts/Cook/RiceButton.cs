using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceButton : MonoBehaviour
{
    public GameObject ricePrefab;  //�� ������. 
    public Transform board;  //����

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void RiceBtn()  //�� ���� �޼���
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        if(board.childCount == 0)  //���� ���� �ƹ��͵� ���� ����
        {
            GameObject rice = Instantiate(ricePrefab, board.position, board.rotation, board);
        }
    }
}
