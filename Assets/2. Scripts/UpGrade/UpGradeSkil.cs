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

        if (_name == "�丮")
        {
            SetCookingAbility();
        }

        else if (_name == "ȫ��")
        {

        }
    }

    public void SetCookingAbility()
    {
        count = int.Parse(GameManager.instance.data.cookCount);
        level = count - 2;
        _gold = count * 100000;

        Text text = GetComponentInChildren<Text>();
        text.text = "�丮 LV." + level + "\n���� : " + _gold + "\nȿ�� : �丮�� �� �ִ�\n����� �� " + count + "����";
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
                int gold_ = int.Parse(GameManager.instance.data.gold) - _goldA;
                GameManager.instance.data.gold = gold_.ToString();
                GameManager.instance.data.cookCount = count.ToString();
                Text text = GetComponentInChildren<Text>();
                text.text = "�丮 LV." + level + "\n���� : " + _gold + "\nȿ�� : �丮�� �� �ִ�\n����� �� " + count + "����";
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

    public void PromotionUpgrade()
    {

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
