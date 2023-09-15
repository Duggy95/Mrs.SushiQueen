using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DialogManager))]
public class StoryDialog : StoryBase
{
    public string[] dialogs;  //출력될 대사들
    DialogManager dialogManager;

    public override void Enter()
    {
        dialogManager = GetComponent<DialogManager>();
        dialogManager.Ondialog(dialogs);  //가지고 있는 대사들 큐에 넣고 출력 시작.
    }

    public override void Execute(StoryManager storyManager)
    {
        dialogManager.Next();  //다음 대사 출력 시도
        if (this.dialogManager.complete == true)
        {
            storyManager.SetNextStory();  //현재 스토리가 끝나면 다음 스토리 불러오기.
        }
    }

    public override void Exit()
    {

    }
}
