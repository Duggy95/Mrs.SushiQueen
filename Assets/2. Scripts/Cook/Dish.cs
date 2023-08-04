using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    public Dictionary<string, int> sushiCounts;  //만든 초밥 저장 딕셔너리
    public List<Sushi> sushiList;  //초밥 리스트
    public Transform boardTr;  //도마 위치

    AudioSource audioSource;
    Transform tr;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sushiList = new List<Sushi>();  //초밥종류, 와사비, 골드
        sushiCounts = new Dictionary<string, int>();  //초밥종류 + 와사비, 갯수
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        // UpdateDish();
    }

    public void AddSushi(string sushiName, string wasabi, int gold) //초밥 저장. DragSushi에서 초밥 접시에 놓을 때 호출.
    {
        Sushi sushi = new Sushi(sushiName, wasabi, gold);
        string sushiKey = sushi.sushiName + "_" + sushi.wasabi;  //초밥종류 + 와사비를 딕셔너리 Key로.
        sushiList.Add(sushi);  //리스트에도 추가
        if (sushiCounts.ContainsKey(sushiKey))  //만약 이미 딕셔너리에 키가 존재하면
        {
            sushiCounts[sushiKey]++;  //갯수 증가
        }
        else  //없으면
        {
            sushiCounts[sushiKey] = 1;  //갯수는 1
        }
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
        foreach (Transform child in tr)
        {
            Destroy(child.gameObject);
        }
    }

    public void ClearBoard()  //도마 위 초밥 삭제 메서드.
    {
        foreach (Transform child in boardTr)
        {
            Destroy(child.gameObject);
        }
    }

    public void Clear()  //도마 위 초밥 삭제 메서드.
    {
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

        foreach (Transform child in boardTr)
        {
            Destroy(child.gameObject);
        }
    }
}
