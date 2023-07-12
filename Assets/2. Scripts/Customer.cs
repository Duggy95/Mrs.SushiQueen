using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    //public Text orderTxt;
    public Sprite[] sprites;
    public string[] sushis;
    public string[] wasabis;
    string message = "�ּ���.";
    string order;

    void OnEnable()
    {
        CookManager.instance.isCustomer = true;
        int randomSprite = Random.Range(0, sprites.Length);
        GetComponent<Image>().sprite = sprites[randomSprite];

        int randomSushi = Random.Range(0, sushis.Length);
        int randomWasabi = Random.Range(0, wasabis.Length);
        order = sushis[randomSushi] + "�ʹ� " +
                    Random.Range(1, 4) + "�� " +
                    "�ͻ�� " + wasabis[randomWasabi] + message;
        Text orderTxt = GameObject.Find("Order_Text").GetComponent<Text>();
        orderTxt.text = order;
    }

    private void Update()
    {
        if(!CookManager.instance.isCustomer)
        {
            Destroy(gameObject);
        }
    }
}
