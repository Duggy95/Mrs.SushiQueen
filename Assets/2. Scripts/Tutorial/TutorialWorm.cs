using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialWorm : TutorialBase
{
    public Image itemSlotImg;
    public Text itemSlotTxt;
    public CanvasGroup itemSlot;

    public override void Enter()
    {
        //아이템 슬롯에 지렁이 넣기
        itemSlotImg.sprite = Resources.Load("White", typeof(Sprite)) as Sprite;
        itemSlotTxt.text = "지렁이   1개";
        itemSlot.interactable = true;
    }

    public override void Execute(TutorialManager tutorialManager)
    {
        //지렁이 들어갔으면 다음 튜토리얼
        if(itemSlotImg != null && itemSlotTxt != null)
        {
            tutorialManager.SetNextTutorial();
        }
    }

    public override void Exit()
    {
    }
}
