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
        //������ ���Կ� ������ �ֱ�
        itemSlotImg.sprite = Resources.Load("White", typeof(Sprite)) as Sprite;
        itemSlotTxt.text = "������   1��";
        itemSlot.interactable = true;
    }

    public override void Execute(TutorialManager tutorialManager)
    {
        //������ ������ ���� Ʃ�丮��
        if(itemSlotImg != null && itemSlotTxt != null)
        {
            tutorialManager.SetNextTutorial();
        }
    }

    public override void Exit()
    {
    }
}
