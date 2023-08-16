using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform inventoryTr;  //인벤토리 위치
    CookInventory inventory;  //인벤토리
    public Transform fishListTr;  //수족관 위치
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

    public void OnBeginDrag(PointerEventData eventData)  //드래그 시작
    {
        copiedSlot = Instantiate(this.gameObject);  //오브젝트 복사
        copiedSlot.transform.SetParent(inventoryTr);
        draggingItem = copiedSlot.gameObject;
        canvasGroup = copiedSlot.gameObject.GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;

        if (fishSlot.fish_Count >= 2)  //2개 이상 남았을때
        {
            fishSlot.fish_Count--;
            inventory.UpdateUI(this.gameObject);
        }
        else if (fishSlot.fish_Count == 1)  //1개일 때
        {
            fishSlot.ClearSlot();  //빈칸으로 만들기
        }
    }

    public void OnDrag(PointerEventData eventData)  //드래그 중
    {
        copiedSlot.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)  //드래그 끝났을 때
    {
        draggingItem = null;
        canvasGroup.blocksRaycasts = true;

        if (copiedSlot.transform.parent == inventoryTr)  //복사본의 부모가 인벤토리라면 
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

            if (fishSlot.fish_Count >= 1)  //슬롯의 갯수가 1이상일 때
            {
                Destroy(copiedSlot.gameObject);  //복사본 삭제
                fishSlot.fish_Count++;  //갯수 1증가
                inventory.UpdateUI(this.gameObject);
            }
            else if (fishSlot.fish_Count == 0)  //슬롯이 빈칸일 때
            {
                //복사본의 정보로 빈칸을 채우기
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
                Destroy(copiedSlot.gameObject);  //복사본 삭제
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
