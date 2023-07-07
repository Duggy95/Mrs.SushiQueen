using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class FishingManager : MonoBehaviour, IPointerClickHandler
{
    public GameObject hpBarPrefab;

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 clickPos = eventData.position;
        var fishHpBar = Instantiate(hpBarPrefab, clickPos, new Quaternion(0, 0, 0, 0));
    }

    public void GoCookScene()
    {
        SceneManager.LoadScene(2);
    }
}
