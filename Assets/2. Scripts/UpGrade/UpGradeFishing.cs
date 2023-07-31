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

    int normalItemPrice = 1000;
    int rareItemPrice = 3000;
    int middleRodPrice = 500000;
    int highRodPrice = 1000000;
    int middleRodAtk = 1;
    int highRodAtk = 2;

    private void Awake()
    {
        endSceneCtrl = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<EndSceneCtrl>();
    }

    private void Start()
    {
        // 게임매니저에 데이터를 불러와서 초기화
        if (GameManager.instance.data.getMiddleRod == "TRUE" && gameObject.name == ("MiddleRod"))
        {
            GetComponentInChildren<Text>().text = "현재 공격력 : " + GameManager.instance.data.atk + "\n구매한 제품입니다";
        }

        if (GameManager.instance.data.getHighRod == "TRUE" && gameObject.name == ("HighRod"))
        {
            GetComponentInChildren<Text>().text = "현재 공격력 : " + GameManager.instance.data.atk + "\n구매한 제품입니다";
        }
    }

    public void MiddleRod()
    {
        if (GameManager.instance.data.getMiddleRod == "TRUE")
        {
            return;
        }

        // 공격력이 2 이하이고 골드가 500000 이상일 때
        if (int.Parse(GameManager.instance.data.gold) >= middleRodPrice)
        {
            int _gold = int.Parse(GameManager.instance.data.gold) - middleRodPrice;
            GameManager.instance.data.gold = _gold.ToString();
            int _atk = int.Parse(GameManager.instance.data.atk) + middleRodAtk;
            GameManager.instance.data.atk = _atk.ToString();
            GameManager.instance.data.getMiddleRod = "TRUE";
            //GameManager.instance.Save("d");

            Text text = GetComponentInChildren<Text>();
            text.text = "현재 공격력 : " + GameManager.instance.data.atk + "\n구매한 제품입니다";
            // 사면 같이 갱신이 되도록 고칠 것
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

        if (int.Parse(GameManager.instance.data.gold) >= highRodPrice)
        {
            int _gold = int.Parse(GameManager.instance.data.gold) - highRodPrice;
            GameManager.instance.data.gold = _gold.ToString();
            int _atk = int.Parse(GameManager.instance.data.atk) + highRodAtk;
            GameManager.instance.data.atk = _atk.ToString();
            GameManager.instance.data.getHighRod = "TRUE";
            //GameManager.instance.Save("d");

            Text text = GetComponentInChildren<Text>();
            text.text = "현재 공격력 : " + GameManager.instance.data.atk + "\n구매한 제품입니다";

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
        Debug.Log("아이템 칸 수 : " + _items.Length);
        Debug.Log("text : " + _text.text);
        bool isFull = true;
        bool isChange = false;

        // 텍스트를 공백으로 분할하여 첫 번째 아이템 이름만 추출
        string[] itemInfo = _text.text.Split(' ');

        string itemName = itemInfo[0];

        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i].gameObject.name.Contains("Slot"))
            {
                if (_items[i].GetComponentInChildren<Text>().text.Contains(itemName))
                {
                    if (_text.text.Contains("지렁이") && int.Parse(GameManager.instance.data.gold) >= normalItemPrice)
                    {
                        Debug.Log("지렁이, 중복");
                        isFull = FindIndexItem("지렁이", i, normalItemPrice);
                        isChange = true;
                        break;
                    }
                    else if (_text.text.Contains("새우") && int.Parse(GameManager.instance.data.gold) >= normalItemPrice)
                    {
                        Debug.Log("새우, 중복");
                        isFull = FindIndexItem("새우", i, normalItemPrice);
                        isChange = true;
                        break;
                    }
                    else if (_text.text.Contains("생선살") && int.Parse(GameManager.instance.data.gold) >= rareItemPrice)
                    {
                        Debug.Log("생선살, 중복");
                        isFull = FindIndexItem("생선살", i, rareItemPrice);
                        isChange = true;
                        break;
                    }
                    else
                    {
                        Debug.Log("중복, 노머니");
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
                        if (int.Parse(GameManager.instance.data.gold) >= normalItemPrice && _text.text.Contains("지렁이"))
                        {
                            Debug.Log("지렁이, 처음");
                            isFull = SetItem(i, "지렁이", "1", _slot, normalItemPrice, Img);
                            break;
                        }
                        else if (int.Parse(GameManager.instance.data.gold) >= normalItemPrice && _text.text.Contains("새우"))
                        {
                            Debug.Log("새우, 처음");
                            isFull = SetItem(i, "새우", "1", _slot, normalItemPrice, Img);
                            break;
                        }
                        else if (int.Parse(GameManager.instance.data.gold) >= rareItemPrice && _text.text.Contains("생선살"))
                        {
                            Debug.Log("생선살, 처음");
                            isFull = SetItem(i, "생선살", "1", _slot, rareItemPrice, Img);
                            break;
                        }
                        else
                        {
                            Debug.Log("처음, 노머니");
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

    bool FindIndexItem(string name, int i, int price)
    {
        string valueToFind = name;
        int newValue = 1;

        // 특정 값(valueToFind)을 만족하는 첫 번째 요소의 인덱스를 찾기
        int index = GameManager.instance.inventory_Items.FindIndex(item => item.item_Name == valueToFind);

        if (index != -1)
        {
            newValue += int.Parse(GameManager.instance.inventory_Items[index].item_Count);

            // 해당 인덱스(index)의 값 변경
            GameManager.instance.inventory_Items[index].item_Count = newValue.ToString();
            _items[i].GetComponentInChildren<Text>().text = name + "   " + newValue.ToString() + "개";
            int _gold = int.Parse(GameManager.instance.data.gold) - price;
            GameManager.instance.data.gold = _gold.ToString();
            Debug.Log("중복 종류 " + index + " changed to " + newValue);
            /* GameManager.instance.Save("i");
             GameManager.instance.Save("d");*/
            endSceneCtrl.UIUpdate();
        }

        return false;
    }

    bool SetItem(int i, string name, string count, ItemSlot slot, int price, Image Img)
    {
        Img.sprite = gameObject.GetComponentsInChildren<Image>()[1].sprite;
        _items[i].GetComponentInChildren<Text>().text = name + "    " + count + "개";
        GameManager.instance.inventory_Items.Add(new InventoryItem(name, count));
        int _gold = int.Parse(GameManager.instance.data.gold) - price;
        GameManager.instance.data.gold = _gold.ToString();
        /* GameManager.instance.Save("i");
         GameManager.instance.Save("d");*/
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
