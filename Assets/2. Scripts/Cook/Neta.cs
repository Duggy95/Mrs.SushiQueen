using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Neta : MonoBehaviour
{
    public FishData fishData;
    Image image;

    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = fishData.netaImg;

        Rice rice = GetComponentInParent<Rice>();
        rice.sushiName = fishData.fishName;
        //print(rice.sushiName);
    }
}
