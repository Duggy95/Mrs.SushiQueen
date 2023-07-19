using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceButton : MonoBehaviour
{
    public GameObject ricePrefab;  //밥 프리팹. 
    public Transform board;  //도마

    public void RiceBtn()  //밥 생성 메서드
    {
        if(board.childCount == 0)  //도마 위에 아무것도 없을 때만
        {
            GameObject rice = Instantiate(ricePrefab, board.position, board.rotation, board);
        }
    }
}
