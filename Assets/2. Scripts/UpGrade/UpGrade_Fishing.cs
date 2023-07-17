using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpGrade_Fishing : MonoBehaviour
{
    public GameObject noMoneyTxt;
    public GameObject ItemContent;

    private void Start()
    {

    }

    public void MiddleRod()
    {
        if (GameManager.instance.atk < 2 && GameManager.instance.gold >= 500000)
        {
            GameManager.instance.gold -= 500000;
            GameManager.instance.atk = 2;

            GameManager.instance.SetAtk();
            GameManager.instance.SetGold();

            Text text = GetComponentInChildren<Text>();
            text.text = "������ ��ǰ�Դϴ�";

            Destroy(this);
        }

        else
        {
            StartCoroutine(NoMoney());
        }
    }

    public void HighRod()
    {
        if (GameManager.instance.atk < 4 && GameManager.instance.gold >= 1000000)
        {
            GameManager.instance.gold -= 1000000;
            GameManager.instance.atk = 4;

            GameManager.instance.SetAtk();
            GameManager.instance.SetGold();

            Text text = GetComponentInChildren<Text>();
            text.text = "������ ��ǰ�Դϴ�";

            Destroy(this);
        }

        else
        {
            StartCoroutine(NoMoney());
        }
    }

    public void Buy_Item()
    {
        Text text = GetComponentInChildren<Text>();
        Image[] _items = ItemContent.gameObject.GetComponentsInChildren<Image>();
        Debug.Log("������ ĭ �� : " + _items.Length);

        // �������� ������ �˻��Ͽ� ������� �ش� ���� ����
        for (int i = 0; i < _items.Length; i++)
        {
            ItemSlot _slot = _items[i].GetComponent<ItemSlot>();
            if (_slot.isEmpty == false)
            {
                _items[i].sprite = gameObject.GetComponent<Image>().sprite;

                if (GameManager.instance.gold > 10000 && text.name == "White")
                {
                    _items[i].GetComponentInChildren<Text>().text = "������";
                    GameManager.instance.items.Add("������");
                    _slot.isEmpty = true;
                }
                else if (GameManager.instance.gold > 10000 && text.name == "Red")
                {
                    _items[i].GetComponentInChildren<Text>().text = "����";
                    GameManager.instance.items.Add("����");
                    _slot.isEmpty = true;
                }
                else if (GameManager.instance.gold > 50000 && text.name == "Rare")
                {
                    _items[i].GetComponentInChildren<Text>().text = "������";
                    GameManager.instance.items.Add("������");
                    _slot.isEmpty = true;
                }
                else
                {
                    StartCoroutine(NoMoney());
                }
                break;
            }
        }
    }

    IEnumerator NoMoney()
    {
        noMoneyTxt.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        noMoneyTxt.gameObject.SetActive(false);
    }

}
