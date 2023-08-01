using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetaButton : MonoBehaviour
{
    public GameObject netaPrefab; //회 프리팹
    public GameObject board;  //도마
    public Text text;
    public FishData fishData;  //생선 데이터
    public int count;
    public bool isEmpty = true;

    CookManager cookManager;
    Text iconTxt;

    private void Awake()
    {
        cookManager = GameObject.FindWithTag("MANAGER").GetComponent<CookManager>();
    }

    private void Start()
    {
        text = GetComponentInChildren<Text>();
    }

    public void FishBtn()
    {
        if (fishData == null)
            return;

        if(board == null)
        {
            board = GameObject.Find("Board_RawImage");  //보드 오브젝트 찾기.
        }

        Transform riceTr = board.transform.Find("Rice(Clone)").transform;  //보드 오브젝트의 자식으로 있는 Rice 찾기.

        bool hasFish = false;  //생선이 이미 존재하는 지

        //중복확인. 현재 회가 씬에 있는지 확인.
        foreach (Transform child in riceTr)
        {
            if (child.CompareTag("FISH"))
            {
                hasFish = true;
                break;
            }
        }

        if (riceTr != null && !hasFish && count > 0 && cookManager.isReady)  //생선이 없을 때만 
        {
            Vector3 netaTr = new Vector3(riceTr.position.x, riceTr.position.y + 10, 0);  //밥 조금 위쪽
            GameObject neta = Instantiate(netaPrefab,
                                                            netaTr, Quaternion.identity, riceTr); //밥 조금 위쪽에 회생성하고 밥의 자식으로 넣기.
            count--;
            UpdateUI();
            neta.GetComponent<Neta>().fishData = fishData;  //생선데이터 넘겨주기.
            riceTr.gameObject.AddComponent<DragSushi>();  //밥 오브젝트에 DragSushi 스크립트 Add.
        }
    }

    public void UpdateUI()
    {
        text.text = fishData.fishName + "     " + count;
        iconTxt.text = fishData.fishName + "     " + count;
    }

    public void Txt(Text text)
    {
        iconTxt = text.GetComponent<Text>(); 
    }
}
