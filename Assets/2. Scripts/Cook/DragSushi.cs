using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragSushi : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Transform dishTr; //접시위치
    CanvasGroup canvasGroup;

    public static GameObject draggingSushi = null;

    //접시 범위
    float right;
    float left;
    float top;
    float bottom;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        right = dishTr.position.x + 200;
        left = dishTr.position.x - 200;
        top = dishTr.position.y + 200;
        bottom = dishTr.position.y - 200;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}
