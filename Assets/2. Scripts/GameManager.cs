using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Data;
using System.IO;

[System.Serializable]
public class Serialization<T> 
{
    public Serialization(List<T> _target) => target = _target;
    public List<T> target;
}

[System.Serializable]
public class InventoryItem
{
   /* public InventoryItem(string _item_Name, string _item_Count)
    { item_Name = _item_Name; item_Count = _item_Count; }*/

    public string item_Name;
    public string item_Count;
}

/*public class InventoryItemList
{
    public List<InventoryItem> _Data;
}*/

[System.Serializable]
public class InventoryFish
{
   /* public InventoryFish(string _fish_Name, string _fish_Gold)
    { fish_Name = _fish_Name; fish_Gold = _fish_Gold; }*/

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

    public List<InventoryItem> inventory_Items = new List<InventoryItem>();
    public List<InventoryFish> inventory_Fishs = new List<InventoryFish>();
    public Data data = new Data();
    public Text logTxt;  //-----------------------
    public Text idTxt;  //-----------------------
    public Text saveTxt;  //-----------------------

    public bool nextStage;
    public bool onLogin = false;

    private void Awake()
    {
        if (instance != this) // 싱글톤된 게 자신이 아니라면 삭제
        {
            Destroy(gameObject);
        }

        else
            DontDestroyOnLoad(gameObject);

        //Load();
        Debug.Log("load");
    }

    public void Save(string type)
    {
        // 타입에 따라 해당 정보를 json화 해서 문자열로 바꾸고 전달
        if (type == "i")
        {
            string item_Json = JsonUtility.ToJson(new Serialization<InventoryItem>(inventory_Items));
            GoogleManager.instance.SaveToCloud_Item(item_Json);
            Debug.Log("Data_Item" + item_Json);
            saveTxt.text = "Data_Item : " + item_Json;  //-----------------------
        }
        else if (type == "f")
        {
            string fish_Json = JsonUtility.ToJson(new Serialization<InventoryFish>(inventory_Fishs));
            GoogleManager.instance.SaveToCloud_Fish(fish_Json);
            Debug.Log("Data_Fish" + fish_Json);
            saveTxt.text = "Data_Fish : " + fish_Json;  //-----------------------

        }
        else if (type == "s")
        {
            string data_Json = JsonUtility.ToJson(data);
            GoogleManager.instance.SaveToCloud_Data(data_Json);
            Debug.Log("Data_Data" + data_Json);
            saveTxt.text = "Data_Data : " + data_Json;  //-----------------------
        }
    }

    public void GetLog()
    {
        logTxt = GameObject.FindGameObjectWithTag("LOG").GetComponent<Text>();
        idTxt = GameObject.FindGameObjectWithTag("ID").GetComponent<Text>();
        saveTxt = GameObject.FindGameObjectWithTag("SAVE").GetComponent<Text>();
    }

    public void Load()
    {
        Debug.Log("load_cloud");

        GoogleManager.instance.LoadFromCloud_Fish();
        GoogleManager.instance.LoadFromCloud_Item();
        GoogleManager.instance.LoadFromCloud_Data();
    }

    public void SetData(string _data)
    {
        string i = "item_Name";
        string f = "fish_Name";
        string d = "score";

        Debug.Log("loadData" + _data);
        logTxt.text = "loadData : " + _data;  //-----------------------

        // 문자열에 포함된 내용에 따라 해당 정보타입으로 바꿔주고 최신화
        if (_data.Contains(i))
        {
            inventory_Items = JsonUtility.FromJson<List<InventoryItem>>(_data);
            saveTxt.text = "inventory_Items : " + inventory_Items;  //-----------------------
        }
        else if (_data.Contains(f))
        {
            inventory_Fishs = JsonUtility.FromJson<List<InventoryFish>>(_data);
            saveTxt.text = "inventory_Fishs : " + inventory_Fishs;  //-----------------------
        }
        else if (_data.Contains(d))
        {
            data = JsonUtility.FromJson<Data>(_data);
            saveTxt.text = "data : " + data;  //-----------------------
        }
        else
        {
            saveTxt.text = "inventory_Items : " + inventory_Items;
            saveTxt.text += "\ninventory_Fishs : " + inventory_Fishs;  //-----------------------
            saveTxt.text += "\ndata : " + data;  //-----------------------
        }
    }
}
