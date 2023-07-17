using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpGrade_Fishing : MonoBehaviour
{
    public GameObject noMoneyTxt;
    public GameObject ItemContent;

    private void Start()
    {

    }

    public void MiddleRod()
    {
        if (GameManager.instance.atk < 2 && GameManager.instance.gold >= 500000)
        {
            GameManager.instance.gold -= 500000;
            GameManager.instance.atk = 2;

            GameManager.instance.SetAtk();
            GameManager.instance.SetGold();

            Text text = GetComponentInChildren<Text>();
            text.text = "구매한 제품입니다";

            Destroy(this);
        }

        else
        {
            StartCoroutine(NoMoney());
        }
    }

    public void HighRod()
    {
        if (GameManager.instance.atk < 4 && GameManager.instance.gold >= 1000000)
        {
            GameManager.instance.gold -= 1000000;
            GameManager.instance.atk = 4;

            GameManager.instance.SetAtk();
            GameManager.instance.SetGold();

            Text text = GetComponentInChildren<Text>();
            text.text = "구매한 제품입니다";

            Destroy(this);
        }

        else
        {
            StartCoroutine(NoMoney());
        }
    }

    public void Buy_Item()
    {
        Text text = GetComponentInChildren<Text>();
        Image[] _items = ItemContent.gameObject.GetComponentsInChildren<Image>();
        Debug.Log("아이템 칸 수 : " + _items.Length);

        // 수족관의 스롯을 검사하여 비었으면 해당 정보 전달
        for (int i = 0; i < _items.Length; i++)
        {
            ItemSlot _slot = _items[i].GetComponent<ItemSlot>();
            if (_slot.isEmpty == false)
            {
                _items[i].sprite = gameObject.GetComponent<Image>().sprite;

                if (GameManager.instance.gold > 10000 && text.name == "White")
                {
                    _items[i].GetComponentInChildren<Text>().text = "지렁이";
                    GameManager.instance.items.Add("지렁이");
                    _slot.isEmpty = true;
                }
                else if (GameManager.instance.gold > 10000 && text.name == "Red")
                {
                    _items[i].GetComponentInChildren<Text>().text = "새우";
                    GameManager.instance.items.Add("새우");
                    _slot.isEmpty = true;
                }
                else if (GameManager.instance.gold > 50000 && text.name == "Rare")
                {
                    _items[i].GetComponentInChildren<Text>().text = "생선살";
                    GameManager.instance.items.Add("생선살");
                    _slot.isEmpty = true;
                }
                else
                {
                    StartCoroutine(NoMoney());
                }
                break;
            }
        }
    }

    IEnumerator NoMoney()
    {
        noMoneyTxt.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        noMoneyTxt.gameObject.SetActive(false);
    }

}
