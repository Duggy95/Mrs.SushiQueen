using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public bool isEmpty = false;

    FishingManager fm;
    Button button;
    Text _text;
    AudioSource audioSource;

    bool isReturn = false; // 매니저가 널인지 판단하여 널일 경우 리턴

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        fm = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<FishingManager>();
        if (fm != null)
        {
            button = GetComponent<Button>(); //버튼 component 가져오기
            button.onClick.AddListener(() => UseItem()); //인자가 없을 때 함수 호출
            _text = GetComponentInChildren<Text>();
        }
        else
        {
            isReturn = true;
        }
    }

    public void UseItem()
    {
        if (isReturn)
            return;

        if (fm.useItem_white || fm.useItem_red || fm.useItem_rare || fm.isFishing)
            return;

        if (_text.text.Contains("지렁이"))
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

            fm.useItem_white = true;
            fm.useItemPanel.gameObject.SetActive(true);
            fm.useWhiteItemTxt.gameObject.SetActive(true);
            print("지렁이 사용");
        }

        else if (_text.text.Contains("새우"))
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

            fm.useItem_red = true;
            fm.useItemPanel.gameObject.SetActive(true);
            fm.useRedItemTxt.gameObject.SetActive(true);
            print("새우 사용");
        }

        else if (_text.text.Contains("생선살"))
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

            fm.useItem_rare = true;
            fm.useItemPanel.gameObject.SetActive(true);
            fm.useRareItemTxt.gameObject.SetActive(true);
            print("생선살 사용");
        }

        UpdateData();
    }

    void UpdateData()
    {
        string[] slotInfo = _text.text.Split(" ");
        string itemName = slotInfo[0];
        string valueToFind = itemName;

        int newValue = 1;
        // 특정 값(valueToFind)을 만족하는 첫 번째 요소의 인덱스를 찾기
        int index = GameManager.instance.inventory_Items.FindIndex(item => item.item_Name == valueToFind);

        if (index != -1)
        {
            int count = int.Parse(GameManager.instance.inventory_Items[index].item_Count) - newValue;

            // 해당 인덱스(index)의 값 변경
            GameManager.instance.inventory_Items[index].item_Count = count.ToString();
            _text.text = itemName + "   " + count + "개";
            if (count <= 0)
            {
                GameManager.instance.inventory_Items.RemoveAt(index);
                Image[] nullImg = gameObject.GetComponentsInChildren<Image>();
                gameObject.GetComponent<CanvasGroup>().interactable = false;
                Text _text = GetComponentInChildren<Text>();
                foreach (Image image in nullImg)
                {
                    if (image.name.Contains("Img"))
                    {
                        image.sprite = null;
                        _text.text = "";
                    }
                }
            }
        }
    }
}
