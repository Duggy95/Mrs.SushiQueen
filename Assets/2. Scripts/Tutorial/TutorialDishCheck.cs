using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDishCheck : TutorialBase
{
    public Dish dish;

    public override void Enter()
    {

    }

    public override void Execute(TutorialManager tutorialManager)
    {
        if(dish.sushiCounts.Count == 1)
        {
            tutorialManager.SetNextTutorial();
            Destroy(this);
        }
    }

    public override void Exit()
    {

    }
}
