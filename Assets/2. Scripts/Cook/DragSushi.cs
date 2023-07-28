using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragSushi : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Transform dishTr; //������ġ
    Transform sushiTr;  //�ʹ���ġ
    Transform boardTr;  //������ġ

    public static GameObject draggingSushi = null;

    //���� ����
    float right;
    float left;
    float top;
    float bottom;

    bool isDish;

    void Awake()
    {
        dishTr = GameObject.Find("Dish_RawImage").GetComponent<Transform>();  //����
        boardTr = GameObject.Find("Board_RawImage").GetComponent<Transform>();  //����
    }

    void Start()
    {
        sushiTr = GetComponent<Transform>();

        //���� ����.
        right = dishTr.position.x + 200;
        left = dishTr.position.x - 200;
        top = dishTr.position.y + 125;
        bottom = dishTr.position.y - 125;
    }
    public void OnBeginDrag(PointerEventData eventData)  //�巡�� ������ ��.
    {
        if(sushiTr.parent != dishTr)  //�� ������Ʈ�� �θ� ���ð� �ƴ� ��
        {
            this.transform.SetParent(boardTr);  //�� ������Ʈ�� �θ� ������.
            draggingSushi = this.gameObject;  //���� �巡�� ���� �ʹ��� �� ������Ʈ
        }
        else  //������ ��
        {
            this.transform.SetParent(dishTr);  //�� ������Ʈ�� �θ� ���÷�.
            draggingSushi = this.gameObject;  //���� �巡�� ���� �ʹ��� �� ������Ʈ
        }
    }

    public void OnDrag(PointerEventData eventData)  //�巡�� ���� ��
    {
        // ���ð� �����ϰ� �ش� �������� �θ�
        // ���ð� �ƴ϶�� �巡�� �۵�
        if (dishTr == null && sushiTr.parent == dishTr)
            return;

        sushiTr.position = Input.mousePosition;  //�ʹ� ��ġ�� ���콺 ��ġ.
    }

    public void OnEndDrag(PointerEventData eventData)  //�巡�� ������ ��. ������ ��
    {
        draggingSushi = null;

        //�ʹ��� ���� �����ȿ� �ְ�, ���ð� null�� �ƴϰ�, �ʹ��� ���� ������Ʈ �ڽ����� ���� ��.
        if (sushiTr.parent == boardTr && dishTr != null && Input.mousePosition.x > left &&
            Input.mousePosition.x < right && Input.mousePosition.y < top &&
            Input.mousePosition.y > bottom)
        {
            sushiTr.SetParent(dishTr);  //�ʹ��� �θ� ���÷�.
            sushiTr.position = Input.mousePosition;  //��ġ�� ���콺�� ��ġ.
            Destroy(this);  //�巡�� �ȵǵ��� ��ũ��Ʈ ����.

            Dish dish = dishTr.GetComponentInParent<Dish>();  //����
            Sushi sushi = sushiTr.GetComponent<Sushi>();  //�ʹ�
            dish.AddSushi(sushi.sushiName, sushi.wasabi, sushi.gold);  //�ʹ� ���� �ʹ� ��ųʸ��� �߰�.
        }
        else
        {
            sushiTr.SetParent(boardTr.transform);
            sushiTr.position = boardTr.position; 
        }
    }
}
