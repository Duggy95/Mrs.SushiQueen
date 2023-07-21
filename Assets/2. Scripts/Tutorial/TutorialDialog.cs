using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogSystem))]
public class TutorialDialog : TutorialBase
{
    DialogSystem dialogSystem;
    public override void Enter()
    {
        dialogSystem = GetComponent<DialogSystem>();
        dialogSystem.Setup();
    }

    public override void Execute(TutorialManager tutorialManager)
    {
        bool isCompleted = dialogSystem.UpdateDialog();

        if(isCompleted == true)
        {
            tutorialManager.SetNextTutorial();
        }
    }

    public override void Exit()
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
