using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DialogManager))]
public class StoryDialog : StoryBase
{
    public string[] dialogs;
    DialogManager dialogManager;

    public override void Enter()
    {
        dialogManager = GetComponent<DialogManager>();
        dialogManager.Ondialog(dialogs);
    }

    public override void Execute(StoryManager storyManager)
    {
        dialogManager.Next();
        if (this.dialogManager.complete == true)
        {
            //Destroy(this, 2);
            storyManager.SetNextStory();
        }
    }

    public override void Exit()
    {

    }
}
