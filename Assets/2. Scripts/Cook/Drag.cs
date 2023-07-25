using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform inventoryTr;  //�κ��丮 ��ġ
    public Transform fishListTr;  //������ ��ġ
    //public Transform cookListTr;  //ȸ��ũ�Ѻ� ��ġ
    FishSlot fishSlot;

    CanvasGroup canvasGroup;
    CanvasGroup inventoryCanvasGroup;
    Transform itemTr;

    public static GameObject draggingItem = null;

    private void Awake()
    {
        string currentScene = SceneManager.GetActiveScene().name;  //���� ��

        //�丮 ���϶��� �����ϵ��� ��.
        if (currentScene == "Cook")
        {
            inventoryTr = GameObject.Find("InventoryImg").GetComponent<Transform>();
            fishListTr = GameObject.Find("FishContent").GetComponent<Transform>();
            //cookListTr = GameObject.Find("CookContent").GetComponent<Transform>();
        }
        else
        {
            Destroy(this);  //��ũ��Ʈ ����.
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
