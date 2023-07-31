using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class Drop : MonoBehaviour, IDropHandler
{
    Text text;
    int count = 0;
    NetaButton netaBtn;
    CookManager cookManager;
    public GameObject fishIconPrefab;
    Transform fishIconContent;

    void Start()
    {
        text = GetComponentInChildren<Text>();
        netaBtn = GetComponent<NetaButton>();
        cookManager = GameObject.FindWithTag("MANAGER").GetComponent<CookManager>();
        fishIconContent = GameObject.FindWithTag("CONTENT").GetComponent<Transform>();
    }

    void Update()
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        if (Drag.draggingItem == null)
            return;

        print("µå¶ø");
        if (transform.childCount == 1 && netaBtn.isEmpty)
        {
            Drag.draggingItem.transform.SetParent(this.transform);
            FishData fishData = Drag.draggingItem.GetComponent<FishSlot>().fishData;
            FishSlot fishSlot = Drag.draggingItem.GetComponent<FishSlot>();
            if (cookManager.fishList.Contains(fishData.fishName))
            {
                Destroy(Drag.draggingItem.gameObject);
                return;
            }
            else
            {
                cookManager.fishList.Add(fishData.fishName);

                this.gameObject.GetComponent<Image>().sprite = fishData.netaImg;
                Drag.draggingItem.GetComponent<FishSlot>().UpdateData();
                count += 5;
                text.text = fishData.fishName + "     " + count.ToString();
                netaBtn.fishData = fishData;
                netaBtn.count = count;
                netaBtn.isEmpty = false;
                Destroy(Drag.draggingItem.gameObject);

                GameObject fishIcon = Instantiate(fishIconPrefab, fishIconContent.transform);
                fishIcon.GetComponent<Image>().sprite = fishData.fishImg;
            }
        }
        else if(transform.childCount == 1 && !netaBtn.isEmpty)
        {
            FishData fishData = Drag.draggingItem.GetComponent<FishSlot>().fishData;
            if (netaBtn.fishData.fishName == fishData.fishName)
            {
                Drag.draggingItem.transform.SetParent(this.transform);
                Drag.draggingItem.GetComponent<FishSlot>().UpdateData();
                count += 5;
                netaBtn.count = count;
                netaBtn.UpdateUI();
                Destroy(Drag.draggingItem.gameObject);
            }
            else
                return;
        }
    }
}
