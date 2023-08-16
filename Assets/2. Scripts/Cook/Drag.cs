using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform inventoryTr;  //�κ��丮 ��ġ
    CookInventory inventory;  //�κ��丮
    public Transform fishListTr;  //������ ��ġ
    FishSlot fishSlot;
    FishData fishData;
    AudioSource audioSource;
    CanvasGroup canvasGroup;
    GameObject copiedSlot;

    public static GameObject draggingItem = null;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        inventoryTr = GameObject.Find("InventoryImg").GetComponent<Transform>();
        inventory = inventoryTr.GetComponentInParent<CookInventory>();
        fishListTr = GameObject.Find("FishContent").GetComponent<Transform>();
    }

    void Start()
    {
        fishSlot = GetComponent<FishSlot>();
        //fishData = fishSlot.fishData;
    }

    public void OnBeginDrag(PointerEventData eventData)  //�巡�� ����
    {
        copiedSlot = Instantiate(this.gameObject);  //������Ʈ ����
        copiedSlot.transform.SetParent(inventoryTr);
        draggingItem = copiedSlot.gameObject;
        canvasGroup = copiedSlot.gameObject.GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;

        if (fishSlot.fish_Count >= 2)  //2�� �̻� ��������
        {
            fishSlot.fish_Count--;
            inventory.UpdateUI(this.gameObject);
        }
        else if (fishSlot.fish_Count == 1)  //1���� ��
        {
            fishSlot.ClearSlot();  //��ĭ���� �����
        }
    }

    public void OnDrag(PointerEventData eventData)  //�巡�� ��
    {
        copiedSlot.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)  //�巡�� ������ ��
    {
        draggingItem = null;
        canvasGroup.blocksRaycasts = true;

        if (copiedSlot.transform.parent == inventoryTr)  //���纻�� �θ� �κ��丮��� 
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

            if (fishSlot.fish_Count >= 1)  //������ ������ 1�̻��� ��
            {
                Destroy(copiedSlot.gameObject);  //���纻 ����
                fishSlot.fish_Count++;  //���� 1����
                inventory.UpdateUI(this.gameObject);
            }
            else if (fishSlot.fish_Count == 0)  //������ ��ĭ�� ��
            {
                //���纻�� ������ ��ĭ�� ä���
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
                inventory.UpdateUI(this.gameObject);
                Destroy(copiedSlot.gameObject);  //���纻 ����
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
