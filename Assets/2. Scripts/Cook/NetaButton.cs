using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetaButton : MonoBehaviour
{
    public GameObject netaPrefab; //ȸ ������
    public GameObject board;  //����
    public FishData fishData;  //���� ������

    public void FishBtn()
    {   
        if(board == null)
        {
            GameObject board = GameObject.Find("Board_RawImage");  //���� ������Ʈ ã��.
        }

        Transform riceTr = board.transform.Find("Rice(Clone)").transform;  //���� ������Ʈ�� �ڽ����� �ִ� Rice ã��.

        bool hasFish = false;  //������ �̹� �����ϴ� ��

        //�ߺ�Ȯ��.
        foreach (Transform child in riceTr)  
        {
            if (child.CompareTag("FISH"))
            {
                hasFish = true;
                break;
            }
        }

        if (riceTr != null && !hasFish)  //������ ���� ���� 
        {
            Vector3 netaTr = new Vector3(riceTr.position.x, riceTr.position.y + 10, 0);  //�� ���� ����
            GameObject neta = Instantiate(netaPrefab,
                                                            netaTr, Quaternion.identity, riceTr); //�� ���� ���ʿ� ȸ�����ϰ� ���� �ڽ����� �ֱ�.
            neta.GetComponent<Neta>().fishData = fishData;  //���������� �Ѱ��ֱ�.
            riceTr.gameObject.AddComponent<DragSushi>();  //�� ������Ʈ�� DragSushi ��ũ��Ʈ Add.
        }
    }
}
