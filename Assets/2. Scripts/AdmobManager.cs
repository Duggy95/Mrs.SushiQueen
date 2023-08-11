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

    EndSceneCtrl endSceneCtrl;

    private void Awake()
    {
        endSceneCtrl = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<EndSceneCtrl>();
    }

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
    const string rewardID = "ca-app-pub-1674172083345294/2243275214";
    RewardedAd rewardAd;


    void LoadRewardAd()
    {
        endSceneCtrl.Reward();

        rewardAd = new RewardedAd(isTestMode ? rewardTestID : rewardID);
        rewardAd.LoadAd(GetAdRequest());
        rewardAd.OnUserEarnedReward += (sender, e) =>
        {
            LogText.text = "리워드 광고 성공";
        };
    }

    public void ShowRewardAd()
    {
        endSceneCtrl.rewardAdSuccess = true;
        rewardAd.Show();
        LoadRewardAd();
    }
    #endregion
}
