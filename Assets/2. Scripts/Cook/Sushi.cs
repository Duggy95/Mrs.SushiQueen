using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sushi : MonoBehaviour
{
    public string sushiName;  //�ʹ� ���� = ���� �̸�
    public string wasabi = "����";  //�ͻ�� ����

    private void Start()
    {
        wasabi = "����";
    }

    private void Update()
    {
        print(sushiName + "," + wasabi);
    }

    public Sushi(string sushiName, string wasabi)  //������
    {
        this.sushiName = sushiName;
        this.wasabi = wasabi;
    }
}
