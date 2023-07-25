using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform inventoryTr;  //인벤토리 위치
    public Transform fishListTr;  //수족관 위치
    //public Transform cookListTr;  //회스크롤뷰 위치
    FishSlot fishSlot;

    CanvasGroup canvasGroup;
    CanvasGroup inventoryCanvasGroup;
    Transform itemTr;

    public static GameObject draggingItem = null;

    private void Awake()
    {
        string currentScene = SceneManager.GetActiveScene().name;  //현재 씬

        //요리 씬일때만 동작하도록 함.
        if (currentScene == "Cook")
        {
            inventoryTr = GameObject.Find("InventoryImg").GetComponent<Transform>();
            fishListTr = GameObject.Find("FishContent").GetComponent<Transform>();
            //cookListTr = GameObject.Find("CookContent").GetComponent<Transform>();
        }
        else
        {
            Destroy(this);  //스크립트 삭제.
        }
    }

    void Start()
    {
        fishSlot = GetComponent<FishSlot>();
        itemTr = GetComponent<Transform>();
        canvasGroup = GetComponent<CanvasGroup>();
        inventoryCanvasGroup = inventoryTr.GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.transform.SetParent(inventoryTr);
        draggingItem = this.gameObject;
        canvasGroup.blocksRaycasts = false;

        fishSlot.UpdateData();
    }

    public void OnDrag(PointerEventData eventData)
    {
        itemTr.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draggingItem = null;
        canvasGroup.blocksRaycasts = true;
        if (itemTr.parent == inventoryTr)
        {
            itemTr.SetParent(fishListTr.transform);

        }
    }
}
