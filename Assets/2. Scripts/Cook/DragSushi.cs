using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

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
        dishTr = GameObject.Find("Dish_RawImage").GetComponent<Transform>();  //접시
        boardTr = GameObject.Find("Board_RawImage").GetComponent<Transform>();  //도마
    }

    void Start()
    {
        sushiTr = GetComponent<Transform>();

        //접시 범위.
        right = dishTr.position.x + 200;
        left = dishTr.position.x - 200;
        top = dishTr.position.y + 125;
        bottom = dishTr.position.y - 125;
    }
    public void OnBeginDrag(PointerEventData eventData)  //드래그 시작할 때.
    {
        if(sushiTr.parent != dishTr)  //이 오브젝트의 부모가 접시가 아닐 때
        {
            this.transform.SetParent(boardTr);  //이 오브젝트의 부모를 도마로.
            draggingSushi = this.gameObject;  //현재 드래그 중인 초밥은 이 오브젝트
        }
        else  //접시일 때
        {
            this.transform.SetParent(dishTr);  //이 오브젝트의 부모를 접시로.
            draggingSushi = this.gameObject;  //현재 드래그 중인 초밥은 이 오브젝트
        }
    }

    public void OnDrag(PointerEventData eventData)  //드래그 중일 때
    {
        // 접시가 존재하고 해당 아이템의 부모가
        // 접시가 아니라면 드래그 작동
        if (dishTr == null && sushiTr.parent == dishTr)
            return;

        sushiTr.position = Input.mousePosition;  //초밥 위치는 마우스 위치.
    }

    public void OnEndDrag(PointerEventData eventData)  //드래그 끝났을 때. 놓았을 때
    {
        draggingSushi = null;

        //초밥이 도마 범위안에 있고, 접시가 null이 아니고, 초밥이 도마 오브젝트 자식으로 있을 때.
        if (sushiTr.parent == boardTr && dishTr != null && Input.mousePosition.x > left &&
            Input.mousePosition.x < right && Input.mousePosition.y < top &&
            Input.mousePosition.y > bottom)
        {
            sushiTr.SetParent(dishTr);  //초밥의 부모를 접시로.
            sushiTr.position = Input.mousePosition;  //위치는 마우스의 위치.
            Destroy(this);  //드래그 안되도록 스크립트 삭제.

            Dish dish = dishTr.GetComponentInParent<Dish>();  //접시
            Sushi sushi = sushiTr.GetComponent<Sushi>();  //초밥
            dish.AddSushi(sushi.sushiName, sushi.wasabi, sushi.gold);  //초밥 정보 초밥 딕셔너리에 추가.
        }
        else
        {
            sushiTr.SetParent(boardTr.transform);
            sushiTr.position = boardTr.position; 
        }
    }
}
