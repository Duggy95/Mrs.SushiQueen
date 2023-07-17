using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public bool isEmpty = false;

    FishingManager fm;
    Button button;
    Text _text;

    void Awake()
    {
        fm = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<FishingManager>();
        button = GetComponent<Button>(); //버튼 component 가져오기
        button.onClick.AddListener(() => UseItem()); //인자가 없을 때 함수 호출
        _text = GetComponentInChildren<Text>();
    }

    public void UseItem()
    {
        if(_text.text == "지렁이")
        {
            fm.useItem_white = true;
        }

        else if(_text.text == "새우")
        {
            fm.useItem_red = true;
        }

        else if(_text.text == "생선살")
        {
            fm.useItem_rare = true;
        }
    }
}
