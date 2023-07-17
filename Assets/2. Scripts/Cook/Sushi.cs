using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sushi : MonoBehaviour
{
    public string sushiName;  //초밥 종류 = 생선 이름
    public string wasabi = "없이";  //와사비 유무

    private void Start()
    {
        wasabi = "없이";
    }

    private void Update()
    {
        print(sushiName + "," + wasabi);
    }

    public Sushi(string sushiName, string wasabi)  //생성자
    {
        this.sushiName = sushiName;
        this.wasabi = wasabi;
    }
}
