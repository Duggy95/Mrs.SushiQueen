using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{

    void OnEnable()
    {
        StartCoroutine(BlinkEff());
    }
    
    // ±ôºý°Å¸®´Â È¿°ú
    IEnumerator BlinkEff()
    {
        yield return null;
    }
}
