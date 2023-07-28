using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpGradeShop : MonoBehaviour
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

        if(_name == "수족관")
        {
            SetAquarium();
        }
        else if (_name == "요리공간")
        {
            SetCookingAbility();
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
            text.text = "수족관 LV." + level + "->" + nextLevel +
                "\n가격 : " + _gold.ToString("N0") +
                "\n현재 물고기 공간\n최대 " + count + "->" + nextCount + "마리";
        }
        else if (level == 4)
        {
            text.text = "수족관 LV." + level + "->" + "Max" +
                "\n가격 : " + _gold.ToString("N0") +
                "\n현재 물고기 공간\n최대 " + count + "->" + nextCount + "마리";
        }
        else
        {
            text.text = "수족관 Lv.Max" +
                "\n가격 : " + _gold.ToString("N0") +
                "\n현재 물고기 공간\n최대 " + count + "마리";
        }
    }

    public void GrowthAquarium()
    {
        if (int.Parse(GameManager.instance.data.fishCount) < 7)
        {
            int countA = int.Parse(GameManager.instance.data.fishCount) + 1;  // 4
            int levelA = int.Parse(GameManager.instance.data.fishCount) - 1;  // 2
            int _goldA = int.Parse(GameManager.instance.data.fishCount) * 100000;

            if (int.Parse(GameManager.instance.data.gold) >= _goldA)
            {
                count = countA;
                level = levelA;
                _gold = _goldA + 100000;
                int nextCount = count + 1;
                int nextLevel = level + 1;
                int gold_ = int.Parse(GameManager.instance.data.gold) - _goldA;
                GameManager.instance.data.gold = gold_.ToString();
                GameManager.instance.data.fishCount = count.ToString();
                Text text = GetComponentInChildren<Text>();
                if (level < 4)
                {
                    text.text = "수족관 LV." + level + "->" + nextLevel +
                        "\n가격 : " + _gold.ToString("N0") +
                        "\n현재 물고기 공간\n최대 " + count + "->" + nextCount + "마리";
                }
                else if (level == 4)
                {
                    text.text = "수족관 LV." + level + "->" + "Max" +
                        "\n가격 : " + _gold.ToString("N0") +
                        "\n현재 물고기 공간\n최대 " + count + "->" + nextCount + "마리";
                }
                else
                {
                    text.text = "수족관 Lv.Max" +
                        "\n가격 : " + _gold.ToString("N0") +
                        "\n현재 물고기 공간\n최대 " + count + "마리";
                }
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
            text.text = "요리공간 LV." + level + "->" + nextCount +
                "\n가격 : " + _gold.ToString("N0") +
                "\n효과 : 요리할 수 있는\n물고기 수 " + count + "->" + nextLevel + "마리";
        }
        else if (level == 4)
        {
            text.text = "요리공간 LV." + level + "->" + "Max" +
                "\n가격 : " + _gold.ToString("N0") +
               "\n효과 : 요리할 수 있는\n물고기 수 " + count + "->" + nextLevel + "마리";
        }
        else
        {
            text.text = "요리공간 Lv.Max" +
                "\n가격 : " + _gold.ToString("N0") +
                "\n효과 : 요리할 수 있는\n물고기 수 " + count + "마리";
        }
    }

    public void GrowthCookingAbility()
    {
        if (int.Parse(GameManager.instance.data.cookCount) < 7)
        {
            int countA = int.Parse(GameManager.instance.data.cookCount) + 1;
            int levelA = int.Parse(GameManager.instance.data.cookCount) - 1;
            int _goldA = int.Parse(GameManager.instance.data.cookCount) * 100000;

            if (int.Parse(GameManager.instance.data.gold) >= _goldA)
            {
                count = countA;
                level = levelA;
                _gold = _goldA + 100000;
                int nextCount = count + 1;
                int nextLevel = level + 1;
                int gold_ = int.Parse(GameManager.instance.data.gold) - _goldA;
                GameManager.instance.data.gold = gold_.ToString();
                GameManager.instance.data.cookCount = count.ToString();
                Text text = GetComponentInChildren<Text>();
                if (level < 4)
                {
                    text.text = "요리공간 LV." + level + "->" + nextCount +
                        "\n가격 : " + _gold.ToString("N0") +
                        "\n효과 : 요리할 수 있는\n물고기 수 " + count + "->" + nextLevel + "마리";
                }
                else if (level == 4)
                {
                    text.text = "요리공간 LV." + level + "->" + "Max" +
                        "\n가격 : " + _gold.ToString("N0") +
                       "\n효과 : 요리할 수 있는\n물고기 수 " + count + "->" + nextLevel + "마리";
                }
                else
                {
                    text.text = "요리공간 Lv.Max" +
                        "\n가격 : " + _gold.ToString("N0") +
                        "\n효과 : 요리할 수 있는\n물고기 수 " + count + "마리";
                }
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
