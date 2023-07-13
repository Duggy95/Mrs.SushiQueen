using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class DragSushi : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Transform dishTr; //접시위치
    Transform sushiTr;  //초밥위치
    Transform boardTr;  //도마위치

    public static GameObject draggingSushi = null;

    //접시 범위
    float right;
    float left;
    float top;
    float bottom;

    bool isDish;

    void Awake()
    {
        dishTr = GameObject.Find("Dish_RawImage").GetComponent<Transform>();
        boardTr = GameObject.Find("Board_RawImage").GetComponent<Transform>();
    }

    void Start()
    {
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

        if (sushiTr.parent == boardTr && dishTr != null && Input.mousePosition.x > left &&
            Input.mousePosition.x < right && Input.mousePosition.y < top &&
            Input.mousePosition.y > bottom)
        {
            sushiTr.SetParent(dishTr);
            sushiTr.position = Input.mousePosition;
            Destroy(this);
        }
        else
        {
            sushiTr.SetParent(boardTr.transform);
            sushiTr.position = boardTr.position;
        }
    }
}
