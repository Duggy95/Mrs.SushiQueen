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

                    fish_Slot.GetComponentInChildren<Text>().text = _fishName + "   " + _fishCount + " " + "����";
                    fish_Slot.isEmpty = true;

                    if(_fishName =="")
                    {
                        print("�̰� �����ϴ�?");
                        Destroy(fishPrefab.GetComponent<Drag>());
                    }

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

                if (fish_Slot.GetComponentInChildren<Text>().text.Split(" ")[0] == "")
                {
                    print("�̰� �����ϴ�?");
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
        text.text = fishSlot.fish_Name + "   " + fishSlot.fish_Count + " " + "����";
    }
}
