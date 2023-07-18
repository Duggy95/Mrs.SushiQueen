using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Neta : MonoBehaviour
{
    public FishData fishData;  //���� ������
    Image image;  //ȸ �̹���

    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = fishData.netaImg;  //�̹����� ���� �������� ȸ �̹�����.

        Sushi sushi = GetComponentInParent<Sushi>(); // �θ� ������Ʈ�� ���ÿ��� ��ũ��Ʈ ��������
        sushi.sushiName = fishData.fishName;  //�ʹ��� �̸��� ���� �������� ���� �̸�����
        //print(rice.sushiName);
    }
}
