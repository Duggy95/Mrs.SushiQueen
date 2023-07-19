using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using Newtonsoft.Json;

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
    public InventoryFish(string _fish_Name, string _fish_Gold)
    { fish_Name = _fish_Name; fish_Gold = _fish_Gold; }

    public string fish_Name;
    public string fish_Gold;
}

[System.Serializable]
public class Save
{
    public Save(string _itemCount, string _fishCount, string _dateCount, string _score, string _gold, string _atk)
    { itemCount = _itemCount; fishCount = _fishCount; dateCount = _dateCount; score = _score; gold = _gold; atk = _atk; }

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

    //public TextAsset inventory_Item;
    public List<InventoryItem> inventory_Items;
    //public TextAsset inventory_Fish;
    public List<InventoryFish> inventory_Fishs;
    //public TextAsset save;
    public List<Save> save;

    public string itemCount;
    public string fishCount;

    public string dateCount;  //  날짜
    public string score;  // 점수
    public string gold;  // 골드
    public string atk;

    public bool nextStage;

    private void Awake()
    {
        if (instance != this) // 싱글톤된 게 자신이 아니라면 삭제
        {
            Destroy(gameObject);
        }

        else
            DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Load();
    }

    public void Save(string type)
    {
        if (type == "i")
        {
            string item_Json = JsonUtility.ToJson(new Serialization<InventoryItem>(inventory_Items));
            GoogleManager.instance.SaveToCloud_Item(item_Json);
        }
        else if (type == "f")
        {
            string fish_Json = JsonUtility.ToJson(new Serialization<InventoryFish>(inventory_Fishs));
            GoogleManager.instance.SaveToCloud_Fish(fish_Json);
        }
        else if (type == "s")
        {
            string save_Json = JsonUtility.ToJson(new Serialization<Save>(save));
            GoogleManager.instance.SaveToCloud_Save(save_Json);
        }
    }

    public void Load()
    {
        GoogleManager.instance.LoadFromCloud_Fish();
        GoogleManager.instance.LoadFromCloud_Item();
        GoogleManager.instance.LoadFromCloud_Save();
    }

    private void OnEnable()
    {
        /*if (PlayerPrefs.HasKey("GOLD"))
        {
            gold = PlayerPrefs.GetInt("GOLD");
        }
        else
            SetGold();

        if (PlayerPrefs.HasKey("DATE"))
        {
            dateCount = PlayerPrefs.GetInt("DATE");
        }
        else
            SetDate();

        if (PlayerPrefs.HasKey("SCORE"))
        {
            score = PlayerPrefs.GetInt("SCORE");
        }
        else
            SetScore();

        if (PlayerPrefs.HasKey("ITEM"))
        {
            itemCount = PlayerPrefs.GetInt("ITEM");
        }
        else
            SetItem();

        if (PlayerPrefs.HasKey("FISH"))
        {
            fishCount = PlayerPrefs.GetInt("FISH");
        }
        else
            SetFish();

        if (PlayerPrefs.HasKey("ATK"))
        {
            atk = PlayerPrefs.GetInt("ATK");
        }
        else
            SetAtk();*/

    }

    /*public void SetGold()
    {
        PlayerPrefs.SetInt("GOLD", gold);
    }

    public void SetDate()
    {
        PlayerPrefs.SetInt("DATE", dateCount);
    }

    public void SetScore()
    {
        PlayerPrefs.SetInt("SCORE", score);
    }

    public void SetItem()
    {
        PlayerPrefs.SetInt("ITEM", itemCount);
    }

    public void SetFish()
    {
        PlayerPrefs.SetInt("FISH", fishCount);
    }

    public void SetAtk()
    {
        PlayerPrefs.SetInt("ATK", atk);
    }*/

    private void OnDisable()
    { 
        //PlayerPrefs.Save();
    }
}
