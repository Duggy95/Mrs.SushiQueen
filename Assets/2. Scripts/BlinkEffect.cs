using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{

    void OnEnable()
    {
        StartCoroutine(BlinkEff());
    }
    
    // �����Ÿ��� ȿ��
    IEnumerator BlinkEff()
    {
        yield return null;
    }
}
