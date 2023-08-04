using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    public Dictionary<string, int> sushiCounts;  //���� �ʹ� ���� ��ųʸ�
    public List<Sushi> sushiList;  //�ʹ� ����Ʈ
    public Transform boardTr;  //���� ��ġ

    AudioSource audioSource;
    Transform tr;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sushiList = new List<Sushi>();  //�ʹ�����, �ͻ��, ���
        sushiCounts = new Dictionary<string, int>();  //�ʹ����� + �ͻ��, ����
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        // UpdateDish();
    }

    public void AddSushi(string sushiName, string wasabi, int gold) //�ʹ� ����. DragSushi���� �ʹ� ���ÿ� ���� �� ȣ��.
    {
        Sushi sushi = new Sushi(sushiName, wasabi, gold);
        string sushiKey = sushi.sushiName + "_" + sushi.wasabi;  //�ʹ����� + �ͻ�� ��ųʸ� Key��.
        sushiList.Add(sushi);  //����Ʈ���� �߰�
        if (sushiCounts.ContainsKey(sushiKey))  //���� �̹� ��ųʸ��� Ű�� �����ϸ�
        {
            sushiCounts[sushiKey]++;  //���� ����
        }
        else  //������
        {
            sushiCounts[sushiKey] = 1;  //������ 1
        }
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
        foreach (Transform child in tr)
        {
            Destroy(child.gameObject);
        }
    }

    public void ClearBoard()  //���� �� �ʹ� ���� �޼���.
    {
        foreach (Transform child in boardTr)
        {
            Destroy(child.gameObject);
        }
    }

    public void Clear()  //���� �� �ʹ� ���� �޼���.
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        foreach (Transform child in boardTr)
        {
            Destroy(child.gameObject);
        }
    }
}
