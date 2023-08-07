using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpGradeSkil : MonoBehaviour
{
    public GameObject noMoneyTxt;
    public GameObject maxLevelTxt;

    EndSceneCtrl endSceneCtrl;
    AudioSource audioSource;

    int level;
    int count;
    int _gold;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        endSceneCtrl = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<EndSceneCtrl>();
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
                int nextCount = count + 5;
                int nextLevel = level + 1;
                int gold_ = int.Parse(GameManager.instance.data.gold) - _goldA;
                GameManager.instance.data.gold = gold_.ToString();
                GameManager.instance.data.cookTime = count.ToString();
                GameManager.instance.data.cookHPLV = level.ToString();
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
                audioSource.PlayOneShot(SoundManager.instance.levelUp, 1);
                endSceneCtrl.UIUpdate();
            }
            else
            {
                audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

                StartCoroutine(NoMoney());
            }
        }
        else
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

            StartCoroutine(MaxLevel());
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
                int nextCount = count + 5;
                int nextLevel = level + 1;
                int gold_ = int.Parse(GameManager.instance.data.gold) - _goldA;
                GameManager.instance.data.gold = gold_.ToString();
                GameManager.instance.data.fishTime = count.ToString();
                GameManager.instance.data.fishHPLV = level.ToString();
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
                audioSource.PlayOneShot(SoundManager.instance.levelUp, 1);
                endSceneCtrl.UIUpdate();
            }
            else
            {
                audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

                StartCoroutine(NoMoney());
            }
        }
        else
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

            StartCoroutine(MaxLevel());
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
                int nextCount = count + 2;
                int nextLevel = level + 1;
                int gold_ = int.Parse(GameManager.instance.data.gold) - _goldA;
                GameManager.instance.data.gold = gold_.ToString();
                GameManager.instance.data.customerTime = count.ToString();
                GameManager.instance.data.customerHPLV = level.ToString();
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
                audioSource.PlayOneShot(SoundManager.instance.levelUp, 1);
                endSceneCtrl.UIUpdate();
            }
            else
            {
                audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

                StartCoroutine(NoMoney());
            }
        }
        else
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

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
