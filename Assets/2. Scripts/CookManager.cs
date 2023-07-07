using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CookManager : MonoBehaviour
{
    public GameObject orderView;
    public GameObject cookView;

    int count = 0;

    void Start()
    {
        orderView.SetActive(true);
        cookView.SetActive(false);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && count % 2 == 0)
        {
            orderView.SetActive(false);
            cookView.SetActive(true);
        }

        else
        {
            orderView.SetActive(true);
            cookView.SetActive(false);
        }
    }

    public void GoEndScene()
    {
        SceneManager.LoadScene(3);
    }
}
