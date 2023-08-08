using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpGradeFishing : MonoBehaviour
{
    public GameObject noMoneyTxt;
    public GameObject impossibleTxt;
    public GameObject ItemContent;
    public Text fullTxt;

    EndSceneCtrl endSceneCtrl;
    Image[] _items;
    AudioSource audioSource;

    int normalItemPrice = 1000;
    int rareItemPrice = 3000;
    int middleRodPrice = 500000;
    int highRodPrice = 1000000;
    int middleRodAtk = 1;
    int highRodAtk = 2;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        endSceneCtrl = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<EndSceneCtrl>();
    }

    private void Start()
    {
        // ���ӸŴ����� �����͸� �ҷ��ͼ� �ʱ�ȭ
        if (GameManager.instance.data.getMiddleRod == "TRUE" && gameObject.name == ("MiddleRod"))
        {
            GetComponentInChildren<Text>().text = "���� ���ݷ� : " + GameManager.instance.data.atk + "\n\n������ ��ǰ�Դϴ�";
        }

        if (GameManager.instance.data.getHighRod == "TRUE" && gameObject.name == ("HighRod"))
        {
            GetComponentInChildren<Text>().text = "���� ���ݷ� : " + GameManager.instance.data.atk + "\n\n������ ��ǰ�Դϴ�";
        }
    }

    public void MiddleRod()
    {
        if (GameManager.instance.data.getMiddleRod == "TRUE")
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

            return;
        }

        // ���ݷ��� 2 �����̰� ��尡 500000 �̻��� ��
        if (int.Parse(GameManager.instance.data.gold) >= middleRodPrice)
        {
            int _gold = int.Parse(GameManager.instance.data.gold) - middleRodPrice;
            GameManager.instance.data.gold = _gold.ToString();
            int _atk = int.Parse(GameManager.instance.data.atk) + middleRodAtk;
            GameManager.instance.data.atk = _atk.ToString();
            GameManager.instance.data.getMiddleRod = "TRUE";

            Text text = GetComponentInChildren<Text>();
            text.text = "���� ���ݷ� : " + GameManager.instance.data.atk + "\n\n������ ��ǰ�Դϴ�";

            // ��� ���� ������ �ǵ��� ��ĥ ��
            audioSource.PlayOneShot(SoundManager.instance.levelUp, 1);
            endSceneCtrl.UIUpdate();
        }

        else
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
            StartCoroutine(NoMoney());
        }
    }

    public void HighRod()
    {
        if (GameManager.instance.data.getHighRod == "TRUE")
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

            return;
        }

        if (int.Parse(GameManager.instance.data.gold) >= highRodPrice)
        {
            int _gold = int.Parse(GameManager.instance.data.gold) - highRodPrice;
            GameManager.instance.data.gold = _gold.ToString();
            int _atk = int.Parse(GameManager.instance.data.atk) + highRodAtk;
            GameManager.instance.data.atk = _atk.ToString();
            GameManager.instance.data.getHighRod = "TRUE";

            Text text = GetComponentInChildren<Text>();
            text.text = "���� ���ݷ� : " + GameManager.instance.data.atk + "\n\n������ ��ǰ�Դϴ�";

            audioSource.PlayOneShot(SoundManager.instance.levelUp, 1);
            endSceneCtrl.UIUpdate();
        }

        else
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

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
            if (_items[i].gameObject.name.Contains("Slot"))
            {
                if (_items[i].GetComponentInChildren<Text>().text.Contains(itemName))
                {
                    if (_text.text.Contains("������") && int.Parse(GameManager.instance.data.gold) >= normalItemPrice)
                    {
                        Debug.Log("������, �ߺ�");
                        isFull = FindIndexItem("������", i, normalItemPrice);
                        isChange = true;
                        audioSource.PlayOneShot(SoundManager.instance.levelUp, 1);
                        break;
                    }
                    else if (_text.text.Contains("����") && int.Parse(GameManager.instance.data.gold) >= normalItemPrice)
                    {
                        Debug.Log("����, �ߺ�");
                        isFull = FindIndexItem("����", i, normalItemPrice);
                        isChange = true;
                        audioSource.PlayOneShot(SoundManager.instance.levelUp, 1);
                        break;
                    }
                    else if (_text.text.Contains("������") && int.Parse(GameManager.instance.data.gold) >= rareItemPrice)
                    {
                        // 5���� ���� ���� �Ұ�
                        if (int.Parse(GameManager.instance.data.dateCount) <= 5)
                        {
                            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
                            StartCoroutine(Impossible());
                        }

                        else
                        {
                            Debug.Log("������, �ߺ�");
                            isFull = FindIndexItem("������", i, rareItemPrice);
                            isChange = true;
                            audioSource.PlayOneShot(SoundManager.instance.levelUp, 1);
                            break;
                        }
                    }
                    else
                    {
                        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

                        Debug.Log("�ߺ�, ��Ӵ�");
                        StartCoroutine(NoMoney());
                        isFull = false;
                        break;
                    }
                }
            }
        }
        if (isChange == false)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i].gameObject.name.Contains("Slot"))
                {
                    ItemSlot _slot = _items[i].GetComponent<ItemSlot>();
                    Image Img = null;
                    Image[] itemImgs = _items[i].GetComponentsInChildren<Image>();
                    foreach (Image itemImg in itemImgs)
                    {
                        if (itemImg.name.Contains("Img"))
                            Img = itemImg;
                    }
                    if (_slot.isEmpty == false)
                    {
                        if (int.Parse(GameManager.instance.data.gold) >= normalItemPrice && _text.text.Contains("������"))
                        {
                            Debug.Log("������, ó��");
                            _items[i].GetComponent<CanvasGroup>().interactable = true;
                            isFull = SetItem(i, "������", "1", _slot, normalItemPrice, Img);
                            audioSource.PlayOneShot(SoundManager.instance.levelUp, 1);
                            break;
                        }
                        else if (int.Parse(GameManager.instance.data.gold) >= normalItemPrice && _text.text.Contains("����"))
                        {
                            Debug.Log("����, ó��");
                            _items[i].GetComponent<CanvasGroup>().interactable = true;
                            isFull = SetItem(i, "����", "1", _slot, normalItemPrice, Img);
                            audioSource.PlayOneShot(SoundManager.instance.levelUp, 1);
                            break;
                        }
                        else if (int.Parse(GameManager.instance.data.gold) >= rareItemPrice && _text.text.Contains("������"))
                        {
                            Debug.Log("������, ó��");
                            _items[i].GetComponent<CanvasGroup>().interactable = true;
                            isFull = SetItem(i, "������", "1", _slot, rareItemPrice, Img);
                            audioSource.PlayOneShot(SoundManager.instance.levelUp, 1);
                            break;
                        }
                        else
                        {
                            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

                            Debug.Log("ó��, ��Ӵ�");
                            StartCoroutine(NoMoney());
                            isFull = false;
                            break;
                        }
                    }
                }
            }
        }
        if (isFull)
        {
            StartCoroutine(Full());
        }
    }

    IEnumerator Impossible()
    {
        impossibleTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        impossibleTxt.gameObject.SetActive(false);
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

            endSceneCtrl.UIUpdate();
        }

        return false;
    }

    bool SetItem(int i, string name, string count, ItemSlot slot, int price, Image Img)
    {
        Img.sprite = gameObject.GetComponentsInChildren<Image>()[1].sprite;
        _items[i].GetComponentInChildren<Text>().text = name + "    " + count + "��";
        GameManager.instance.inventory_Items.Add(new InventoryItem(name, count));
        int _gold = int.Parse(GameManager.instance.data.gold) - price;
        GameManager.instance.data.gold = _gold.ToString();

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

    private void OnDisable()
    {
        StopCoroutine(Full());
        StopCoroutine(NoMoney());
    }
}
