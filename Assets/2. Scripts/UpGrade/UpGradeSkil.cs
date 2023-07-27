using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpGradeSkil : MonoBehaviour
{
    public GameObject noMoneyTxt;
    public GameObject maxLevelTxt;

    EndSceneCtrl endSceneCtrl;

    int level;
    int count;
    int _gold;

    private void Awake()
    {
        endSceneCtrl = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<EndSceneCtrl>();
        Text text = GetComponentInChildren<Text>();
        string[] info = text.text.Split(" ");
        string _name = info[0];

        if (_name == "낚시체력")
        {
            SetFishingTimer();
        }
        else if(_name == "요리체력")
        {
            SetCookingTimer();
        }
        else if(_name == "응대")
        {
            SetCustomerTimer();
        }
    }

    public void SetCookingTimer()
    {
        count = int.Parse(GameManager.instance.data.cookTime);
        level = int.Parse(GameManager.instance.data.cookHPLV);
        _gold = level * 100000;

        Text text = GetComponentInChildren<Text>();
        text.text = "요리체력 LV." + level + "\n가격 : " + _gold.ToString("N0") + "\n현재 요리할 수 있는\n시간 " + count + "초";
    }

    public void GrowthCookingTimer()
    {
        if (int.Parse(GameManager.instance.data.cookHPLV) < 5)
        {
            int countA = int.Parse(GameManager.instance.data.cookTime) + 10;
            int levelA = int.Parse(GameManager.instance.data.cookHPLV);
            int _goldA = levelA * 100000;

            if (int.Parse(GameManager.instance.data.gold) >= _goldA)
            {
                count = countA;
                level = levelA + 1;
                _gold = level * 100000;
                int gold_ = int.Parse(GameManager.instance.data.gold) - _goldA;
                GameManager.instance.data.gold = gold_.ToString();
                GameManager.instance.data.cookTime = count.ToString();
                GameManager.instance.data.cookHPLV = level.ToString();
                Text text = GetComponentInChildren<Text>();
                text.text = "요리체력 LV." + level + "\n가격 : " + _gold.ToString("N0") + "\n현재 요리할 수 있는\n시간 " + count + "초";
                //GameManager.instance.Save("d");
                endSceneCtrl.UIUpdate();
            }
            else
            {
                StartCoroutine(NoMoney());
            }
        }
        else
        {
            StartCoroutine(MaxLevel());
        }
    }

    public void SetFishingTimer()
    {
        count = int.Parse(GameManager.instance.data.fishTime);
        level = int.Parse(GameManager.instance.data.fishHPLV);
        _gold = level * 100000;

        Text text = GetComponentInChildren<Text>();
        text.text = "낚시체력 LV." + level + "\n가격 : " + _gold + "\n현재 낚시할 수 있는\n시간 " + count + "초";
    }

    public void GrowthFishingTimer()
    {
        if (int.Parse(GameManager.instance.data.fishHPLV) < 5)
        {
            int countA = int.Parse(GameManager.instance.data.fishTime) + 10;
            int levelA = int.Parse(GameManager.instance.data.fishHPLV);
            int _goldA = levelA * 100000;

            if (int.Parse(GameManager.instance.data.gold) >= _goldA)
            {
                count = countA;
                level = levelA + 1;
                _gold = level * 100000;
                int gold_ = int.Parse(GameManager.instance.data.gold) - _goldA;
                GameManager.instance.data.gold = gold_.ToString();
                GameManager.instance.data.fishTime = count.ToString();
                GameManager.instance.data.fishHPLV = level.ToString();
                Text text = GetComponentInChildren<Text>();
                text.text = "낚시체력 LV." + level + "\n가격 : " + _gold.ToString("N0") + "\n현재 낚시할 수 있는\n시간 " + count + "초";
                //GameManager.instance.Save("d");
                endSceneCtrl.UIUpdate();
            }
            else
            {
                StartCoroutine(NoMoney());
            }
        }
        else
        {
            StartCoroutine(MaxLevel());
        }
    }

    public void SetCustomerTimer()
    {
        count = int.Parse(GameManager.instance.data.customerTime);
        level = int.Parse(GameManager.instance.data.customerHPLV);
        _gold = level * 100000;

        Text text = GetComponentInChildren<Text>();
        text.text = "응대 LV." + level + "\n가격 : " + _gold + "\n현재 손님 대기시간 \n시간 " + count + "초";
    }

    public void GrowthCustomerTimer()
    {
        if (int.Parse(GameManager.instance.data.customerHPLV) < 5)
        {
            int countA = int.Parse(GameManager.instance.data.customerTime) + 2;
            int levelA = int.Parse(GameManager.instance.data.customerHPLV);
            int _goldA = levelA * 100000;

            if (int.Parse(GameManager.instance.data.gold) >= _goldA)
            {
                count = countA;
                level = levelA + 1;
                _gold = level * 100000;
                int gold_ = int.Parse(GameManager.instance.data.gold) - _goldA;
                GameManager.instance.data.gold = gold_.ToString();
                GameManager.instance.data.customerTime = count.ToString();
                GameManager.instance.data.customerHPLV = level.ToString();
                Text text = GetComponentInChildren<Text>();
                text.text = "응대 LV." + level + "\n가격 : " + _gold + "\n현재 손님 대기시간 \n시간 " + count + "초";
                //GameManager.instance.Save("d");
                endSceneCtrl.UIUpdate();
            }
            else
            {
                StartCoroutine(NoMoney());
            }
        }
        else
        {
            StartCoroutine(MaxLevel());
        }
    }

    IEnumerator NoMoney()
    {
        noMoneyTxt.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        noMoneyTxt.gameObject.SetActive(false);
    }

    IEnumerator MaxLevel()
    {
        maxLevelTxt.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        maxLevelTxt.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        StopCoroutine(MaxLevel());
        StopCoroutine(NoMoney());
    }
}
