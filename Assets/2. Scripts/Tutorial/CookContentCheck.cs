using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookContentCheck : MonoBehaviour
{
    public TutorialCook tc;  //Ʃ�丮�� ��Ŵ���
    Text[] texts;
    void Start()
    {
        texts = GetComponentsInChildren<Text>();
    }

    void Update()
    {
        //���� ��ư���� ��� ��ĭ�� �ƴ� �� 
        if (texts[0].text != "0" && texts[1].text != "0" && texts[2].text != "0")
        {
            tc.onReady = true;
        }
    }
}
