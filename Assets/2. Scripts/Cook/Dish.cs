using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    public Dictionary<string, int> sushiList = new Dictionary<string, int>();  //�ʹ�����, �ͻ��

    void Start()
    {
        
    }

    void Update()
    {
        print(sushiList.Count);
    }
}
