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
        if (instance != this) // �̱���� �� �ڽ��� �ƴ϶�� ����
        {
            Destroy(gameObject);
        }

        else
            DontDestroyOnLoad(gameObject);
    }

    public bool onSaving;
    public bool onLoading;
    public string saveData;

    // GooglePlay ���Ӽ��� �ʱ�ȭ �Լ�
    public void Init()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = false;
        PlayGamesPlatform.Activate();
    }

    // �α���
    public void Login()
    {
        Init();
        Social.localUser.Authenticate((bool _login) =>
        {
            if (_login == true)
            {
                Debug.Log("Google Login Complete");
            }
            else
            {
                Debug.Log("Google Login Fail");
            }
        });
    }

    public bool CheckLogin()
    {
        return Social.localUser.authenticated;
    }

    #region Save

    // �ܺο��� �Լ� ȣ�� �� string���� ���� ����
    public void SaveToCloud_Item(string _data)
    {
        StartCoroutine(Save_Item(_data));
    }

    IEnumerator Save_Item(string _data)
    {
        Debug.Log("Try to save data");
        // �α����� �Ϸ�ɶ����� ��ٸ�
        while (CheckLogin() == false)
        {
            Login();
            yield return new WaitForSeconds(2f);
        }
        onSaving = true;

        string id = Social.localUser.id;
        string fileName = string.Format("{0}_ITEM", id);
        saveData = _data;

        OpenSavedGame(fileName, true);
    }

    // �ܺο��� �Լ� ȣ�� �� string���� ���� ����
    public void SaveToCloud_Fish(string _data)
    {
        StartCoroutine(Save_Fish(_data));
    }

    IEnumerator Save_Fish(string _data)
    {
        Debug.Log("Try to save data");
        // �α����� �Ϸ�ɶ����� ��ٸ�
        while (CheckLogin() == false)
        {
            Login();
            yield return new WaitForSeconds(2f);
        }
        onSaving = true;

        string id = Social.localUser.id;
        string fileName = string.Format("{0}_FISH", id);
        saveData = _data;

        OpenSavedGame(fileName, true);
    }

    // �ܺο��� �Լ� ȣ�� �� string���� ���� ����
    public void SaveToCloud_Dave(string _data)
    {
        StartCoroutine(Save_Dave(_data));
    }

    IEnumerator Save_Dave(string _data)
    {
        Debug.Log("Try to save data");
        // �α����� �Ϸ�ɶ����� ��ٸ�
        while (CheckLogin() == false)
        {
            Login();
            yield return new WaitForSeconds(2f);
        }
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
    }

    void OnSavedGameWritten(SavedGameRequestStatus _status, ISavedGameMetadata _data)
    {
        onSaving = false;

        if (_status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Save Complete");
        }
        else
        {
            Debug.Log("Save Fail");
        }
    }

    #endregion

    #region Load

    public void LoadFromCloud_Item()
    {
        StartCoroutine(Load_Item());
    }

    IEnumerator Load_Item()
    {
        onLoading = true;
        while (CheckLogin() == false)
        {
            Login();
            yield return new WaitForSeconds(2f);
        }

        Debug.Log("Try to load data");

        string id = Social.localUser.id;
        string fileName = string.Format("{0}_ITEM", id);

        OpenSavedGame(fileName, false);
    }

    public void LoadFromCloud_Fish()
    {
        StartCoroutine(Load_Fish());
    }

    IEnumerator Load_Fish()
    {
        onLoading = true;
        while (CheckLogin() == false)
        {
            Login();
            yield return new WaitForSeconds(2f);
        }

        Debug.Log("Try to load data");

        string id = Social.localUser.id;
        string fileName = string.Format("{0}_FISH", id);

        OpenSavedGame(fileName, false);
    }

    public void LoadFromCloud_Dave()
    {
        StartCoroutine(Load_Dave());
    }

    IEnumerator Load_Dave()
    {
        onLoading = true;
        while (CheckLogin() == false)
        {
            Login();
            yield return new WaitForSeconds(2f);
        }

        Debug.Log("Try to load data");

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
        }
        else
        {
            Debug.Log("Load Fail");
        }

        onLoading = false;
    }

    #endregion
}
