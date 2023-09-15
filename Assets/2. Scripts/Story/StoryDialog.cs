using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DialogManager))]
public class StoryDialog : StoryBase
{
    public string[] dialogs;  //��µ� ����
    DialogManager dialogManager;

    public override void Enter()
    {
        dialogManager = GetComponent<DialogManager>();
        dialogManager.Ondialog(dialogs);  //������ �ִ� ���� ť�� �ְ� ��� ����.
    }

    public override void Execute(StoryManager storyManager)
    {
        dialogManager.Next();  //���� ��� ��� �õ�
        if (this.dialogManager.complete == true)
        {
            storyManager.SetNextStory();  //���� ���丮�� ������ ���� ���丮 �ҷ�����.
        }
    }

    public override void Exit()
    {

    }
}
