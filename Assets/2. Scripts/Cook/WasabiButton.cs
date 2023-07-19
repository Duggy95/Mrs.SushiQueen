using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasabiButton : MonoBehaviour
{
    public GameObject wasabi;  //�ͻ�� ������
    public GameObject wasabi1;  //ū �ͻ�� ������
    public GameObject board;  //����

    public void WasabiBtn()
    {
        if (board == null)  //������ ������
        {
            GameObject board = GameObject.Find("Board_RawImage");  //���� ������Ʈ ã��
        }

        Transform riceTr = board.transform.Find("Rice(Clone)").transform;  //���� ������Ʈ�� �ڽ� ������Ʈ ã��.
        if(riceTr != null)
        {
            Vector3 wasabiTr = new Vector3(riceTr.position.x, riceTr.position.y + 10, 0);  //�� ���� ����
            Sushi sushi = riceTr.GetComponent<Sushi>();  //�ʹ� ��������.

            if (sushi.wasabi == "����" && riceTr.childCount == 0)  //�ʹ��� �ͻ�� "����" �̰� �ڽ��� 0�� ��
            {
                GameObject Wasabi = Instantiate(wasabi, wasabiTr, Quaternion.identity, riceTr);  //�ͻ�� ����.
                sushi.wasabi = "��������";  //�ʹ信 �ͻ�� �� ����.
            }
            else if (sushi.wasabi == "��������" && riceTr.childCount == 1)  //�ʹ��� �ͻ�� �����̰� �ڽ��� 1�� ��
            {
                GameObject Wasabi1 = Instantiate(wasabi1, wasabiTr, Quaternion.identity, riceTr);  //�ͻ�� ����.
                sushi.wasabi = "���� �־";  //�ʹ信 �ͻ�� �� ����.
            }
        }
    }
}
