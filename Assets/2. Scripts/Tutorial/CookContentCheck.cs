using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookContentCheck : MonoBehaviour
{
    public TutorialCook tc;  //튜토리얼 쿡매니저
    Text[] texts;
    void Start()
    {
        texts = GetComponentsInChildren<Text>();
    }

    void Update()
    {
        //생선 버튼들이 모두 빈칸이 아닐 때 
        if (texts[0].text != "0" && texts[1].text != "0" && texts[2].text != "0")
        {
            tc.onReady = true;
        }
    }
}
