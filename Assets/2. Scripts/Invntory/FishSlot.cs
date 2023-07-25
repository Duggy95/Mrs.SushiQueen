using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FishSlot : MonoBehaviour
{
    public FishData[] fishDatas;
    public FishData fishData;
    Text _text;

    public bool isEmpty = false;
    public int fish_GradeNum;
    public int fish_ColorNum;
    public string fish_Name;

    private void Start()
    {
        _text = GetComponentInChildren<Text>();
        fish_Name = _text.text.Split(" ")[0];

        print(fish_Name);
        // �迭�� ��ȸ�ϸ鼭 �̸��� ���� ��Ҹ� ã�� ���� ����
        for (int i = 0; i < fishDatas.Length; i++)
        {
            // �迭�� �� ����� �̸��� ���Ͽ� ���� �̸��� ���� ��Ҹ� ã��
            if (fishDatas[i].fishName == fish_Name)
            {
                fishData = fishDatas[i];
                print(fishData.fishName);
                break;
            }
        }
    }

    public void UpdateData()
    {
        string[] slotInfo = _text.text.Split(" ");
        string fishName = slotInfo[0];
        print(fishName);
        string valueToFind = _text.text;

        int newValue = 1;
        // Ư�� ��(valueToFind)�� �����ϴ� ù ��° ����� �ε����� ã��
        int index = GameManager.instance.inventory_Fishs.FindIndex(item => item.fish_Name == valueToFind);

        if (index != -1)
        {
            int count = int.Parse(GameManager.instance.inventory_Fishs[index].fish_Count) - newValue;

            // �ش� �ε���(index)�� �� ����
            GameManager.instance.inventory_Fishs[index].fish_Count = count.ToString();
            _text.text = fishName + "   " + count + "����";
            GameManager.instance.Save("f");
            if (count <= 0)
            {
                GameManager.instance.inventory_Fishs.RemoveAt(index);
                gameObject.GetComponentInChildren<Image>().sprite = null;
                GetComponentInChildren<Text>().text = "�� ����";
                GameManager.instance.Save("f");
            }
        }
    }
}
