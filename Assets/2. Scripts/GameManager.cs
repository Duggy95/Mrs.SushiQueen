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
    public InventoryFish(string _fish_Name)
    { fish_Name = _fish_Name; }

    public string fish_Name;
}

[System.Serializable]
public class Data
{
    /*public Data(string _itemCount, string _fishCount, string _dateCount, string _score, string _gold, string _atk)
    { itemCount = _itemCount; fishCount = _fishCount; dateCount = _dateCount; score = _score; gold = _gold; atk = _atk; }*/

    public string itemCount = "3";
    public string fishCount = "3";
    public string dateCount = "1";  //  날짜
    public string score = "500";  // 점수
    public string gold = "0";  // 골드
    public string atk = "1";
    public string getMiddleRod = "FALSE";
    public string getHighRod = "FALSE";
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

    /*public List<InventoryItem> inventory_Items = new List<InventoryItem>();
    public List<InventoryFish> inventory_Fishs = new List<InventoryFish>();
    public Data data = new Data();*/
    public List<InventoryItem> inventory_Items;
    public List<InventoryFish> inventory_Fishs;
    public Data data;
    /*public Text logTxt;  //-----------------------
    public Text idTxt;  //-----------------------
    public Text saveTxt;  //-----------------------*/

    public bool nextStage;
    //public bool onLogin = false;

    private void Awake()
    {
        if (instance != this) // 싱글톤된 게 자신이 아니라면 삭제
        {
            Destroy(gameObject);
        }

        else
            DontDestroyOnLoad(gameObject);

        Load();
    }

    /*public void LogIn()
    {
        GPGSBinder.Inst.Login();
    }*/

    public void Save(string type)
    {
        // 타입에 따라 해당 정보를 json화 해서 문자열로 바꾸고 전달
        if (type == "i")
        {
            string item_Json = JsonUtility.ToJson(new Serialization<InventoryItem>(inventory_Items));
            /*string fileName = string.Format("ITEM");
            GPGSBinder.Inst.SaveCloud(fileName, item_Json);
            Debug.Log("Data_Item" + item_Json);*/
            PlayerPrefs.SetString("ITEM", item_Json);
        }
        else if (type == "f")
        {
            string fish_Json = JsonUtility.ToJson(new Serialization<InventoryFish>(inventory_Fishs));
            /*string fileName = string.Format("FISH");
            GPGSBinder.Inst.SaveCloud(fileName, fish_Json);
            Debug.Log("Data_Fish" + fish_Json);*/
            PlayerPrefs.SetString("FISH", fish_Json);
        }
        else if (type == "s")
        {
            string data_Json = JsonUtility.ToJson(data);
            /*string fileName = string.Format("DATA");
            GPGSBinder.Inst.SaveCloud(fileName, data_Json);
            Debug.Log("Data_Data" + data_Json);*/
            PlayerPrefs.SetString("DATA", data_Json);
        }
    }

    /*public void GetLog()
    {
        logTxt = GameObject.FindGameObjectWithTag("LOG").GetComponent<Text>();
        idTxt = GameObject.FindGameObjectWithTag("ID").GetComponent<Text>();
        saveTxt = GameObject.FindGameObjectWithTag("SAVE").GetComponent<Text>();
    }*/

    public void Load()
    {
        /*Debug.Log("load_cloud");

        GPGSBinder.Inst.LoadCloud("ITEM");
        GPGSBinder.Inst.LoadCloud("FISH");
        GPGSBinder.Inst.LoadCloud("DATA");*/

        if (PlayerPrefs.HasKey("ITEM"))
        {
            string item_Data = PlayerPrefs.GetString("ITEM");
            inventory_Items = JsonUtility.FromJson<Serialization<InventoryItem>>(item_Data).target;
            Debug.Log("inventory_Items : " + item_Data);
        }
        if (PlayerPrefs.HasKey("FISH"))
        {
            string fish_Data = PlayerPrefs.GetString("FISH");
            inventory_Fishs = JsonUtility.FromJson<Serialization<InventoryFish>>(fish_Data).target;
            Debug.Log("inventory_Fishs : " + fish_Data);
        }
        if (PlayerPrefs.HasKey("DATA"))
        {
            string data_Data = PlayerPrefs.GetString("DATA");
            data = JsonUtility.FromJson<Data>(data_Data);
            Debug.Log("inventory_Items : " + data_Data);
        }  
    }

    public void SetData(string _data)
    {
        string i = "item_Name";
        string f = "fish_Name";
        string d = "score";

        // 문자열에 포함된 내용에 따라 해당 정보타입으로 바꿔주고 최신화
        if (_data.Contains(i))
        {
            inventory_Items = JsonUtility.FromJson<Serialization<InventoryItem>>(_data).target;
        }
        else if (_data.Contains(f))
        {
            inventory_Fishs = JsonUtility.FromJson<Serialization<InventoryFish>>(_data).target;
        }
        else if (_data.Contains(d))
        {
            data = JsonUtility.FromJson<Data>(_data);
        }
    }

    public void DeleteData()
    {
        /*GPGSBinder.Inst.DeleteCloud("ITEM");
        GPGSBinder.Inst.DeleteCloud("FISH");
        GPGSBinder.Inst.DeleteCloud("DATA");*/

        PlayerPrefs.DeleteAll();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
