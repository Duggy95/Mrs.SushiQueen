using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookInventory : MonoBehaviour
{
    public GameObject fishContent;
    public GameObject fishSlotPrefab;

    int fishSlotCount;

    void Start()
    {
        fishSlotCount = int.Parse(GameManager.instance.data.fishCount);

        for (int i = 0; i < fishSlotCount; i++)
        {
            GameObject fishPrefab = Instantiate(fishSlotPrefab, fishContent.transform);
            FishSlot fish_Slot = fishPrefab.GetComponent<FishSlot>();

            if (GameManager.instance.inventory_Fishs.Count > i)
            {
                string _fishName = GameManager.instance.inventory_Fishs[i].fish_Name;
                string _fishCount = GameManager.instance.inventory_Fishs[i].fish_Count;

                fish_Slot.GetComponentInChildren<Text>().text = _fishName + "   " + _fishCount + " " +"마리";
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
