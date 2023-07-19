using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sushi : MonoBehaviour
{
    public string sushiName;  //초밥 종류 = 생선 이름
    public string wasabi = "없이";  //와사비 유무
    public int gold;  //가격


    private void Start()
    {
        wasabi = "없이";  //기본 값.
    }

    private void Update()
    {
        print(sushiName + "," + wasabi);
    }

    public Sushi(string sushiName, string wasabi, int gold)  //생성자
    {
        this.sushiName = sushiName;
        this.wasabi = wasabi;
        this.gold = gold;
    }
}
