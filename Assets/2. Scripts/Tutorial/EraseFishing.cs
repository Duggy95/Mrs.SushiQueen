using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraseFishing : TutorialBase
{
    public GameObject tf;  //Ʃ�丮�� �ǽ� �Ŵ���

    public override void Enter()
    {

    }

    public override void Execute(TutorialManager tutorialManager)
    {
        if(tf.activeSelf == false)  //Ʃ�丮�� �ǽ� �Ŵ����� �������
        {
            tutorialManager.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        Destroy(this);
    }
}
