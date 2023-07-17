using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
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
        button = GetComponent<Button>(); //��ư component ��������
        button.onClick.AddListener(() => UseItem()); //���ڰ� ���� �� �Լ� ȣ��
        _text = GetComponentInChildren<Text>();
    }

    public void UseItem()
    {
        if(_text.text == "������")
        {
            fm.useItem_white = true;
        }

        else if(_text.text == "����")
        {
            fm.useItem_red = true;
        }

        else if(_text.text == "������")
        {
            fm.useItem_rare = true;
        }
    }
}
