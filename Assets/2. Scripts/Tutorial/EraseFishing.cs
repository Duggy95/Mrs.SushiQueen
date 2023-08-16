using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraseFishing : TutorialBase
{
    public GameObject tf;  //튜토리얼 피쉬 매니저

    public override void Enter()
    {

    }

    public override void Execute(TutorialManager tutorialManager)
    {
        if(tf.activeSelf == false)  //튜토리얼 피쉬 매니저가 사라지면
        {
            tutorialManager.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        Destroy(this);
    }
}
