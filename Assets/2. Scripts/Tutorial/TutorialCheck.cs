using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCheck : TutorialBase
{
    public TutorialFishing tf;
    public override void Enter()
    {

    }

    public override void Execute(TutorialManager tutorialManager)
    {
        if(tf.fishCome)
        {
            tutorialManager.SetNextTutorial();
            //Destroy(this);
        }
    }

    public override void Exit()
    {
    }
}
