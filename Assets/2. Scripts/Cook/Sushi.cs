using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sushi : MonoBehaviour
{
    public string sushiName;  //�ʹ� ���� = ���� �̸�
    public string wasabi = "����";  //�ͻ�� ����
    public int gold;  //����


    private void Start()
    {
        wasabi = "����";  //�⺻ ��.
    }

    private void Update()
    {
        print(sushiName + "," + wasabi);
    }

    public Sushi(string sushiName, string wasabi, int gold)  //������
    {
        this.sushiName = sushiName;
        this.wasabi = wasabi;
        this.gold = gold;
    }
}
