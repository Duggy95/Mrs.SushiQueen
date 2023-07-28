using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform inventoryTr;  //�κ��丮 ��ġ
    CookInventory inventory;
    public Transform fishListTr;  //������ ��ġ
    //public Transform cookListTr;  //ȸ��ũ�Ѻ� ��ġ
    FishSlot fishSlot;

    CanvasGroup canvasGroup;
    GameObject copiedSlot;
    Transform itemTr;

    public static GameObject draggingItem = null;

    private void Awake()
    {
        //�丮 ���϶��� �����ϵ��� ��.

        inventoryTr = GameObject.Find("InventoryImg").GetComponent<Transform>();
        inventory = inventoryTr.GetComponentInParent<CookInventory>();
        fishListTr = GameObject.Find("FishContent").GetComponent<Transform>();
        //cookListTr = GameObject.Find("CookContent").GetComponent<Transform>();

    }

    void Start()
    {
        fishSlot = GetComponent<FishSlot>();
        itemTr = GetComponent<Transform>();
        //canvasGroup = GetComponent<CanvasGroup>();

        /*if(fishSlot.fish_Count == 0)
        {
            Destroy(this);
        }*/
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

        //fishSlot.UpdateData();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //itemTr.position = Input.mousePosition;
        copiedSlot.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draggingItem = null;
        canvasGroup.blocksRaycasts = true;
        /*if (itemTr.parent == inventoryTr)
        {
            itemTr.SetParent(fishListTr.transform);
        }*/

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
                print("��������� ȣ��");
                fishSlot.fish_Count = copiedSlot.GetComponent<FishSlot>().fish_Count;
                fishSlot.gameObject.GetComponent<Image>().sprite = copiedSlot.GetComponent<Image>().sprite;
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
