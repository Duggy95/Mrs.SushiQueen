using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetaButton : MonoBehaviour
{
    public GameObject netaPrefab; //회 프리팹
    public Transform board;  //도마

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void FishBtn()
    {
        Transform riceTr = GameObject.Find("Rice(Clone)").transform;  //씬에서 밥 위치 찾기.
        Vector3 netaTr = new Vector3(riceTr.position.x, riceTr.position.y + 15, 0);  //밥 조금 위쪽
        GameObject neta = Instantiate(netaPrefab, 
                                                        netaTr, Quaternion.identity, riceTr); //밥 조금 위쪽에 회생성하고 밥의 자식으로 넣기.
        riceTr.gameObject.AddComponent<DragSushi>();
    }
}
