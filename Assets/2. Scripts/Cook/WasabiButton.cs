using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasabiButton : MonoBehaviour
{
    public GameObject wasabi;  //�ͻ�� ������
    public GameObject board;  //����

    public void WasabiBtn()
    {
        if (board == null)
        {
            GameObject board = GameObject.Find("Board_RawImage");
        }

        Transform riceTr = board.transform.Find("Rice(Clone)").transform;
        Vector3 wasabiTr = new Vector3(riceTr.position.x, riceTr.position.y + 10, 0);  //�� ���� ����
        GameObject neta = Instantiate(wasabi,
                                                        wasabiTr, Quaternion.identity, riceTr);  //�ͻ�� ����.

        Sushi sushi = riceTr.GetComponent<Sushi>();
        sushi.wasabi = "��������";  //�ʹ信 �ͻ�� �� ����.
    }
}
