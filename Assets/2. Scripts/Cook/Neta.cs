using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Neta : MonoBehaviour
{
    public FishData fishData;  //생선 데이터
    Image image;  //회 이미지

    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = fishData.netaImg;  //이미지를 생선 데이터의 회 이미지로.

        Sushi sushi = GetComponentInParent<Sushi>(); // 부모 오브젝트인 스시에서 스크립트 가져오기
        sushi.sushiName = fishData.fishName;  //초밥의 이름을 생선 데이터의 생선 이름으로
        //print(rice.sushiName);
    }
}
