using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceButton : MonoBehaviour
{
    public GameObject ricePrefab;  //�� ������. 
    public Transform board;  //����

    public void RiceBtn()  //�� ���� �޼���
    {
        if(board.childCount == 0)  //���� ���� �ƹ��͵� ���� ����
        {
            GameObject rice = Instantiate(ricePrefab, board.position, board.rotation, board);
        }
    }
}
