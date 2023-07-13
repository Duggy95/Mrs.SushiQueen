using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject itemContent;
    public GameObject fishContent;
    public GameObject itemSlotPrefab;
    public GameObject fishSlotPrefab;

    int itemSlotCount;
    int fishSlotCount;

    void Start()
    {
        itemSlotCount = GameManager.instance.itemCount;
        fishSlotCount = GameManager.instance.fishCount;

        for (int i = 0; i < itemSlotCount; i++)
        {
            Instantiate(itemSlotPrefab, itemContent.transform);
        }

        for (int i = 0; i < fishSlotCount; i++)
        {
            Instantiate(fishSlotPrefab, fishContent.transform);
        }
    }
}
