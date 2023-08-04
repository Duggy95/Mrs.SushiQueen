using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraseFishing : TutorialBase
{
    public GameObject tf;
    public override void Enter()
    {

    }

    public override void Execute(TutorialManager tutorialManager)
    {
        if(tf.activeSelf == false)
        {
            tutorialManager.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        Destroy(this);
    }
}
