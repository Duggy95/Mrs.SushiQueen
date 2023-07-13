using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    public Dictionary<string, int> sushiList = new Dictionary<string, int>();  //초밥종류, 와사비

    void Start()
    {
        
    }

    void Update()
    {
        print(sushiList.Count);
    }
}
