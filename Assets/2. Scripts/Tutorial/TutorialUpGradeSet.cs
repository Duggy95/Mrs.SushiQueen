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

        if (_name == "����ü��")
        {
            SetFishingTimer();
        }
        else if (_name == "�丮ü��")
        {
            SetCookingTimer();
        }
        else if (_name == "�մ�����")
        {
            SetCustomerTimer();
        }
        else if (_name == "������")
        {
            SetAquarium();
        }
        else if (_name == "�丮����")
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
            text.text = "�丮ü�� LV." + level + " -> " + nextLevel +
                "\n���� : " + _gold.ToString("N0") +
                "\n���� �丮�� �� �ִ�\n�ð� " + count + " -> " + nextCount + "��";
        }
        else if (level == 4)
        {
            text.text = "�丮ü�� LV." + level + " -> " + "Max" +
                "\n���� : " + _gold.ToString("N0") +
                "\n���� �丮�� �� �ִ�\n�ð� " + count + " -> " + nextCount + "��";
        }
        else
        {
            text.text = "�丮ü�� Lv.Max" +
                "\n���� : ----" +
                "\n���� �丮�� �� �ִ�\n�ð� " + count + "��";
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
            text.text = "����ü�� LV." + level + " -> " + nextLevel +
                "\n���� : " + _gold.ToString("N0") +
                "\n���� ������ �� �ִ�\n�ð� " + count + " -> " + nextCount + "��";
        }
        else if (level == 4)
        {
            text.text = "����ü�� LV." + level + " -> " + "Max" +
                "\n���� : " + _gold.ToString("N0") +
                "\n���� ������ �� �ִ�\n�ð� " + count + " -> " + nextCount + "��";
        }
        else
        {
            text.text = "����ü�� Lv.Max" +
                "\n���� : ----" +
                "\n���� ������ �� �ִ�\n�ð� " + count + "��";
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
            text.text = "���� LV." + level + " -> " + nextLevel +
                "\n���� : " + _gold.ToString("N0") +
                "\n���� �մ� ���ð� \n�ð� " + count + " -> " + nextCount + "��";
        }
        else if (level == 4)
        {
            text.text = "���� LV." + level + " -> " + "Max" +
                "\n���� : " + _gold.ToString("N0") +
                "\n���� �մ� ���ð�\n�ð� " + count + " -> " + nextCount + "��";
        }
        else
        {
            text.text = "���� Lv.Max" +
               "\n���� : ----" +
                "\n���� �մ� ���ð�\n�ð� " + count + "��";
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
            text.text = "������ LV." + level + " -> " + nextLevel +
                "\n���� : " + _gold.ToString("N0") +
                "\n���� ����� ����\n�ִ� " + count + " -> " + nextCount + "����";
        }
        else if (level == 4)
        {
            text.text = "������ LV." + level + " -> " + "Max" +
                "\n���� : " + _gold.ToString("N0") +
                "\n���� ����� ����\n�ִ� " + count + " -> " + nextCount + "����";
        }
        else
        {
            text.text = "������ Lv.Max" +
                "\n���� : ----" +
                "\n���� ����� ����\n�ִ� " + count + "����";
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
            text.text = "�丮���� LV." + level + " -> " + nextLevel +
                "\n���� : " + _gold.ToString("N0") +
                "\nȿ�� : �丮�� �� �ִ�\n����� �� " + count + " -> " + nextCount + "����";
        }
        else if (level == 4)
        {
            text.text = "�丮���� LV." + level + " -> " + "Max" +
                "\n���� : " + _gold.ToString("N0") +
               "\nȿ�� : �丮�� �� �ִ�\n����� �� " + count + " -> " + nextCount + "����";
        }
        else
        {
            text.text = "�丮���� Lv.Max" +
                "\n���� : ----" +
                "\nȿ�� : �丮�� �� �ִ�\n����� �� " + count + "����";
        }
    }
}
