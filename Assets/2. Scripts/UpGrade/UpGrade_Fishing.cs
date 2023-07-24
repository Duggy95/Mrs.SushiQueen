using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpGrade_Fishing : MonoBehaviour
{
    public GameObject noMoneyTxt; 
    public GameObject ItemContent;
    public GameObject middleRod;
    public GameObject highRod;

    EndSceneCtrl endSceneCtrl;
    Image[] _items;

    private void Awake()
    {
        endSceneCtrl = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<EndSceneCtrl>();
    }

    private void Start()
    {
        // 게임매니저에 데이터를 불러와서 초기화
        if (GameManager.instance.data.getMiddleRod == "TRUE")
        {
            middleRod.GetComponentInChildren<Text>().text = "구매한 제품입니다";
        }

        if (GameManager.instance.data.getHighRod == "TRUE")
        {
            highRod.GetComponentInChildren<Text>().text = "구매한 제품입니다";
        }
    }

    public void MiddleRod()
    {
        if (GameManager.instance.data.getMiddleRod == "TRUE")
        {
            return;
        }

        // 공격력이 2 이하이고 골드가 500000 이상일 때
        if (int.Parse(GameManager.instance.data.atk) < 2 && int.Parse(GameManager.instance.data.gold) >= 500000)
        {
            int _gold = int.Parse(GameManager.instance.data.gold) - 500000;
            GameManager.instance.data.gold = _gold.ToString();
            GameManager.instance.data.atk = 2.ToString();
            GameManager.instance.data.getMiddleRod = "TRUE";
            GameManager.instance.Save("s");

            Text text = GetComponentInChildren<Text>();
            text.text = "구매한 제품입니다";

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

        if (int.Parse(GameManager.instance.data.atk) < 4 && int.Parse(GameManager.instance.data.gold) >= 1000000)
        {
            int _gold = int.Parse(GameManager.instance.data.gold) - 1000000;
            GameManager.instance.data.gold = _gold.ToString();
            GameManager.instance.data.atk = 4.ToString();
            GameManager.instance.data.getHighRod = "TRUE";
            GameManager.instance.Save("s");

            Text text = GetComponentInChildren<Text>();
            text.text = "구매한 제품입니다";

            endSceneCtrl.UIUpdate();
        }

        else
        {
            StartCoroutine(NoMoney());
        }
    }

    public void Buy_Item()
    {
        Text text = GetComponentInChildren<Text>();
        _items = ItemContent.gameObject.GetComponentsInChildren<Image>();
        Debug.Log("아이템 칸 수 : " + _items.Length);

        // 수족관의 스롯을 검사하여 비었으면 해당 정보 전달
        for (int i = 0; i < _items.Length; i++)
        {
            ItemSlot _slot = _items[i].GetComponent<ItemSlot>();
            if (_slot.isEmpty == false)
            {
                if (int.Parse(GameManager.instance.data.gold) >= 10000 && text.gameObject.name == "White")
                {
                    /*_items[i].sprite = gameObject.GetComponent<Image>().sprite;
                    _items[i].GetComponentInChildren<Text>().text = "지렁이";
                    InventoryItem _inventoryItem = new InventoryItem();
                    _inventoryItem.item_Name = "지렁이";
                    _inventoryItem.item_Count = "1";
                    GameManager.instance.inventory_Items.Add(_inventoryItem);
                    GameManager.instance.inventory_Items.Add(new InventoryItem("지렁이", "1"));

                    int _gold = int.Parse(GameManager.instance.data.gold) - 10000;
                    GameManager.instance.data.gold = _gold.ToString();
                    GameManager.instance.Save("i");
                    endSceneCtrl.UIUpdate();*/
                    SetItem(i, "지렁이", "1", _slot);
                }
                else if (int.Parse(GameManager.instance.data.gold) >= 10000 && text.gameObject.name == "Red")
                {
                    /*_items[i].sprite = gameObject.GetComponent<Image>().sprite;
                    _items[i].GetComponentInChildren<Text>().text = "새우";
                    InventoryItem _inventoryItem = new InventoryItem();
                    _inventoryItem.item_Name = "새우";
                    _inventoryItem.item_Count = "1";
                    GameManager.instance.inventory_Items.Add(_inventoryItem);
                    GameManager.instance.inventory_Items.Add(new InventoryItem("새우", "1"));

                    int _gold = int.Parse(GameManager.instance.data.gold) - 10000;
                    GameManager.instance.data.gold = _gold.ToString();
                    GameManager.instance.Save("i");
                    endSceneCtrl.UIUpdate();*/
                    SetItem(i, "새우", "1", _slot);

                }
                else if (int.Parse(GameManager.instance.data.gold) >= 50000 && text.gameObject.name == "Rare")
                {
                    /*_items[i].sprite = gameObject.GetComponent<Image>().sprite;
                    _items[i].GetComponentInChildren<Text>().text = "생선살";
                    InventoryItem _inventoryItem = new InventoryItem();
                    _inventoryItem.item_Name = "생선살";
                    _inventoryItem.item_Count = "1";
                    GameManager.instance.inventory_Items.Add(_inventoryItem);
                    GameManager.instance.inventory_Items.Add(new InventoryItem("생선살", "1"));

                    int _gold = int.Parse(GameManager.instance.data.gold) - 50000;
                    GameManager.instance.data.gold = _gold.ToString();
                    GameManager.instance.Save("i");*/

                    SetItem(i, "생선살", "1", _slot);
                }
                else
                {
                    StartCoroutine(NoMoney());
                }
                break;
            }
        }
    }

    void SetItem(int i, string name, string count, ItemSlot slot)
    {
        _items[i].sprite = gameObject.GetComponent<Image>().sprite;
        _items[i].GetComponentInChildren<Text>().text = name + "    " + count + "개";
        GameManager.instance.inventory_Items.Add(new InventoryItem(name, count));
        int _gold = int.Parse(GameManager.instance.data.gold) - 10000;
        GameManager.instance.data.gold = _gold.ToString();
        GameManager.instance.Save("i");
        slot.isEmpty = true;

        endSceneCtrl.UIUpdate();
    }

    IEnumerator NoMoney()
    {
        noMoneyTxt.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        noMoneyTxt.gameObject.SetActive(false);
    }
}
