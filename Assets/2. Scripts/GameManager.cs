using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Serialization<T>
{
    public Serialization(List<T> _target) => target = _target;
    public List<T> target;
}

[System.Serializable]
public class InventoryItem
{
    public InventoryItem(string _item_Name, string _item_Count)
    { item_Name = _item_Name; item_Count = _item_Count; }

    public string item_Name;
    public string item_Count;
}

[System.Serializable]
public class InventoryFish
{
    public InventoryFish(string _fish_Name, string _fish_Count)
    { fish_Name = _fish_Name; fish_Count = _fish_Count; }

    public string fish_Name;
    public string fish_Count;
}

[System.Serializable]
public class Data
{
    public string itemCount = "3";
    public string fishCount = "3";
    public string cookCount = "3";
    public string dateCount = "0";  //  날짜
    public string score = "500";  // 점수
    public string gold = "0";  // 골드
    public string atk = "1";
    public string getMiddleRod = "FALSE";
    public string getHighRod = "FALSE";
    public string fishTime = "90";
    public string fishHPLV = "1";
    public string cookTime = "180";
    public string cookHPLV = "1";
    public string customerTime = "20";
    public string customerHPLV = "1";
}

[System.Serializable]
public class TodayFishInfo
{
    public TodayFishInfo(string _fish_Name, int _fish_Count)
    { fish_NameT = _fish_Name; fish_CountT = _fish_Count; }

    public string fish_NameT;
    public int fish_CountT;
}

[System.Serializable]
public class TodayData
{
    public int score = 0;  // 점수
    public int gold = 0;  // 골드
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindAnyObjectByType<GameManager>();
            }
            return m_instance;
        }
    }
    static GameManager m_instance;


    List<InventoryItem> _inventory_Items = new List<InventoryItem>();
    public List<InventoryItem> inventory_Items
    {
        get
        {
            return _inventory_Items;
        }
        set
        {
            _inventory_Items = value;
        }
    }

    List<InventoryFish> _inventory_Fishs = new List<InventoryFish>();
    public List<InventoryFish> inventory_Fishs
    {
        get
        {
            return _inventory_Fishs;
        }
        set
        {
            _inventory_Fishs = value;
        }
    }

    Data _data = new Data();
    public Data data
    {
        get
        {
            return _data;
        }
        set
        {
            _data = value;
        }
    }

    public TodayData todayData = new TodayData();
    public List<TodayFishInfo> todayFishInfos = new List<TodayFishInfo>();

    public bool viewInventory = false;
    public bool viewReceipt = false;
    public bool nextStage = false;
    public bool loginSuccess = false;

    private void Awake()
    {
        if (instance != this) // 싱글톤된 게 자신이 아니라면 삭제
        {
            Destroy(gameObject);
        }

        else
            DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (loginSuccess)
            return;

        if(GPGSBinder.Inst.LoginS())
        {
            loginSuccess = true;
            Load();
            Debug.Log("login : " +loginSuccess);
        }
    }

    public void LogIn()
    {
        GPGSBinder.Inst.Login();
    }

    public void Save(string type)
    {
        // 타입에 따라 해당 정보를 json화 해서 문자열로 바꾸고 전달
        if (type == "i")
        {
            string item_Json = JsonUtility.ToJson(new Serialization<InventoryItem>(_inventory_Items));
            Debug.Log("saveItemData : " + item_Json);
            string fileName = string.Format("ITEM");
            GPGSBinder.Inst.SaveCloud(fileName, item_Json);
            //PlayerPrefs.SetString("ITEM", item_Json);
        }
        else if (type == "f")
        {
            string fish_Json = JsonUtility.ToJson(new Serialization<InventoryFish>(_inventory_Fishs));
            Debug.Log("saveFishData : " + fish_Json);
            string fileName = string.Format("FISH");
            GPGSBinder.Inst.SaveCloud(fileName, fish_Json);
            //PlayerPrefs.SetString("FISH", fish_Json);
        }
        else if (type == "d")
        {
            string data_Json = JsonUtility.ToJson(_data);
            Debug.Log("saveData : " + data_Json);
            string fileName = string.Format("DATA");
            GPGSBinder.Inst.SaveCloud(fileName, data_Json);
            //PlayerPrefs.SetString("DATA", data_Json);
        }
    }

    public void Load()
    {
        print("loadData");
        GPGSBinder.Inst.LoadCloud("DATA");

        print("loadItem");
        GPGSBinder.Inst.LoadCloud("ITEM");

        print("loadfish");
        GPGSBinder.Inst.LoadCloud("FISH");

        /*if (PlayerPrefs.HasKey("ITEM"))
        {
            string item_Data = PlayerPrefs.GetString("ITEM");
            inventory_Items = JsonUtility.FromJson<Serialization<InventoryItem>>(item_Data).target;
            Debug.Log("inventory_Items : " + item_Data);
        }
        else
        {
            Save("i");
        }
        if (PlayerPrefs.HasKey("FISH"))
        {
            string fish_Data = PlayerPrefs.GetString("FISH");
            inventory_Fishs = JsonUtility.FromJson<Serialization<InventoryFish>>(fish_Data).target;
            Debug.Log("inventory_Fishs : " + fish_Data);
        }
        else
        {
            Save("f");
        }
        if (PlayerPrefs.HasKey("DATA"))
        {
            string data_Data = PlayerPrefs.GetString("DATA");
            data = JsonUtility.FromJson<Data>(data_Data);
            Debug.Log("inventory_Items : " + data_Data);
        }
        else
        {
            Save("d");
        }*/
    }

    public void SetData(string loadData)
    {
        string i = "item_Name";
        string f = "fish_Name";
        string d = "score";

        print("data load start");
        // 문자열에 포함된 내용에 따라 해당 정보타입으로 바꿔주고 최신화
        if (loadData.Contains(i))
        {
            _inventory_Items = JsonUtility.FromJson<Serialization<InventoryItem>>(loadData).target;
            Debug.Log("loadItemData : " + loadData);
        }
        else if (loadData.Contains(f))
        {
            _inventory_Fishs = JsonUtility.FromJson<Serialization<InventoryFish>>(loadData).target;
            Debug.Log("loadFishData : " + loadData);
        }
        else if (loadData.Contains(d))
        {
            _data = JsonUtility.FromJson<Data>(loadData);
            Debug.Log("loadData : " + loadData);
        }
    }

    public void DeleteData()
    {
        GPGSBinder.Inst.DeleteCloud("ITEM");
        GPGSBinder.Inst.DeleteCloud("FISH");
        GPGSBinder.Inst.DeleteCloud("DATA");

        data = new Data();
        Save("d");

        inventory_Items.Clear();
        Save("i");

        inventory_Fishs.Clear();
        Save("f");

        Debug.Log("deleteItemData : " + inventory_Items.Count);
        Debug.Log("deleteFishData : " + inventory_Fishs.Count);
        Debug.Log("deleteData : " + data);

        /*PlayerPrefs.DeleteAll();
        Debug.Log("데이터 삭제");*/
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
