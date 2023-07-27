using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Receipt : MonoBehaviour
{
    public Text receiptTxt;
    public Text headTxt;

    List<TodayFishInfo> info = new List<TodayFishInfo>();

    private void Start()
    {
        for (int i = 0; i < GameManager.instance.todayFishInfos.Count; i++)
        {
            string fishName = GameManager.instance.todayFishInfos[i].fish_NameT;

            int index = info.FindIndex(info => info.fish_NameT == fishName);

            if (index != -1)
            {
                info[index].fish_CountT += 1;
            }

            else
            {
                info.Add(new TodayFishInfo(GameManager.instance.todayFishInfos[i].fish_NameT, 1));
            }
        }

        string fishInfoTxt = null;
        int count = 0;

        for (int i = 0; i < info.Count; i++)
        {
            count++;
            if (count % 2 != 0)
            {
                if (count == 1)
                {
                    fishInfoTxt = info[i].fish_NameT + info[i].fish_CountT + "����";
                }
                else
                {
                    fishInfoTxt = fishInfoTxt + "\n" + info[i].fish_NameT + info[i].fish_CountT + "����";
                }
            }

            if (count % 2 == 0)
            {
                fishInfoTxt = fishInfoTxt + "\t\t" + info[i].fish_NameT + info[i].fish_CountT + "����";
            }
        }

        if(fishInfoTxt == null)
        {
            fishInfoTxt = "���� ���� ����Ⱑ �����ϴ�.";
        }

        receiptTxt.text = "���� ���Ͱ� ����\n\n" + GameManager.instance.todayData.gold + "�� / " + GameManager.instance.todayData.score + "��" +
                        "\n\n���� ���� �����\n\n" + fishInfoTxt;

        headTxt.text = GameManager.instance.data.dateCount + "���� ������";
    }
}
