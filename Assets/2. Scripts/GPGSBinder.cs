using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi.Events;

public class GPGSBinder 
{
    static GPGSBinder inst = new GPGSBinder();
    public static GPGSBinder Inst => inst;

    ISavedGameClient SavedGame =>
        PlayGamesPlatform.Instance.SavedGame;

    IEventsClient Events =>
        PlayGamesPlatform.Instance.Events;

    /*private void Awake()
    {
        PlayGamesPlatform.Activate();
    }*/

    public bool LoginS()
    {
        return Social.localUser.authenticated;
    }

    void Init()
    {
        var config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }


    public void Login(Action<bool, UnityEngine.SocialPlatforms.ILocalUser> onLoginSuccess = null)
    {
        Init();
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (success) =>
        {
            onLoginSuccess?.Invoke(success == SignInStatus.Success, Social.localUser);
            //GameManager.instance.Load();
            Debug.Log("id : " + Social.localUser.id);
        });
    }

    /*public void Login()
    {
        Init();
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool isSuccess) =>
            {
                if (isSuccess)
                {
                    GameManager.instance.Load();
                    GameManager.instance.loginSuccess = true;
                    Debug.Log("id : " + Social.localUser.id);
                }
                else
                {
                    Debug.Log("Login Failed");
                }
            }
            );
        }
    }*/

    public void Logout()
    {
        GameManager.instance.loginSuccess = false;
        PlayGamesPlatform.Instance.SignOut();
    }


    public void SaveCloud(string fileName, string saveData, Action<bool> onCloudSaved = null)
    {
        //Debug.Log("save 1 : " + fileName + "/" + saveData);

        SavedGame.OpenWithAutomaticConflictResolution(fileName, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLastKnownGood, (status, game) =>
            {
                //Debug.Log("save 2 : " + status + "/" + SavedGameRequestStatus.Success);

                if (status == SavedGameRequestStatus.Success)
                {
                    //Debug.Log("save 3 : ");

                    var update = new SavedGameMetadataUpdate.Builder().Build();
                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(saveData);
                    SavedGame.CommitUpdate(game, update, bytes, (status2, game2) =>
                    {
                        //Debug.Log("save 4 : ");
                        onCloudSaved?.Invoke(status2 == SavedGameRequestStatus.Success);
                    });
                }
            });
    }

    public void LoadCloud(string fileName, Action<bool, string> onCloudLoaded = null)
    {
        //Debug.Log("load 1 : " + fileName);
        SavedGame.OpenWithAutomaticConflictResolution(fileName, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLastKnownGood, (status, game) =>
            {
                //Debug.Log("load 2 : " + status + "/" + SavedGameRequestStatus.Success);

                if (status == SavedGameRequestStatus.Success)
                {
                    //Debug.Log("load 3");

                    SavedGame.ReadBinaryData(game, (status2, loadedData) =>
                    {
                        //Debug.Log("load 4");

                        if (status2 == SavedGameRequestStatus.Success)
                        {
                            string data = System.Text.Encoding.UTF8.GetString(loadedData);
                            onCloudLoaded?.Invoke(true, data);
                            GameManager.instance.SetData(data);
                           // Debug.Log("load : " + data);
                        }
                        else
                        {
                            onCloudLoaded?.Invoke(false, null);
                            //Debug.Log("load failed");
                            /*GameManager.instance.Save("i");
                            GameManager.instance.Save("f");
                            GameManager.instance.Save("d");*/
                        }
                    });
                }
            });
    }

    public void DeleteCloud(string fileName, Action<bool> onCloudDeleted = null)
    {
        SavedGame.OpenWithAutomaticConflictResolution(fileName,
            DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, (status, game) =>
            {
                if (status == SavedGameRequestStatus.Success)
                {
                    SavedGame.Delete(game);
                    onCloudDeleted?.Invoke(true);
                }
                else
                    onCloudDeleted?.Invoke(false);
            });
    }

    public void ShowAchievementUI() =>
        Social.ShowAchievementsUI();

    public void UnlockAchievement(string gpgsId, Action<bool> onUnlocked = null) =>
        Social.ReportProgress(gpgsId, 100, success => onUnlocked?.Invoke(success));

    public void IncrementAchievement(string gpgsId, int steps, Action<bool> onUnlocked = null) =>
        PlayGamesPlatform.Instance.IncrementAchievement(gpgsId, steps, success => onUnlocked?.Invoke(success));

    public void ShowAllLeaderboardUI() =>
        Social.ShowLeaderboardUI();

    public void ShowTargetLeaderboardUI(string gpgsId) =>
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(gpgsId);

    public void ReportLeaderboard(string gpgsId, long score, Action<bool> onReported = null) =>
        Social.ReportScore(score, gpgsId, success => onReported?.Invoke(success));

    public void LoadAllLeaderboardArray(string gpgsId, Action<UnityEngine.SocialPlatforms.IScore[]> onloaded = null) =>
        Social.LoadScores(gpgsId, onloaded);

    public void LoadCustomLeaderboardArray(string gpgsId, int rowCount, LeaderboardStart leaderboardStart,
        LeaderboardTimeSpan leaderboardTimeSpan, Action<bool, LeaderboardScoreData> onloaded = null)
    {
        PlayGamesPlatform.Instance.LoadScores(gpgsId, leaderboardStart, rowCount, LeaderboardCollection.Public, leaderboardTimeSpan, data =>
        {
            onloaded?.Invoke(data.Status == ResponseStatus.Success, data);
        });
    }

    public void IncrementEvent(string gpgsId, uint steps)
    {
        Events.IncrementEvent(gpgsId, steps);
    }

    public void LoadEvent(string gpgsId, Action<bool, IEvent> onEventLoaded = null)
    {
        Events.FetchEvent(DataSource.ReadCacheOrNetwork, gpgsId, (status, iEvent) =>
        {
            onEventLoaded?.Invoke(status == ResponseStatus.Success, iEvent);
        });
    }

    public void LoadAllEvent(Action<bool, List<IEvent>> onEventsLoaded = null)
    {
        Events.FetchAllEvents(DataSource.ReadCacheOrNetwork, (status, events) =>
        {
            onEventsLoaded?.Invoke(status == ResponseStatus.Success, events);
        });
    }
}