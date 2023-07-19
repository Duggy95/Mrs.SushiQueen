using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasabiButton : MonoBehaviour
{
    public GameObject wasabi;  //와사비 프리팹
    public GameObject wasabi1;  //큰 와사비 프리팹
    public GameObject board;  //도마

    public void WasabiBtn()
    {
        if (board == null)  //도마가 없으면
        {
            GameObject board = GameObject.Find("Board_RawImage");  //도마 오브젝트 찾기
        }

        Transform riceTr = board.transform.Find("Rice(Clone)").transform;  //도마 오브젝트의 자식 오브젝트 찾기.
        if(riceTr != null)
        {
            Vector3 wasabiTr = new Vector3(riceTr.position.x, riceTr.position.y + 10, 0);  //밥 조금 위쪽
            Sushi sushi = riceTr.GetComponent<Sushi>();  //초밥 가져오기.

            if (sushi.wasabi == "없이" && riceTr.childCount == 0)  //초밥의 와사비가 "없이" 이고 자식이 0일 때
            {
                GameObject Wasabi = Instantiate(wasabi, wasabiTr, Quaternion.identity, riceTr);  //와사비 생성.
                sushi.wasabi = "보통으로";  //초밥에 와사비 값 저장.
            }
            else if (sushi.wasabi == "보통으로" && riceTr.childCount == 1)  //초밥의 와사비가 보통이고 자식이 1일 때
            {
                GameObject Wasabi1 = Instantiate(wasabi1, wasabiTr, Quaternion.identity, riceTr);  //와사비 생성.
                sushi.wasabi = "많이 넣어서";  //초밥에 와사비 값 저장.
            }
        }
    }
}
