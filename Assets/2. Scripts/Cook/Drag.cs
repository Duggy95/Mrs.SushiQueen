using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform inventoryTr;  //인벤토리 위치
    CookInventory inventory;
    public Transform fishListTr;  //수족관 위치
    //public Transform cookListTr;  //회스크롤뷰 위치
    FishSlot fishSlot;
    FishData fishData;

    //CookManager cookManager;
    CanvasGroup canvasGroup;
    GameObject copiedSlot;
    //Transform itemTr;

    public static GameObject draggingItem = null;

    private void Awake()
    {
        inventoryTr = GameObject.Find("InventoryImg").GetComponent<Transform>();
        inventory = inventoryTr.GetComponentInParent<CookInventory>();
        fishListTr = GameObject.Find("FishContent").GetComponent<Transform>();
        //cookManager = GameObject.FindWithTag("MANAGER").GetComponent<CookManager>();
        //cookListTr = GameObject.Find("CookContent").GetComponent<Transform>();
    }

    void Start()
    {
        fishSlot = GetComponent<FishSlot>();
        //fishData = fishSlot.fishData;
        //itemTr = GetComponent<Transform>();
        //canvasGroup = GetComponent<CanvasGroup>();

        if(fishSlot.fish_Name == "")
        {
            Destroy(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        copiedSlot = Instantiate(this.gameObject);
        copiedSlot.transform.SetParent(inventoryTr);
        draggingItem = copiedSlot.gameObject;
        canvasGroup = copiedSlot.gameObject.GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;

        if (fishSlot.fish_Count >= 2)
        {
            fishSlot.fish_Count--;
            //fishSlot.UpdateSlot();
            inventory.UpdateUI(this.gameObject);
        }
        else if (fishSlot.fish_Count == 1)
        {
            fishSlot.ClearSlot();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        copiedSlot.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draggingItem = null;
        canvasGroup.blocksRaycasts = true;

        if (copiedSlot.transform.parent == inventoryTr)
        {
            if (fishSlot.fish_Count >= 1)
            {
                Destroy(copiedSlot.gameObject);
                fishSlot.fish_Count++;
                inventory.UpdateUI(this.gameObject);
            }
            else if (fishSlot.fish_Count == 0)
            {
                print("빈공간에서 호출");
                fishSlot.fish_Count = copiedSlot.GetComponent<FishSlot>().fish_Count;
                Image[] fishImage = fishSlot.gameObject.GetComponentsInChildren<Image>();
                Image[] slotImage = copiedSlot.gameObject.GetComponentsInChildren<Image>();
                foreach(Image image in fishImage)
                {
                    if(image.name.Contains("Img"))
                    {
                        image.sprite = slotImage[1].sprite;
                    }
                }
                //fishSlot.gameObject.GetComponentInChildren<Image>().sprite = copiedSlot.GetComponent<Image>().sprite;
                inventory.UpdateUI(this.gameObject);
                Destroy(copiedSlot.gameObject);
            }
        }
        else
        {
            if (fishSlot.fish_Count == 0)
            {
                Destroy(this);
            }
        }
    }
}
