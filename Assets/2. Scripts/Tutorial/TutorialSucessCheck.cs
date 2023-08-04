using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSucessCheck : TutorialBase
{
    public TutorialCook tc;

    public override void Enter()
    {

    }

    public override void Execute(TutorialManager tutorialManager)
    {
        /*print("¼º°ø È½¼ö : " + tc.sucessCount);

        if(tc.sucessCount >= 3)
        {
            tutorialManager.SetNextTutorial();
            Destroy(this);
        }*/
    }

    public override void Exit()
    {

    }
}
