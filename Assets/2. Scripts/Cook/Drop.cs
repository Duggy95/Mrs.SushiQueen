using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    NetaButton netaBtn;

    void Start()
    {
        netaBtn = GetComponent<NetaButton>();
    }

    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 1)
        {
            Drag.draggingItem.transform.SetParent(this.transform);
            this.gameObject.GetComponent<Image>().sprite = Drag.draggingItem.GetComponent<Image>().sprite;
            netaBtn.fishData = Drag.draggingItem.GetComponent<FishSlot>().fishData;
            Destroy(Drag.draggingItem.gameObject);
        }
    }
}
