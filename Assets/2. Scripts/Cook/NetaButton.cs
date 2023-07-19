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

        bool hasFish = false;  //생선이 이미 존재하는 지

        //중복확인.
        foreach (Transform child in riceTr)  
        {
            if (child.CompareTag("FISH"))
            {
                hasFish = true;
                break;
            }
        }

        if (riceTr != null && !hasFish)  //생선이 없을 때만 
        {
            Vector3 netaTr = new Vector3(riceTr.position.x, riceTr.position.y + 10, 0);  //밥 조금 위쪽
            GameObject neta = Instantiate(netaPrefab,
                                                            netaTr, Quaternion.identity, riceTr); //밥 조금 위쪽에 회생성하고 밥의 자식으로 넣기.
            neta.GetComponent<Neta>().fishData = fishData;  //생선데이터 넘겨주기.
            riceTr.gameObject.AddComponent<DragSushi>();  //밥 오브젝트에 DragSushi 스크립트 Add.
        }
    }
}
