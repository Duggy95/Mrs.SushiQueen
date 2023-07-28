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
                    fishInfoTxt = info[i].fish_NameT + info[i].fish_CountT + "마리";
                }
                else
                {
                    fishInfoTxt = fishInfoTxt + "\n" + info[i].fish_NameT + info[i].fish_CountT + "마리";
                }
            }

            if (count % 2 == 0)
            {
                fishInfoTxt = fishInfoTxt + "\t\t" + info[i].fish_NameT + info[i].fish_CountT + "마리";
            }
        }

        if (fishInfoTxt == null)
        {
            fishInfoTxt = "오늘 잡은 물고기가 없습니다.";
        }

        if (GameManager.instance.todayData.score > 0)
        {
            receiptTxt.text = "수익과 점수\n\n" + string.Format("<color=#8C8C8C>{0}</color>", GameManager.instance.todayData.gold + "원 / ") +
                 string.Format("<color=#0000>{0}</color>", GameManager.instance.todayData.score) + string.Format("<color=#8C8C8C>{0}</color>", "점") + "\n\n잡은 물고기\n\n" + string.Format("<color=#8C8C8C>{0}</color>", fishInfoTxt);
        }
        else if (GameManager.instance.todayData.score < 0)
        {
            receiptTxt.text = "수익과 점수\n\n" + string.Format("<color=#8C8C8C>{0}</color>", GameManager.instance.todayData.gold + "원 / ") +
                 string.Format("<color=#0054FF>{0}</color>", GameManager.instance.todayData.score) + string.Format("<color=#8C8C8C>{0}</color>", "점") + "\n\n잡은 물고기\n\n" + string.Format("<color=#8C8C8C>{0}</color>", fishInfoTxt);
        }
        else
        {
            receiptTxt.text = "수익과 점수\n\n" + string.Format("<color=#8C8C8C>{0}</color>", GameManager.instance.todayData.gold + "원 / ") +
                string.Format("<color=#8C8C8C>{0}</color>", GameManager.instance.todayData.score + "점") + "\n\n잡은 물고기\n\n" + string.Format("<color=#8C8C8C>{0}</color>", fishInfoTxt);
        }
        headTxt.text = GameManager.instance.data.dateCount + "일차 영수증";
    }
}
