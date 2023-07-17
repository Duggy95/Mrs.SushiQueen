using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sushi : MonoBehaviour
{
    public string sushiName;
    public string wasabi;

    private void Update()
    {
        print(sushiName + "," + wasabi);
    }

    public Sushi(string sushiName, string wasabi)
    {
        this.sushiName = sushiName;
        this.wasabi = wasabi;
    }
}
