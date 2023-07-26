using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookInventory : MonoBehaviour
{
    public GameObject fishContent;
    public GameObject sushiContent;
    public GameObject sushiPrefab;
    public GameObject fishSlotPrefab;

    int fishSlotCount;
    int sushiCount;

    void Start()
    {
        fishSlotCount = int.Parse(GameManager.instance.data.fishCount);
        sushiCount = int.Parse(GameManager.instance.data.cookCount);

        for (int i = 0; i < fishSlotCount; i++)
        {
            GameObject fishPrefab = Instantiate(fishSlotPrefab, fishContent.transform);
            FishSlot fish_Slot = fishPrefab.GetComponent<FishSlot>();

            if (GameManager.instance.inventory_Fishs.Count > i)
            {
                string _fishName = GameManager.instance.inventory_Fishs[i].fish_Name;
                string _fishCount = GameManager.instance.inventory_Fishs[i].fish_Count;

                fish_Slot.GetComponentInChildren<Text>().text = _fishName + "   " + _fishCount + " " + "마리";
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

        for(int i = 0; i < sushiCount; i++)
        {
            GameObject sushiBtn = Instantiate(sushiPrefab, sushiContent.transform);
        }
    }

    public void UpdateUI(GameObject gameObj)
    {
        FishSlot fishSlot = gameObj.GetComponent<FishSlot>();
        Text text = gameObj.GetComponentInChildren<Text>();
        text.text = fishSlot.fish_Name + "   " + fishSlot.fish_Count + " " + "마리";

    }
}
