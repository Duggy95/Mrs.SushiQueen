using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject itemContent;
    public GameObject fishContent;
    public GameObject itemSlotPrefab;
    public GameObject fishSlotPrefab;

    int itemSlotCount;
    int fishSlotCount;

    void Start()
    {
        itemSlotCount = int.Parse(GameManager.instance.data.itemCount);
        fishSlotCount = int.Parse(GameManager.instance.data.fishCount);

        for (int i = 0; i < itemSlotCount; i++)
        {
            GameObject itemPrefab = Instantiate(itemSlotPrefab, itemContent.transform);
            ItemSlot item_Slot = itemPrefab.GetComponent<ItemSlot>();

            if (GameManager.instance.inventory_Items.Count > i)
            {
                string _itemName = GameManager.instance.inventory_Items[i].item_Name;
                string _itemCount = GameManager.instance.inventory_Items[i].item_Count;
                /*GameManager.instance.logTxt.text = "_item : " + _itemName;
                GameManager.instance.saveTxt.text = "itemDataCount : " + GameManager.instance.inventory_Items.Count;*/

                item_Slot.GetComponentInChildren<Text>().text = _itemName + "   " + _itemCount + "개";
                item_Slot.isEmpty = true;

                if (_itemName == "지렁이")
                    item_Slot.GetComponent<Image>().sprite = Resources.Load("White", typeof(Sprite)) as Sprite;
                else if (_itemName == "새우")
                    item_Slot.GetComponent<Image>().sprite = Resources.Load("Red", typeof(Sprite)) as Sprite;
                else if (_itemName == "생선살")
                    item_Slot.GetComponent<Image>().sprite = Resources.Load("Rare", typeof(Sprite)) as Sprite;
            }
        }

        for (int i = 0; i < fishSlotCount; i++)
        {
            GameObject fishPrefab = Instantiate(fishSlotPrefab, fishContent.transform);
            FishSlot fish_Slot = fishPrefab.GetComponent<FishSlot>();

            if (GameManager.instance.inventory_Fishs.Count > i)
            {
                string _fishName = GameManager.instance.inventory_Fishs[i].fish_Name;
                string _fishCount = GameManager.instance.inventory_Fishs[i].fish_Count;
                // GameManager.instance.logTxt.text = "_fish : " + _fishName;

                fish_Slot.GetComponentInChildren<Text>().text = _fishName + "   " + _fishCount + "마리";
                fish_Slot.isEmpty = true;

                if (_fishName == "광어")
                    fish_Slot.GetComponent<Image>().sprite = Resources.Load("Flatfish", typeof(Sprite)) as Sprite;
                else if (_fishName == "연어")
                    fish_Slot.GetComponent<Image>().sprite = Resources.Load("Salmon", typeof(Sprite)) as Sprite;
                else if (_fishName == "도미")
                    fish_Slot.GetComponent<Image>().sprite = Resources.Load("Snapper", typeof(Sprite)) as Sprite;
                else if (_fishName == "참치")
                    fish_Slot.GetComponent<Image>().sprite = Resources.Load("Tuna", typeof(Sprite)) as Sprite;
            }
        }
    }
}
