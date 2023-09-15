using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    public GameObject fishIconPrefab;
    Text text;  //텍스트
    NetaButton netaBtn;
    CookManager cookManager;
    Transform fishIconContent;  //생선아이콘 패널위치
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

        //드래그 중인 오브젝트에서 컴포넌트 가져오기
        FishData fishData = Drag.draggingItem.GetComponent<FishSlot>().fishData;
        FishSlot fishSlot = Drag.draggingItem.GetComponent<FishSlot>();

        //텍스트에 생선이름과 일치하는 것이 없고, 쿡매니저에 중복된 생선이름이 있다면
        if (!text.text.Contains(fishData.fishName) && cookManager.fishList.Contains(fishData.fishName))
            return;

        if (transform.childCount == 2 && netaBtn.isEmpty)  //네타버튼이 비어있다면
        {
            // print("드랍");
            Drag.draggingItem.transform.SetParent(this.transform);
            
            this.gameObject.GetComponentInChildren<Image>().sprite = fishData.netaImg;  //이미지 가져오기
            fishSlot.UpdateData();
            count += 5;  //5씩 증가
            text.text = fishData.fishName + "     " + count.ToString();
            netaBtn.fishData = fishData;
            netaBtn.count = count;
            netaBtn.isEmpty = false;  //비어있는 상태가 아님
            Destroy(Drag.draggingItem.gameObject);

            GameObject fishIcon = Instantiate(fishIconPrefab, fishIconContent.transform);  //아이콘생성
            fishIcon.GetComponentsInChildren<Image>()[1].sprite = fishData.netaImg;
            Text iconText = fishIcon.GetComponentInChildren<Text>();
            iconText.text = fishData.fishName + "     " + count;
            netaBtn.Txt(iconText);
            cookManager.fishList.Add(fishData.fishName);  //쿡매니저에 생선리스트에 추가
            audioSource.PlayOneShot(SoundManager.instance.dropSound, 1);
        }
        else if (transform.childCount == 2 && !netaBtn.isEmpty)  //네타버튼이 비어있지 않다면
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
