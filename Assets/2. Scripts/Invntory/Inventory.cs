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
        /*itemSlotCount = int.Parse(GameManager.instance.save[0].itemCount);
        fishSlotCount = int.Parse(GameManager.instance.save[1].fishCount);*/
        itemSlotCount = int.Parse(GameManager.instance.data.itemCount);
        fishSlotCount = int.Parse(GameManager.instance.data.fishCount);

        for (int i = 0; i < itemSlotCount; i++)
        {
            ItemSlot item_Slot = Instantiate(itemSlotPrefab, itemContent.transform).GetComponent<ItemSlot>();
            //SetItem(item_Slot);
            if (GameManager.instance.inventory_Items.Count > i)
            {
                string _item = GameManager.instance.inventory_Items[i].item_Name;
                item_Slot.GetComponentInChildren<Text>().text = _item + "   " + GameManager.instance.inventory_Items[i].item_Count + "��";

                if (_item == "������")
                    item_Slot.GetComponent<Image>().sprite = Resources.Load("White", typeof(Sprite)) as Sprite;
                else if (_item == "����")
                    item_Slot.GetComponent<Image>().sprite = Resources.Load("Red", typeof(Sprite)) as Sprite;
                else if (_item == "������")
                    item_Slot.GetComponent<Image>().sprite = Resources.Load("Rare", typeof(Sprite)) as Sprite;
            }
        }

        for (int i = 0; i < fishSlotCount; i++)
        {
            FishSlot fish_Slot = Instantiate(fishSlotPrefab, fishContent.transform).GetComponent<FishSlot>();
            //SetFish(i, fish_Slot);

            if (GameManager.instance.inventory_Fishs.Count > i)
            {
                string _fish = GameManager.instance.inventory_Fishs[i].fish_Name;
                fish_Slot.GetComponentInChildren<Text>().text = _fish;

                if (_fish == "����")
                    fish_Slot.GetComponent<Image>().sprite = Resources.Load("Flatfish", typeof(Sprite)) as Sprite;
                else if (_fish == "����")
                    fish_Slot.GetComponent<Image>().sprite = Resources.Load("Salmon", typeof(Sprite)) as Sprite;
                else if (_fish == "����")
                    fish_Slot.GetComponent<Image>().sprite = Resources.Load("Snapper", typeof(Sprite)) as Sprite;
                else if (_fish == "��ġ")
                    fish_Slot.GetComponent<Image>().sprite = Resources.Load("Tuna", typeof(Sprite)) as Sprite;
            }
        }
    }
}
