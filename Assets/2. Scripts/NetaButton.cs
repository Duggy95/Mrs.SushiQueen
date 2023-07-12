using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetaButton : MonoBehaviour
{
    public GameObject netaPrefab;
    public Transform board;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void FishBtn()
    {
        Transform riceTr = GameObject.Find("Rice(Clone)").transform;
        Vector3 netaTr = new Vector3(riceTr.position.x, riceTr.position.y + 15, 0);
        GameObject neta = Instantiate(netaPrefab, netaTr, Quaternion.identity, board);
    }
}
