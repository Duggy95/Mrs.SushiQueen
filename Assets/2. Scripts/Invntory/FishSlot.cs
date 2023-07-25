using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FishSlot : MonoBehaviour
{
    public FishData[] fishDatas;
    public FishData fishData;
    Text _text;

    public bool isEmpty = false;
    public int fish_GradeNum;
    public int fish_ColorNum;
    public string fish_Name;

    private void Start()
    {
        _text = GetComponentInChildren<Text>();
        fish_Name = _text.text.Split(" ")[0];

        print(fish_Name);
        // 배열을 순회하면서 이름이 같은 요소를 찾기 위한 루프
        for (int i = 0; i < fishDatas.Length; i++)
        {
            // 배열의 각 요소의 이름과 비교하여 같은 이름을 가진 요소를 찾음
            if (fishDatas[i].fishName == fish_Name)
            {
                fishData = fishDatas[i];
                print(fishData.fishName);
                break;
            }
        }
    }

    public void UpdateData()
    {
        string[] slotInfo = _text.text.Split(" ");
        string fishName = slotInfo[0];
        print(fishName);
        string valueToFind = _text.text;

        int newValue = 1;
        // 특정 값(valueToFind)을 만족하는 첫 번째 요소의 인덱스를 찾기
        int index = GameManager.instance.inventory_Fishs.FindIndex(item => item.fish_Name == valueToFind);

        if (index != -1)
        {
            int count = int.Parse(GameManager.instance.inventory_Fishs[index].fish_Count) - newValue;

            // 해당 인덱스(index)의 값 변경
            GameManager.instance.inventory_Fishs[index].fish_Count = count.ToString();
            _text.text = fishName + "   " + count + "마리";
            GameManager.instance.Save("f");
            if (count <= 0)
            {
                GameManager.instance.inventory_Fishs.RemoveAt(index);
                gameObject.GetComponentInChildren<Image>().sprite = null;
                GetComponentInChildren<Text>().text = "빈 공간";
                GameManager.instance.Save("f");
            }
        }
    }
}
