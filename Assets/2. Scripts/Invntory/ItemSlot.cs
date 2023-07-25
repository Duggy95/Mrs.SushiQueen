using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        if(fm != null )
        {
            button = GetComponent<Button>(); //버튼 component 가져오기
            button.onClick.AddListener(() => UseItem()); //인자가 없을 때 함수 호출
            _text = GetComponentInChildren<Text>();
        }
        else
        {
            return;
        }
    }

    public void UseItem()
    {
        if(_text.text.Contains("지렁이"))
        {
            fm.useItem_white = true;
            print("지렁이 사용");
        }

        else if(_text.text.Contains("새우"))
        {
            fm.useItem_red = true;
            print("새우 사용");
        }

        else if(_text.text.Contains("생선살"))
        {
            fm.useItem_rare = true;
            print("생선살 사용");
        }

        UpdateData();
    }

    void UpdateData()
    {
        string[] slotInfo = _text.text.Split(" ");
        string itemName = slotInfo[0];
        print(itemName);
        string valueToFind = _text.text;

        int newValue = 1;
        // 특정 값(valueToFind)을 만족하는 첫 번째 요소의 인덱스를 찾기
        int index = GameManager.instance.inventory_Items.FindIndex(item => item.item_Name == valueToFind);

        if (index != -1)
        {
            int count = int.Parse(GameManager.instance.inventory_Items[index].item_Count) - newValue;

            // 해당 인덱스(index)의 값 변경
            GameManager.instance.inventory_Items[index].item_Count = count.ToString();
            _text.text = itemName + "   " + count + "개";
            GameManager.instance.Save("i");
            if (count <= 0)
            {
                GameManager.instance.inventory_Fishs.RemoveAt(index);
                gameObject.GetComponentInChildren<Image>().sprite = null;
                GetComponentInChildren<Text>().text = "빈 공간";
                GameManager.instance.Save("f");
            }
        }
    }
}
