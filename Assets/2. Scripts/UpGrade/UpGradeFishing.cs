using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpGradeFishing : MonoBehaviour
{
    public GameObject noMoneyTxt;
    public GameObject ItemContent;
    public Text fullTxt;

    EndSceneCtrl endSceneCtrl;
    Image[] _items;

    int normalItemPrice = 100000;
    int rareItemPrice = 500000;
    int middleRodPrice = 500000;
    int highRodPrice = 1000000;
    int middleRodAtk = 2;
    int highRodAtk = 4;

    private void Awake()
    {
        endSceneCtrl = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<EndSceneCtrl>();
    }

    private void Start()
    {
        // ���ӸŴ����� �����͸� �ҷ��ͼ� �ʱ�ȭ
        if (GameManager.instance.data.getMiddleRod == "TRUE" && gameObject.name == ("MiddleRod"))
        {
            GetComponentInChildren<Text>().text = "������ ��ǰ�Դϴ�";
        }

        if (GameManager.instance.data.getHighRod == "TRUE" && gameObject.name == ("HighRod"))
        {
            GetComponentInChildren<Text>().text = "������ ��ǰ�Դϴ�";
        }
    }

    public void MiddleRod()
    {
        if (GameManager.instance.data.getMiddleRod == "TRUE")
        {
            return;
        }

        // ���ݷ��� 2 �����̰� ��尡 500000 �̻��� ��
        if (int.Parse(GameManager.instance.data.atk) < middleRodAtk && int.Parse(GameManager.instance.data.gold) >= middleRodPrice)
        {
            int _gold = int.Parse(GameManager.instance.data.gold) - middleRodPrice;
            GameManager.instance.data.gold = _gold.ToString();
            GameManager.instance.data.atk = middleRodAtk.ToString();
            GameManager.instance.data.getMiddleRod = "TRUE";
            GameManager.instance.Save("s");

            Text text = GetComponentInChildren<Text>();
            text.text = "������ ��ǰ�Դϴ�";

            endSceneCtrl.UIUpdate();
        }

        else
        {
            StartCoroutine(NoMoney());
        }
    }

    public void HighRod()
    {
        if (GameManager.instance.data.getHighRod == "TRUE")
        {
            return;
        }

        if (int.Parse(GameManager.instance.data.atk) < highRodAtk && int.Parse(GameManager.instance.data.gold) >= highRodPrice)
        {
            int _gold = int.Parse(GameManager.instance.data.gold) - highRodPrice;
            GameManager.instance.data.gold = _gold.ToString();
            GameManager.instance.data.atk = highRodAtk.ToString();
            GameManager.instance.data.getHighRod = "TRUE";
            GameManager.instance.Save("s");

            Text text = GetComponentInChildren<Text>();
            text.text = "������ ��ǰ�Դϴ�";

            endSceneCtrl.UIUpdate();
        }

        else
        {
            StartCoroutine(NoMoney());
        }
    }

    public void Buy_Item()
    {
        Text _text = GetComponentInChildren<Text>();
        _items = ItemContent.gameObject.GetComponentsInChildren<Image>();
        Debug.Log("������ ĭ �� : " + _items.Length);
        Debug.Log("text : " + _text.text);
        bool isFull = true;
        bool isChange = false;

        // �ؽ�Ʈ�� �������� �����Ͽ� ù ��° ������ �̸��� ����
        string[] itemInfo = _text.text.Split(' ');

        string itemName = itemInfo[0];

        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i].GetComponentInChildren<Text>().text.Contains(itemName))
            {
                if (_text.text.Contains("������") && int.Parse(GameManager.instance.data.gold) >= normalItemPrice)
                {
                    Debug.Log("������, �ߺ�");
                    isFull = FindIndexItem("������", i, normalItemPrice);
                    isChange = true;
                    break;
                }
                else if (_text.text.Contains("����") && int.Parse(GameManager.instance.data.gold) >= normalItemPrice)
                {
                    Debug.Log("����, �ߺ�");
                    isFull = FindIndexItem("����", i, normalItemPrice);
                    isChange = true;
                    break;
                }
                else if (_text.text.Contains("������") && int.Parse(GameManager.instance.data.gold) >= rareItemPrice)
                {
                    Debug.Log("������, �ߺ�");
                    isFull = FindIndexItem("������", i, rareItemPrice);
                    isChange = true;
                    break;
                }
                else
                {
                    Debug.Log("�ߺ�, ��Ӵ�");
                    StartCoroutine(NoMoney());
                    isFull = false;
                    break;
                }
            }
            /*else if (_items[i].GetComponentInChildren<Text>().text.Contains("����"))
            {
                
                else
                {
                    Debug.Log("����, �ߺ�, ��Ӵ�");
                    StartCoroutine(NoMoney());
                    break;
                }
            }
            else if (_items[i].GetComponentInChildren<Text>().text.Contains("������"))
            {
                
                else
                {
                    Debug.Log("������, �ߺ�, ��Ӵ�");
                    StartCoroutine(NoMoney());
                    break;
                }
            }*/
        }
        if (isChange == false)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                ItemSlot _slot = _items[i].GetComponent<ItemSlot>();

                if (_slot.isEmpty == false)
                {
                    if (int.Parse(GameManager.instance.data.gold) >= normalItemPrice && _text.text.Contains("������"))
                    {
                        Debug.Log("������, ó��");
                        isFull = SetItem(i, "������", "1", _slot, normalItemPrice);
                        break;
                    }
                    else if (int.Parse(GameManager.instance.data.gold) >= normalItemPrice && _text.text.Contains("����"))
                    {
                        Debug.Log("����, ó��");
                        isFull = SetItem(i, "����", "1", _slot, normalItemPrice);
                        break;
                    }
                    else if (int.Parse(GameManager.instance.data.gold) >= rareItemPrice && _text.text.Contains("������"))
                    {
                        Debug.Log("������, ó��");
                        isFull = SetItem(i, "������", "1", _slot, rareItemPrice);
                        break;
                    }
                    else
                    {
                        Debug.Log("ó��, ��Ӵ�");
                        StartCoroutine(NoMoney());
                        isFull = false;
                        break;
                    }
                }
            }
        }
        if (isFull)
        {
            StartCoroutine(Full());
        }
    }

    bool FindIndexItem(string name, int i, int price)
    {
        string valueToFind = name;
        int newValue = 1;

        // Ư�� ��(valueToFind)�� �����ϴ� ù ��° ����� �ε����� ã��
        int index = GameManager.instance.inventory_Items.FindIndex(item => item.item_Name == valueToFind);

        if (index != -1)
        {
            newValue += int.Parse(GameManager.instance.inventory_Items[index].item_Count);

            // �ش� �ε���(index)�� �� ����
            GameManager.instance.inventory_Items[index].item_Count = newValue.ToString();
            _items[i].GetComponentInChildren<Text>().text = name + "   " + newValue.ToString() + "��";
            int _gold = int.Parse(GameManager.instance.data.gold) - price;
            GameManager.instance.data.gold = _gold.ToString();
            Debug.Log("�ߺ� ���� " + index + " changed to " + newValue);
            GameManager.instance.Save("i");
            GameManager.instance.Save("s");
            endSceneCtrl.UIUpdate();
        }

        return false;
    }

    bool SetItem(int i, string name, string count, ItemSlot slot, int price)
    {
        _items[i].sprite = gameObject.GetComponent<Image>().sprite;
        _items[i].GetComponentInChildren<Text>().text = name + "    " + count + "��";
        GameManager.instance.inventory_Items.Add(new InventoryItem(name, count));
        int _gold = int.Parse(GameManager.instance.data.gold) - price;
        GameManager.instance.data.gold = _gold.ToString();
        GameManager.instance.Save("i");
        GameManager.instance.Save("s");
        slot.isEmpty = true;

        endSceneCtrl.UIUpdate();

        return false;
    }

    IEnumerator NoMoney()
    {
        noMoneyTxt.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        noMoneyTxt.gameObject.SetActive(false);
    }

    IEnumerator Full()
    {
        fullTxt.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        fullTxt.gameObject.SetActive(false);
    }
}