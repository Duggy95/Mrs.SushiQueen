using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDishCheck : TutorialBase
{
    public Dish dish;
    public int count;
    public override void Enter()
    {
        print("���Ե�");
    }

    public override void Execute(TutorialManager tutorialManager)
    {
        if(dish.sushiCounts.Count == count)
        {
            print("������");
            tutorialManager.SetNextTutorial();
        }
    }

    public override void Exit()
    {

    }
}
