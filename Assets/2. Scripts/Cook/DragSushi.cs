using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class DragSushi : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Transform dishTr; //접시위치
    Transform sushiTr;
    Transform boardTr;
    CanvasGroup canvasGroup;

    public static GameObject draggingSushi = null;

    //접시 범위
    float right;
    float left;
    float top;
    float bottom;

    void Awake()
    {
        dishTr = GameObject.Find("Dish_RawImage").GetComponent<Transform>();
        boardTr = GameObject.Find("Board_RawImage").GetComponent<Transform>();
    }

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        sushiTr = GetComponent<Transform>();

        right = dishTr.position.x + 200;
        left = dishTr.position.x - 200;
        top = dishTr.position.y + 200;
        bottom = dishTr.position.y - 200;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        this.transform.SetParent(boardTr);
        draggingSushi = this.gameObject;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 접시가 존재하고 해당 아이템의 부모가
        // 접시가 아니라면 드래그 작동
        if (dishTr == null && sushiTr.parent == dishTr)
            return;

        sushiTr.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draggingSushi = null;
        canvasGroup.blocksRaycasts = true;

        if (sushiTr.parent == boardTr && Input.mousePosition.x > left &&
            Input.mousePosition.x < right && Input.mousePosition.y < top &&
            Input.mousePosition.y > bottom)
        {
            sushiTr.SetParent(dishTr);
            sushiTr.position = dishTr.position;
        }
        else
        {
            sushiTr.SetParent(boardTr.transform);
        }
    }
}
