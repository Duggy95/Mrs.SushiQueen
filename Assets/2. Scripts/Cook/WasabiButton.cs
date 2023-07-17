using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasabiButton : MonoBehaviour
{
    public GameObject wasabi;  //와사비 프리팹
    public GameObject board;  //도마

    public void WasabiBtn()
    {
        if (board == null)
        {
            GameObject board = GameObject.Find("Board_RawImage");
        }

        Transform riceTr = board.transform.Find("Rice(Clone)").transform;
        Vector3 wasabiTr = new Vector3(riceTr.position.x, riceTr.position.y + 10, 0);  //밥 조금 위쪽
        GameObject neta = Instantiate(wasabi,
                                                        wasabiTr, Quaternion.identity, riceTr);  //와사비 생성.

        Sushi sushi = riceTr.GetComponent<Sushi>();
        sushi.wasabi = "보통으로";  //초밥에 와사비 값 저장.
    }
}
