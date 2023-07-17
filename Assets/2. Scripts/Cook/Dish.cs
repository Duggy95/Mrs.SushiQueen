using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    //public List<Sushi> sushiList;
    public Dictionary<string, int> sushiCounts;
    Transform tr;

    void Start()
    {
        //sushiList = new List<Sushi>();  //초밥종류, 와사비
        sushiCounts = new Dictionary<string, int>();  //초밥종류 + 와사비, 갯수
        tr = GetComponent<RectTransform>();
    }

    void Update()
    {
        // UpdateDish();
    }

    public void AddSushi(string sushiName, string wasabi) //초밥 저장. DragSushi에서 초밥 접시에 놓을 때 호출.
    {
        Sushi sushi = new Sushi(sushiName, wasabi);
        string sushiKey = sushi.sushiName + " _" + sushi.wasabi+ " ";  //초밥종류 + 와사비를 딕셔너리 Key로.

        if (sushiCounts.ContainsKey(sushiKey))  //만약 이미 딕셔너리에 키가 존재하면
        {
            sushiCounts[sushiKey]++;  //갯수 증가
        }
        else  //없으면
        {
            sushiCounts[sushiKey] = 1;  //갯수는 1
        }
        //sushiList.Add(sushi);
    }

    public void UpdateDish()
    {
        foreach (var sushi in sushiCounts)
        {
            string sushiInfo = sushi.Key; 
            int count = sushi.Value;

            Debug.Log($"초밥 정보: {sushiInfo}, 중복 횟수: {count}");
        }
    }

    public void ClearSushi()  //접시 위 초밥들 삭제 메서드.
    {
        foreach (RectTransform child in tr)
        {
            Destroy(child.gameObject);
        }
    }
}
