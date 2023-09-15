using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    public GameObject fishIconPrefab;
    Text text;  //�ؽ�Ʈ
    NetaButton netaBtn;
    CookManager cookManager;
    Transform fishIconContent;  //���������� �г���ġ
    AudioSource audioSource;
    int count = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        text = GetComponentInChildren<Text>();
        netaBtn = GetComponent<NetaButton>();
        fishIconContent = GameObject.FindWithTag("CONTENT").GetComponent<Transform>();
        cookManager = GameObject.FindWithTag("MANAGER").GetComponent<CookManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (Drag.draggingItem == null)
            return;

        //�巡�� ���� ������Ʈ���� ������Ʈ ��������
        FishData fishData = Drag.draggingItem.GetComponent<FishSlot>().fishData;
        FishSlot fishSlot = Drag.draggingItem.GetComponent<FishSlot>();

        //�ؽ�Ʈ�� �����̸��� ��ġ�ϴ� ���� ����, ��Ŵ����� �ߺ��� �����̸��� �ִٸ�
        if (!text.text.Contains(fishData.fishName) && cookManager.fishList.Contains(fishData.fishName))
            return;

        if (transform.childCount == 2 && netaBtn.isEmpty)  //��Ÿ��ư�� ����ִٸ�
        {
            // print("���");
            Drag.draggingItem.transform.SetParent(this.transform);
            
            this.gameObject.GetComponentInChildren<Image>().sprite = fishData.netaImg;  //�̹��� ��������
            fishSlot.UpdateData();
            count += 5;  //5�� ����
            text.text = fishData.fishName + "     " + count.ToString();
            netaBtn.fishData = fishData;
            netaBtn.count = count;
            netaBtn.isEmpty = false;  //����ִ� ���°� �ƴ�
            Destroy(Drag.draggingItem.gameObject);

            GameObject fishIcon = Instantiate(fishIconPrefab, fishIconContent.transform);  //�����ܻ���
            fishIcon.GetComponentsInChildren<Image>()[1].sprite = fishData.netaImg;
            Text iconText = fishIcon.GetComponentInChildren<Text>();
            iconText.text = fishData.fishName + "     " + count;
            netaBtn.Txt(iconText);
            cookManager.fishList.Add(fishData.fishName);  //��Ŵ����� ��������Ʈ�� �߰�
            audioSource.PlayOneShot(SoundManager.instance.dropSound, 1);
        }
        else if (transform.childCount == 2 && !netaBtn.isEmpty)  //��Ÿ��ư�� ������� �ʴٸ�
        {
            fishData = Drag.draggingItem.GetComponent<FishSlot>().fishData;
            if (netaBtn.fishData.fishName == fishData.fishName)
            {
                Drag.draggingItem.transform.SetParent(this.transform);
                Drag.draggingItem.GetComponent<FishSlot>().UpdateData();
                count += 5;
                netaBtn.count = count;
                netaBtn.UpdateUI();
                audioSource.PlayOneShot(SoundManager.instance.dropSound, 1);
                Destroy(Drag.draggingItem.gameObject);
            }
            else
                return;
        }
    }
}
