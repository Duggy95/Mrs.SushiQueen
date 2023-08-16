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

    Image itemImgPrefab;
    Image fishImgPrefab;

    int itemSlotCount;
    int fishSlotCount;

    void Start()
    {

        itemSlotCount = int.Parse(GameManager.instance.data.itemCount);
        fishSlotCount = int.Parse(GameManager.instance.data.fishCount);
        // ������ â
        for (int i = 0; i < itemSlotCount; i++)
        {
            List<InventoryItem> itemList = GameManager.instance.inventory_Items;
            GameObject itemPrefab = Instantiate(itemSlotPrefab, itemContent.transform);
            ItemSlot item_Slot = itemPrefab.GetComponent<ItemSlot>();
            Image[] itemImgPrefabs = itemPrefab.GetComponentsInChildren<Image>();
            foreach (Image itemImg in itemImgPrefabs)
            {
                if (itemImg.name.Contains("Img"))
                    itemImgPrefab = itemImg;
            }

            if (itemList.Count > i)
            {
                string _itemName = itemList[i].item_Name;
                string _itemCount = itemList[i].item_Count;

                item_Slot.GetComponentInChildren<Text>().text = _itemName + "   " + _itemCount + "��";
                item_Slot.isEmpty = true;
                CanvasGroup slotCanvas = itemPrefab.GetComponent<CanvasGroup>();
                slotCanvas.interactable = true;

                if (_itemName == "������")
                    itemImgPrefab.GetComponentInChildren<Image>().sprite = Resources.Load("White", typeof(Sprite)) as Sprite;
                else if (_itemName == "����")
                    itemImgPrefab.GetComponentInChildren<Image>().sprite = Resources.Load("Red", typeof(Sprite)) as Sprite;
                else if (_itemName == "������")
                    itemImgPrefab.GetComponentInChildren<Image>().sprite = Resources.Load("Rare", typeof(Sprite)) as Sprite;
            }
        }
        // ������
        for (int i = 0; i < fishSlotCount; i++)
        {
            List<InventoryFish> fishList = GameManager.instance.inventory_Fishs;
            GameObject fishPrefab = Instantiate(fishSlotPrefab, fishContent.transform);
            FishSlot fish_Slot = fishPrefab.GetComponent<FishSlot>();
            Image[] fishSlotPrefabs = fishPrefab.GetComponentsInChildren<Image>();
            foreach (Image fishImg in fishSlotPrefabs)
            {
                if (fishImg.name.Contains("Img"))
                    fishImgPrefab = fishImg;
            }

            if (fishList.Count > i)
            {
                string _fishName = fishList[i].fish_Name;
                string _fishCount = fishList[i].fish_Count;
                CanvasGroup slotCanvas = fishPrefab.GetComponent<CanvasGroup>();
                slotCanvas.interactable = true;

                fish_Slot.GetComponentInChildren<Text>().text = _fishName + "   " + _fishCount + " " + "����";
                fish_Slot.isEmpty = true;

                if (_fishName == "����")
                    fishImgPrefab.GetComponent<Image>().sprite = Resources.Load("Flatfish", typeof(Sprite)) as Sprite;
                else if (_fishName == "����")
                    fishImgPrefab.GetComponent<Image>().sprite = Resources.Load("Salmon", typeof(Sprite)) as Sprite;
                else if (_fishName == "����")
                    fishImgPrefab.GetComponent<Image>().sprite = Resources.Load("Snapper", typeof(Sprite)) as Sprite;
                else if (_fishName == "��ġ")
                    fishImgPrefab.GetComponent<Image>().sprite = Resources.Load("Tuna", typeof(Sprite)) as Sprite;
                else if (_fishName == "��¡��")
                    fishImgPrefab.GetComponent<Image>().sprite = Resources.Load("Squid", typeof(Sprite)) as Sprite;
                else if (_fishName == "������")
                    fishImgPrefab.GetComponent<Image>().sprite = Resources.Load("Bigeye Trevally", typeof(Sprite)) as Sprite;
                else if (_fishName == "���")
                    fishImgPrefab.GetComponent<Image>().sprite = Resources.Load("Bass", typeof(Sprite)) as Sprite;
                else if (_fishName == "���")
                    fishImgPrefab.GetComponent<Image>().sprite = Resources.Load("Yellowtail", typeof(Sprite)) as Sprite;
                else if (_fishName == "û��")
                    fishImgPrefab.GetComponent<Image>().sprite = Resources.Load("Herring", typeof(Sprite)) as Sprite;
                else if (_fishName == "����")
                    fishImgPrefab.GetComponent<Image>().sprite = Resources.Load("Mackerel", typeof(Sprite)) as Sprite;
            }
        }
    }
}
