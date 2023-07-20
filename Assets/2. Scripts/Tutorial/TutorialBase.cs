using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialBase : MonoBehaviour
{
    //�ش� Ʃ�丮�� ������ ������ �� 1ȸ ȣ��.
    public abstract void Enter();

    //�ش� Ʃ�丮�� ������ �����ϴ� ���� �� ������ ȣ��.
    public abstract void Execute(TutorialManager tutorialManager);

    //�ش� Ʃ�丮�� ������ ������ �� 1ȸ ȣ��.
    public abstract void Exit();
}
