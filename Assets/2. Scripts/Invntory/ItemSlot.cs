using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public bool isEmpty = false;

    FishingManager fm;
    Button button;
    Text _text;

    void Awake()
    {
        fm = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<FishingManager>();
        if(fm != null )
        {
            button = GetComponent<Button>(); //��ư component ��������
            button.onClick.AddListener(() => UseItem()); //���ڰ� ���� �� �Լ� ȣ��
            _text = GetComponentInChildren<Text>();
        }
        else
        {
            return;
        }
    }

    public void UseItem()
    {
        if(_text.text.Contains("������"))
        {
            fm.useItem_white = true;
            print("������ ���");
        }

        else if(_text.text.Contains("����"))
        {
            fm.useItem_red = true;
            print("���� ���");
        }

        else if(_text.text.Contains("������"))
        {
            fm.useItem_rare = true;
            print("������ ���");
        }

        UpdateData();
    }

    void UpdateData()
    {
        string[] slotInfo = _text.text.Split(" ");
        string itemName = slotInfo[0];
        print(itemName);
        string valueToFind = _text.text;

        int newValue = 1;
        // Ư�� ��(valueToFind)�� �����ϴ� ù ��° ����� �ε����� ã��
        int index = GameManager.instance.inventory_Items.FindIndex(item => item.item_Name == valueToFind);

        if (index != -1)
        {
            int count = int.Parse(GameManager.instance.inventory_Items[index].item_Count) - newValue;

            // �ش� �ε���(index)�� �� ����
            GameManager.instance.inventory_Items[index].item_Count = count.ToString();
            _text.text = itemName + "   " + count + "��";
            GameManager.instance.Save("i");
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
