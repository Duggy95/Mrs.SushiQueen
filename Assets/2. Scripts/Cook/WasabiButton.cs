using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasabiButton : MonoBehaviour
{
    public GameObject wasabi;  //�ͻ�� ������
    public GameObject wasabi1;  //ū �ͻ�� ������
    public GameObject board;  //����
    GameObject Wasabi;
    GameObject Wasabi1;
    bool isWasabi;

    public void WasabiBtn()
    {
        if (board == null)
        {
            GameObject board = GameObject.Find("Board_RawImage");  //���� ������Ʈ ã��
        }

        if(Wasabi == null)
        {
            Transform riceTr = board.transform.Find("Rice(Clone)").transform;  //���� ������Ʈ�� �ڽ� ������Ʈ ã��.
            Vector3 wasabiTr = new Vector3(riceTr.position.x, riceTr.position.y + 10, 0);  //�� ���� ����
            Wasabi = Instantiate(wasabi, wasabiTr, Quaternion.identity, riceTr);  //�ͻ�� ����.

            Sushi sushi = riceTr.GetComponent<Sushi>();
            sushi.wasabi = "��������";  //�ʹ信 �ͻ�� �� ����.
        }
        else if(Wasabi != null)
        {
            Transform riceTr = board.transform.Find("Rice(Clone)").transform;  //���� ������Ʈ�� �ڽ� ������Ʈ ã��.
            Vector3 wasabiTr = new Vector3(riceTr.position.x, riceTr.position.y + 10, 0);  //�� ���� ����
            Wasabi1 = Instantiate(wasabi1, wasabiTr, Quaternion.identity, riceTr);  //�ͻ�� ����.

            Sushi sushi = riceTr.GetComponent<Sushi>();
            sushi.wasabi = "���� �־�";  //�ʹ信 �ͻ�� �� ����.
        }
    }
}
