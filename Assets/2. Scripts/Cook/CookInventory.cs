using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CookInventory : MonoBehaviour
{
    public GameObject fishContent;
    public GameObject sushiContent;
    public GameObject sushiPrefab;
    public GameObject fishSlotPrefab;
    Scene currentScene;

    Image fishImgPrefab;
    int fishSlotCount;
    int sushiCount;

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();

        if (currentScene.buildIndex == 2)
        {
            fishSlotCount = int.Parse(GameManager.instance.data.fishCount);
            sushiCount = int.Parse(GameManager.instance.data.cookCount);

            for (int i = 0; i < fishSlotCount; i++)
            {
                GameObject fishPrefab = Instantiate(fishSlotPrefab, fishContent.transform);
                FishSlot fish_Slot = fishPrefab.GetComponent<FishSlot>();
                Image[] fishSlotPrefabs = fishPrefab.GetComponentsInChildren<Image>();
                foreach (Image fishImg in fishSlotPrefabs)
                {
                    if (fishImg.name.Contains("Img"))
                        fishImgPrefab = fishImg;
                }

                if (GameManager.instance.inventory_Fishs.Count > i)
                {
                    string _fishName = GameManager.instance.inventory_Fishs[i].fish_Name;
                    string _fishCount = GameManager.instance.inventory_Fishs[i].fish_Count;

                    fish_Slot.GetComponentInChildren<Text>().text = _fishName + "   " + _fishCount + " " + "마리";
                    fish_Slot.isEmpty = true;

                    if(_fishName =="")
                    {
                        print("이거 동작하니?");
                        Destroy(fishPrefab.GetComponent<Drag>());
                    }

                    if (_fishName == "광어")
                        fishImgPrefab.GetComponent<Image>().sprite = Resources.Load("Flatfish", typeof(Sprite)) as Sprite;
                    else if (_fishName == "연어")
                        fishImgPrefab.GetComponent<Image>().sprite = Resources.Load("Salmon", typeof(Sprite)) as Sprite;
                    else if (_fishName == "도미")
                        fishImgPrefab.GetComponent<Image>().sprite = Resources.Load("Snapper", typeof(Sprite)) as Sprite;
                    else if (_fishName == "참치")
                        fishImgPrefab.GetComponent<Image>().sprite = Resources.Load("Tuna", typeof(Sprite)) as Sprite;
                    else if (_fishName == "오징어")
                        fishImgPrefab.GetComponent<Image>().sprite = Resources.Load("Squid", typeof(Sprite)) as Sprite;
                    else if (_fishName == "전갱이")
                        fishImgPrefab.GetComponent<Image>().sprite = Resources.Load("Bigeye Trevally", typeof(Sprite)) as Sprite;
                    else if (_fishName == "농어")
                        fishImgPrefab.GetComponent<Image>().sprite = Resources.Load("Bass", typeof(Sprite)) as Sprite;
                    else if (_fishName == "방어")
                        fishImgPrefab.GetComponent<Image>().sprite = Resources.Load("Yellowtail", typeof(Sprite)) as Sprite;
                    else if (_fishName == "청어")
                        fishImgPrefab.GetComponent<Image>().sprite = Resources.Load("Herring", typeof(Sprite)) as Sprite;
                    else if (_fishName == "고등어")
                        fishImgPrefab.GetComponent<Image>().sprite = Resources.Load("Mackerel", typeof(Sprite)) as Sprite;
                }

                if (fish_Slot.GetComponentInChildren<Text>().text.Split(" ")[0] == "")
                {
                    print("이거 동작하니?");
                    Destroy(fishPrefab.GetComponent<Drag>());
                }
            }

            for (int i = 0; i < sushiCount; i++)
            {
                GameObject sushiBtn = Instantiate(sushiPrefab, sushiContent.transform);
            }
        }
    }

    public void UpdateUI(GameObject gameObj)
    {
        FishSlot fishSlot = gameObj.GetComponent<FishSlot>();
        Text text = gameObj.GetComponentInChildren<Text>();
        text.text = fishSlot.fish_Name + "   " + fishSlot.fish_Count + " " + "마리";
    }
}
