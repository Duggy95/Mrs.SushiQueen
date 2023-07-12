using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform inventoryTr;  //인벤토리 위치
    public Transform fishListTr;  //생선리스트 위치
    public Transform cookListTr;  //요리리스트 위치
    public Transform[] cookChildTr;  //----

    //회 스크롤 뷰 범위이다.
    float right;
    float left;
    float top;
    float bottom;
    CanvasGroup canvasGroup;
    CanvasGroup inventoryCanvasGroup;
    Transform itemTr;

    public static GameObject draggingItem = null;

    private void Awake()
    {
        inventoryTr = GameObject.Find("InventoryImg").GetComponent<Transform>();
        fishListTr = GameObject.Find("FishContent").GetComponent<Transform>();
        cookListTr = GameObject.Find("CookContent").GetComponent<Transform>();
    }

    void Start()
    {
        itemTr = GetComponentInChildren<Transform>();
        inventoryCanvasGroup = inventoryTr.GetComponent<CanvasGroup>();
        canvasGroup = GetComponent<CanvasGroup>();
        cookChildTr = cookListTr.GetComponentsInChildren<Transform>();

        right = cookListTr.position.x + 535;
        left = cookListTr.position.x - 535;
        top = cookListTr.position.y + 140;
        bottom = cookListTr.position.y - 140;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 회 스크롤뷰가 존재하고 해당 아이템의 부모가
        // 요리 스크롤뷰가 아니라면 드래그 작동
        if (cookListTr == null && itemTr.parent == cookListTr)
            return;

        itemTr.position = Input.mousePosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.transform.SetParent(inventoryTr);
        draggingItem = this.gameObject;
        canvasGroup.blocksRaycasts = false;
        inventoryCanvasGroup.alpha = 0.3f;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draggingItem = null;
        canvasGroup.blocksRaycasts = true;
        inventoryCanvasGroup.alpha = 1f;

        // 아이템의 부모가 인벤토리이고 위치가 회 스크롤뷰 위라면 
        // 회 스크롤뷰 자식으로 넣고 스크립트 제거
        if (itemTr.parent == inventoryTr && Input.mousePosition.x > left &&
            Input.mousePosition.x < right && Input.mousePosition.y < top &&
            Input.mousePosition.y > bottom)
        {
            for (int i = 0; i < cookChildTr.Length; i++)
            {
                if (cookChildTr[i].childCount == 1)
                {
                    itemTr.SetParent(cookChildTr[i].transform);
                    itemTr.position = cookChildTr[i].position;
                    Destroy(this);
                    break;
                }
            }

            if(itemTr.parent == inventoryTr)
            {
                itemTr.SetParent(fishListTr.transform);
            }
        }

        else
        {
            itemTr.SetParent(fishListTr.transform);
        }
    }
}
