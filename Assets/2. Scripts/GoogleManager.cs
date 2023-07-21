using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

public class GoogleManager : MonoBehaviour
{
    public static GoogleManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindAnyObjectByType<GoogleManager>();
            }
            return m_instance;
        }
    }
    static GoogleManager m_instance;

    private void Awake()
    {
        if (instance != this) // 싱글톤된 게 자신이 아니라면 삭제
        {
            Destroy(gameObject);
        }

        else
            DontDestroyOnLoad(gameObject);
    }

    public bool onSaving;
    public bool onLoading;
    public string saveData;

    // GooglePlay 게임서비스 초기화 함수
    public void Init()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = false;
        PlayGamesPlatform.Activate();
    }

    // 로그인
    public void Login()
    {
        Init();
        Social.localUser.Authenticate((bool _login) =>
        {
            if (_login == true)
            {
                Debug.Log("Google id :" + Social.localUser.id);
                Debug.Log("Google Login Complete");
                GameManager.instance.idTxt.text = "Google Login Complete : " + Social.localUser.id;  //-----------------------
                GameManager.instance.onLogin = true;
                GameManager.instance.Load();
                /*GameManager.instance.Save("i");
                GameManager.instance.Save("f");
                GameManager.instance.Save("s");*/
            }
            else
            {
                Debug.Log("Google Login Fail");
                GameManager.instance.idTxt.text = "Google Login Fail";  //-----------------------
            }
        });
    }

    public void Logout()
    {
        GameManager.instance.Save("i");
        GameManager.instance.Save("f");
        GameManager.instance.Save("s");
        PlayGamesPlatform.Instance.SignOut();
    }

    public bool CheckLogin()
    {
        GameManager.instance.logTxt.text = "CheckLogin" + Social.localUser.authenticated;
        return Social.localUser.authenticated;
    }

    #region Save

    // 외부에서 함수 호출 시 string으로 정보 저장
    public void SaveToCloud_Item(string _data)
    {
        if (CheckLogin() == false)
            return;

        Save_Item(_data);
    }

    void Save_Item(string _data)
    {
        Debug.Log("Try to save data");
        GameManager.instance.logTxt.text = "Try to save data";
        GameManager.instance.idTxt.text = "data : " + _data;
        // 로그인이 완료될때까지 기다림
        /*while (CheckLogin() == false)
        {
            Login();
            yield return new WaitForSeconds(2f);
        }*/
        onSaving = true;

        string id = Social.localUser.id;
        string fileName = string.Format("{0}_ITEM", id);
        saveData = _data;

        OpenSavedGame(fileName, true);
    }

    // 외부에서 함수 호출 시 string으로 정보 저장
    public void SaveToCloud_Fish(string _data)
    {
        if (CheckLogin() == false)
            return;

        Save_Fish(_data);
    }

    void Save_Fish(string _data)
    {
        Debug.Log("Try to save data");
        GameManager.instance.logTxt.text = "Try to save data";
        GameManager.instance.idTxt.text = "data : " + _data;

        // 로그인이 완료될때까지 기다림
        /*while (CheckLogin() == false)
        {
            Login();
            yield return new WaitForSeconds(2f);
        }*/
        onSaving = true;

        string id = Social.localUser.id;
        string fileName = string.Format("{0}_FISH", id);
        saveData = _data;

        OpenSavedGame(fileName, true);
    }

    // 외부에서 함수 호출 시 string으로 정보 저장
    public void SaveToCloud_Data(string _data)
    {
        if (CheckLogin() == false)
            return;

        Save_Data(_data);
    }

    void Save_Data(string _data)
    {
        Debug.Log("Try to save data");
        GameManager.instance.logTxt.text = "Try to save data";
        GameManager.instance.idTxt.text = "data : " + _data;

        // 로그인이 완료될때까지 기다림
        /*while (CheckLogin() == false)
        {
            Login();
            yield return new WaitForSeconds(2f);
        }*/
        onSaving = true;

        string id = Social.localUser.id;
        string fileName = string.Format("{0}_DATA", id);
        saveData = _data;

        OpenSavedGame(fileName, true);
    }

    void OpenSavedGame(string _fileName, bool _saved)
    {
        ISavedGameClient savedClient = PlayGamesPlatform.Instance.SavedGame;

        if (_saved == true)
            savedClient.OpenWithAutomaticConflictResolution(_fileName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpenedToSave);
        else
        {
            savedClient.OpenWithAutomaticConflictResolution(_fileName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpenedToRead);
        }
    }

    void OnSavedGameOpenedToSave(SavedGameRequestStatus _status, ISavedGameMetadata _data)
    {
        if (_status == SavedGameRequestStatus.Success)
        {
            byte[] b = Encoding.UTF8.GetBytes(string.Format(saveData));
            SaveGame(_data, b, DateTime.Now.TimeOfDay);
            GameManager.instance.logTxt.text = "byte : " + b;

        }
        else
        {
            Debug.Log("Fail");
        }
    }

    void SaveGame(ISavedGameMetadata _data, byte[] _byte, TimeSpan _playTime)
    {
        ISavedGameClient savedClient = PlayGamesPlatform.Instance.SavedGame;
        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();

        builder = builder.WithUpdatedPlayedTime(_playTime).WithUpdatedDescription("Saved at " + DateTime.Now);

        SavedGameMetadataUpdate updateData = builder.Build();
        savedClient.CommitUpdate(_data, updateData, _byte, OnSavedGameWritten);
        GameManager.instance.logTxt.text = "saving";
    }

    void OnSavedGameWritten(SavedGameRequestStatus _status, ISavedGameMetadata _data)
    {
        onSaving = false;

        if (_status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Save Complete");
            GameManager.instance.logTxt.text = "Save Complete";
        }
        else
        {
            Debug.Log("Save Fail");
            GameManager.instance.logTxt.text = "Save Fail";
        }
    }

    #endregion

    #region Load

    public void LoadFromCloud_Item()
    {
        if (CheckLogin() == false)
            return;

        Load_Item();
    }

    void Load_Item()
    {
        onLoading = true;
        /*while (CheckLogin() == false)
        {
            Login();
            yield return new WaitForSeconds(2f);
        }*/

        Debug.Log("Try to load data");
        GameManager.instance.logTxt.text = "Try to load data";
        string id = Social.localUser.id;
        string fileName = string.Format("{0}_ITEM", id);

        OpenSavedGame(fileName, false);
    }

    public void LoadFromCloud_Fish()
    {
        if (CheckLogin() == false)
            return;

        Load_Fish();
    }

    void Load_Fish()
    {
        onLoading = true;
        /*while (CheckLogin() == false)
        {
            Login();
            yield return new WaitForSeconds(2f);
        }*/

        Debug.Log("Try to load data");
        GameManager.instance.logTxt.text = "Try to load data";

        string id = Social.localUser.id;
        string fileName = string.Format("{0}_FISH", id);

        OpenSavedGame(fileName, false);
    }

    public void LoadFromCloud_Data()
    {
        if (CheckLogin() == false)
            return;

        Load_Data();
    }

    void Load_Data()
    {
        onLoading = true;
        /*while (CheckLogin() == false)
        {
            Login();
            yield return new WaitForSeconds(2f);
        }*/

        Debug.Log("Try to load data");
        GameManager.instance.logTxt.text = "Try to load data";

        string id = Social.localUser.id;
        string fileName = string.Format("{0}_DATA", id);

        OpenSavedGame(fileName, false);
    }

    void OnSavedGameOpenedToRead(SavedGameRequestStatus _status, ISavedGameMetadata _data)
    {
        if (_status == SavedGameRequestStatus.Success)
        {
            LoadGameData(_data);
        }
        else
        {
            GameManager.instance.logTxt.text = "load data Fail";

            Debug.Log("Fail");
        }
    }

    void LoadGameData(ISavedGameMetadata _data)
    {
        ISavedGameClient savedClient = PlayGamesPlatform.Instance.SavedGame;
        savedClient.ReadBinaryData(_data, OnSavedGameDataRead);
    }

    void OnSavedGameDataRead(SavedGameRequestStatus _status, byte[] _byte)
    {
        if (_status == SavedGameRequestStatus.Success)
        {
            string data = Encoding.UTF8.GetString(_byte);
            GameManager.instance.SetData(data);
            GameManager.instance.logTxt.text = "load data : " + data;
        }
        else
        {
            Debug.Log("Load Fail");
            GameManager.instance.logTxt.text = "Load Fail";
        }

        onLoading = false;
    }

    #endregion
}
