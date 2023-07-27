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

        if(_name == "������")
        {
            SetAquarium();
        }

        else if(_name == "���׸���")
        {

        }
    }

    public void SetAquarium()
    {
        count = int.Parse(GameManager.instance.data.fishCount);
        level = int.Parse(GameManager.instance.data.fishCount) - 2;
        _gold = int.Parse(GameManager.instance.data.fishCount) * 100000;

        Text text = GetComponentInChildren<Text>();
        text.text = "������ LV." + level + "\n���� : " + _gold + "\nȿ�� : ����� ����\n�ִ� " + count + "����";
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
                int gold_ = int.Parse(GameManager.instance.data.gold) - _goldA;
                GameManager.instance.data.gold = gold_.ToString();
                GameManager.instance.data.fishCount = count.ToString();
                Text text = GetComponentInChildren<Text>();
                text.text = "������ LV." + level + "\n���� : " + _gold + "\nȿ�� : ����� ����\n�ִ� " + count + "����";
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

    public void InteriorUpgrade()
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
