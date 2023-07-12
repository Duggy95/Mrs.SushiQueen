using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform inventoryTr;  //�κ��丮 ��ġ
    public Transform fishListTr;  //��������Ʈ ��ġ
    public Transform cookListTr;  //�丮����Ʈ ��ġ
    public Transform[] cookChildTr;  //----

    //ȸ ��ũ�� �� �����̴�.
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
        // ȸ ��ũ�Ѻ䰡 �����ϰ� �ش� �������� �θ�
        // �丮 ��ũ�Ѻ䰡 �ƴ϶�� �巡�� �۵�
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

        // �������� �θ� �κ��丮�̰� ��ġ�� ȸ ��ũ�Ѻ� ����� 
        // ȸ ��ũ�Ѻ� �ڽ����� �ְ� ��ũ��Ʈ ����
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
