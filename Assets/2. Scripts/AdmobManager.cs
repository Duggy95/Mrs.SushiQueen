using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class AdmobManager : MonoBehaviour
{
    public bool isTestMode;
    public Text LogText;
    public Button RewardAdsBtn;


    void Start()
    {
        LoadRewardAd();
    }

    void Update()
    {
        RewardAdsBtn.interactable = rewardAd.IsLoaded();
    }

    AdRequest GetAdRequest()
    {
        return new AdRequest.Builder().AddTestDevice("1DF7B7CC05014E8").Build();
    }

    #region 리워드 광고
    const string rewardTestID = "ca-app-pub-3940256099942544/5224354917";
    const string rewardID = "ca-app-pub-1674172083345294/9022837378";
    RewardedAd rewardAd;


    void LoadRewardAd()
    {
        rewardAd = new RewardedAd(isTestMode ? rewardTestID : rewardID);
        rewardAd.LoadAd(GetAdRequest());
        rewardAd.OnUserEarnedReward += (sender, e) =>
        {
            LogText.text = "리워드 광고 성공";

            GameManager.instance.nextStage = true;
            int _date = int.Parse(GameManager.instance.data.dateCount) + 1;
            int rewardGold = int.Parse(GameManager.instance.data.gold) + 30000;
            GameManager.instance.data.dateCount = _date.ToString();
            GameManager.instance.data.gold = rewardGold.ToString();
            SceneManager.LoadScene(0);
        };
    }

    public void ShowRewardAd()
    {
        rewardAd.Show();
        LoadRewardAd();
    }
    #endregion
}
