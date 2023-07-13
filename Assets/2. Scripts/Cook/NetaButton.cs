using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetaButton : MonoBehaviour
{
    public GameObject netaPrefab; //회 프리팹
    public GameObject board;  //도마
    public FishData fishData;

    public void FishBtn()
    {   
        if(board == null)
        {
            GameObject board = GameObject.Find("Board_RawImage");
        }

        Transform riceTr = board.transform.Find("Rice(Clone)").transform;
        Vector3 netaTr = new Vector3(riceTr.position.x, riceTr.position.y + 10, 0);  //밥 조금 위쪽
        GameObject neta = Instantiate(netaPrefab,
                                                        netaTr, Quaternion.identity, riceTr); //밥 조금 위쪽에 회생성하고 밥의 자식으로 넣기.
        neta.GetComponent<Neta>().fishData = fishData;
        riceTr.gameObject.AddComponent<DragSushi>();
    }
}
