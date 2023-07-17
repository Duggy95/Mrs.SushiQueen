using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    //public List<Sushi> sushiList;
    public Dictionary<string, int> sushiCounts;
    Transform tr;

    void Start()
    {
        //sushiList = new List<Sushi>();  //�ʹ�����, �ͻ��
        sushiCounts = new Dictionary<string, int>();  //�ʹ����� + �ͻ��, ����
        tr = GetComponent<RectTransform>();
    }

    void Update()
    {
        // UpdateDish();
    }

    public void AddSushi(string sushiName, string wasabi) //�ʹ� ����. DragSushi���� �ʹ� ���ÿ� ���� �� ȣ��.
    {
        Sushi sushi = new Sushi(sushiName, wasabi);
        string sushiKey = sushi.sushiName + " _" + sushi.wasabi+ " ";  //�ʹ����� + �ͻ�� ��ųʸ� Key��.

        if (sushiCounts.ContainsKey(sushiKey))  //���� �̹� ��ųʸ��� Ű�� �����ϸ�
        {
            sushiCounts[sushiKey]++;  //���� ����
        }
        else  //������
        {
            sushiCounts[sushiKey] = 1;  //������ 1
        }
        //sushiList.Add(sushi);
    }

    public void UpdateDish()
    {
        foreach (var sushi in sushiCounts)
        {
            string sushiInfo = sushi.Key; 
            int count = sushi.Value;

            Debug.Log($"�ʹ� ����: {sushiInfo}, �ߺ� Ƚ��: {count}");
        }
    }

    public void ClearSushi()  //���� �� �ʹ�� ���� �޼���.
    {
        foreach (RectTransform child in tr)
        {
            Destroy(child.gameObject);
        }
    }
}
