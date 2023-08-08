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

    bool isReturn = false; // �Ŵ����� ������ �Ǵ��Ͽ� ���� ��� ����

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        fm = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<FishingManager>();
        if (fm != null)
        {
            button = GetComponent<Button>(); //��ư component ��������
            button.onClick.AddListener(() => UseItem()); //���ڰ� ���� �� �Լ� ȣ��
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

        if (_text.text.Contains("������"))
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

            fm.useItem_white = true;
            fm.useItemPanel.gameObject.SetActive(true);
            fm.useWhiteItemTxt.gameObject.SetActive(true);
            print("������ ���");
        }

        else if (_text.text.Contains("����"))
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

            fm.useItem_red = true;
            fm.useItemPanel.gameObject.SetActive(true);
            fm.useRedItemTxt.gameObject.SetActive(true);
            print("���� ���");
        }

        else if (_text.text.Contains("������"))
        {
            audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);

            fm.useItem_rare = true;
            fm.useItemPanel.gameObject.SetActive(true);
            fm.useRareItemTxt.gameObject.SetActive(true);
            print("������ ���");
        }

        UpdateData();
    }

    void UpdateData()
    {
        string[] slotInfo = _text.text.Split(" ");
        string itemName = slotInfo[0];
        string valueToFind = itemName;

        int newValue = 1;
        // Ư�� ��(valueToFind)�� �����ϴ� ù ��° ����� �ε����� ã��
        int index = GameManager.instance.inventory_Items.FindIndex(item => item.item_Name == valueToFind);

        if (index != -1)
        {
            int count = int.Parse(GameManager.instance.inventory_Items[index].item_Count) - newValue;

            // �ش� �ε���(index)�� �� ����
            GameManager.instance.inventory_Items[index].item_Count = count.ToString();
            _text.text = itemName + "   " + count + "��";
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
