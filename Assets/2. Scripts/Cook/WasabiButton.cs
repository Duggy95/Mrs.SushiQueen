using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasabiButton : MonoBehaviour
{
    public GameObject wasabi;  //와사비 프리팹
    public GameObject wasabi1;  //큰 와사비 프리팹
    public GameObject board;  //도마
    GameObject Wasabi;
    GameObject Wasabi1;
    bool isWasabi;

    public void WasabiBtn()
    {
        if (board == null)
        {
            GameObject board = GameObject.Find("Board_RawImage");  //보드 오브젝트 찾기
        }

        if(Wasabi == null)
        {
            Transform riceTr = board.transform.Find("Rice(Clone)").transform;  //보드 오브젝트의 자식 오브젝트 찾기.
            Vector3 wasabiTr = new Vector3(riceTr.position.x, riceTr.position.y + 10, 0);  //밥 조금 위쪽
            Wasabi = Instantiate(wasabi, wasabiTr, Quaternion.identity, riceTr);  //와사비 생성.

            Sushi sushi = riceTr.GetComponent<Sushi>();
            sushi.wasabi = "보통으로";  //초밥에 와사비 값 저장.
        }
        else if(Wasabi != null)
        {
            Transform riceTr = board.transform.Find("Rice(Clone)").transform;  //보드 오브젝트의 자식 오브젝트 찾기.
            Vector3 wasabiTr = new Vector3(riceTr.position.x, riceTr.position.y + 10, 0);  //밥 조금 위쪽
            Wasabi1 = Instantiate(wasabi1, wasabiTr, Quaternion.identity, riceTr);  //와사비 생성.

            Sushi sushi = riceTr.GetComponent<Sushi>();
            sushi.wasabi = "많이 넣어";  //초밥에 와사비 값 저장.
        }
    }
}
