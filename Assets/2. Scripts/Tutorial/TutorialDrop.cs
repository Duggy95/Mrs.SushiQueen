using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialDrop : MonoBehaviour, IDropHandler
{
    Text text;
    int count = 0;
    TutorialNetaBtn netaBtn;
    public TutorialCook tc;
    public GameObject fishIconPrefab;
    public Transform fishIconContent;

    void Start()
    {
        text = GetComponentInChildren<Text>();
        netaBtn = GetComponent<TutorialNetaBtn>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (Drag.draggingItem == null)
            return;

        FishData fishData = Drag.draggingItem.GetComponent<FishSlot>().fishData;
        FishSlot fishSlot = Drag.draggingItem.GetComponent<FishSlot>();

        if (!text.text.Contains(fishData.fishName) && tc.fishList.Contains(fishData.fishName))
            return;

        if (transform.childCount == 2 && netaBtn.isEmpty)
        {
            print("µå¶ø");
            Drag.draggingItem.transform.SetParent(this.transform);

            this.gameObject.GetComponentInChildren<Image>().sprite = fishData.netaImg;
            //fishSlot.UpdateData();
            count += 5;
            text.text = fishData.fishName + "     " + count.ToString();
            netaBtn.fishData = fishData;
            netaBtn.count = count;
            netaBtn.isEmpty = false;
            Destroy(Drag.draggingItem.gameObject);

            GameObject fishIcon = Instantiate(fishIconPrefab, fishIconContent.transform);
            fishIcon.GetComponentsInChildren<Image>()[1].sprite = fishData.netaImg;
            Text iconText = fishIcon.GetComponentInChildren<Text>();
            iconText.text = fishData.fishName + "     " + count;
            netaBtn.Txt(iconText);
            tc.fishList.Add(fishData.fishName);
        }
        else if (transform.childCount == 2 && !netaBtn.isEmpty)
        {
            fishData = Drag.draggingItem.GetComponent<FishSlot>().fishData;
            if (netaBtn.fishData.fishName == fishData.fishName)
            {
                Drag.draggingItem.transform.SetParent(this.transform);
                //Drag.draggingItem.GetComponent<FishSlot>().UpdateData();
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
