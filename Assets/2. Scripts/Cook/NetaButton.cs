using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetaButton : MonoBehaviour
{
    public GameObject netaPrefab; //회 프리팹
    public GameObject board;  //도마
    public FishData fishData;  //생선 데이터

    public void FishBtn()
    {   
        if(board == null)
        {
            GameObject board = GameObject.Find("Board_RawImage");  //보드 오브젝트 찾기.
        }

        Transform riceTr = board.transform.Find("Rice(Clone)").transform;  //보드 오브젝트의 자식으로 있는 Rice 찾기.
        Vector3 netaTr = new Vector3(riceTr.position.x, riceTr.position.y + 10, 0);  //밥 조금 위쪽
        GameObject neta = Instantiate(netaPrefab,
                                                        netaTr, Quaternion.identity, riceTr); //밥 조금 위쪽에 회생성하고 밥의 자식으로 넣기.
        neta.GetComponent<Neta>().fishData = fishData;  //생선데이터 넘겨주기.
        riceTr.gameObject.AddComponent<DragSushi>();  //밥 오브젝트에 DragSushi 스크립트 Add.
    }
}
