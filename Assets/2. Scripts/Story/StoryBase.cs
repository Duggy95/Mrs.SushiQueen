using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StoryBase : MonoBehaviour
{
    public abstract void Enter();

    public abstract void Execute(StoryManager storyManager);

    public abstract void Exit();
}
