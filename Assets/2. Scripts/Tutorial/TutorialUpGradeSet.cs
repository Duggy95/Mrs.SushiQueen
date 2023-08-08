using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUpGradeSet : MonoBehaviour
{
    int count;
    int _gold;
    int level;

    private void Awake()
    {
        Text text = GetComponentInChildren<Text>();
        string[] info = text.text.Split(" ");
        string _name = info[0];

        if (_name == "낚시체력")
        {
            SetFishingTimer();
        }
        else if (_name == "요리체력")
        {
            SetCookingTimer();
        }
        else if (_name == "손님응대")
        {
            SetCustomerTimer();
        }
        else if (_name == "수족관")
        {
            SetAquarium();
        }
        else if (_name == "요리공간")
        {
            SetCookingAbility();
        }
    }

    public void SetCookingTimer()
    {
        count = int.Parse(GameManager.instance.data.cookTime);
        level = int.Parse(GameManager.instance.data.cookHPLV);
        _gold = level * 100000;
        int nextCount = count + 5;
        int nextLevel = level + 1;
        Text text = GetComponentInChildren<Text>();
        if (level < 4)
        {
            text.text = "요리체력 LV." + level + " -> " + nextLevel +
                "\n가격 : " + _gold.ToString("N0") +
                "\n현재 요리할 수 있는\n시간 " + count + " -> " + nextCount + "초";
        }
        else if (level == 4)
        {
            text.text = "요리체력 LV." + level + " -> " + "Max" +
                "\n가격 : " + _gold.ToString("N0") +
                "\n현재 요리할 수 있는\n시간 " + count + " -> " + nextCount + "초";
        }
        else
        {
            text.text = "요리체력 Lv.Max" +
                "\n가격 : ----" +
                "\n현재 요리할 수 있는\n시간 " + count + "초";
        }
    }

    public void SetFishingTimer()
    {
        count = int.Parse(GameManager.instance.data.fishTime);
        level = int.Parse(GameManager.instance.data.fishHPLV);
        _gold = level * 100000;
        int nextCount = count + 5;
        int nextLevel = level + 1;
        Text text = GetComponentInChildren<Text>();
        if (level < 4)
        {
            text.text = "낚시체력 LV." + level + " -> " + nextLevel +
                "\n가격 : " + _gold.ToString("N0") +
                "\n현재 낚시할 수 있는\n시간 " + count + " -> " + nextCount + "초";
        }
        else if (level == 4)
        {
            text.text = "낚시체력 LV." + level + " -> " + "Max" +
                "\n가격 : " + _gold.ToString("N0") +
                "\n현재 낚시할 수 있는\n시간 " + count + " -> " + nextCount + "초";
        }
        else
        {
            text.text = "낚시체력 Lv.Max" +
                "\n가격 : ----" +
                "\n현재 낚시할 수 있는\n시간 " + count + "초";
        }
    }

    public void SetCustomerTimer()
    {
        count = int.Parse(GameManager.instance.data.customerTime);
        level = int.Parse(GameManager.instance.data.customerHPLV);
        _gold = level * 100000;
        int nextCount = count + 2;
        int nextLevel = level + 1;
        Text text = GetComponentInChildren<Text>();
        if (level < 4)
        {
            text.text = "응대 LV." + level + " -> " + nextLevel +
                "\n가격 : " + _gold.ToString("N0") +
                "\n현재 손님 대기시간 \n시간 " + count + " -> " + nextCount + "초";
        }
        else if (level == 4)
        {
            text.text = "응대 LV." + level + " -> " + "Max" +
                "\n가격 : " + _gold.ToString("N0") +
                "\n현재 손님 대기시간\n시간 " + count + " -> " + nextCount + "초";
        }
        else
        {
            text.text = "응대 Lv.Max" +
               "\n가격 : ----" +
                "\n현재 손님 대기시간\n시간 " + count + "초";
        }
    }

    public void SetAquarium()
    {
        count = int.Parse(GameManager.instance.data.fishCount);
        level = int.Parse(GameManager.instance.data.fishCount) - 2;
        _gold = int.Parse(GameManager.instance.data.fishCount) * 100000;
        int nextCount = count + 1;
        int nextLevel = level + 1;
        Text text = GetComponentInChildren<Text>();
        if (level < 4)
        {
            text.text = "수족관 LV." + level + " -> " + nextLevel +
                "\n가격 : " + _gold.ToString("N0") +
                "\n현재 물고기 공간\n최대 " + count + " -> " + nextCount + "마리";
        }
        else if (level == 4)
        {
            text.text = "수족관 LV." + level + " -> " + "Max" +
                "\n가격 : " + _gold.ToString("N0") +
                "\n현재 물고기 공간\n최대 " + count + " -> " + nextCount + "마리";
        }
        else
        {
            text.text = "수족관 Lv.Max" +
                "\n가격 : ----" +
                "\n현재 물고기 공간\n최대 " + count + "마리";
        }
    }

    public void SetCookingAbility()
    {
        count = int.Parse(GameManager.instance.data.cookCount);
        level = count - 2;
        _gold = count * 100000;
        int nextCount = count + 1;
        int nextLevel = level + 1;
        Text text = GetComponentInChildren<Text>();
        if (level < 4)
        {
            text.text = "요리공간 LV." + level + " -> " + nextLevel +
                "\n가격 : " + _gold.ToString("N0") +
                "\n효과 : 요리할 수 있는\n물고기 수 " + count + " -> " + nextCount + "마리";
        }
        else if (level == 4)
        {
            text.text = "요리공간 LV." + level + " -> " + "Max" +
                "\n가격 : " + _gold.ToString("N0") +
               "\n효과 : 요리할 수 있는\n물고기 수 " + count + " -> " + nextCount + "마리";
        }
        else
        {
            text.text = "요리공간 Lv.Max" +
                "\n가격 : ----" +
                "\n효과 : 요리할 수 있는\n물고기 수 " + count + "마리";
        }
    }
}
