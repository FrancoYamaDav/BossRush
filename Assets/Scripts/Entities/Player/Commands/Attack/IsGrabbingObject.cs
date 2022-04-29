using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrabbingObject : QuestionCommand
{
    PlayerController _pc;
    public IsGrabbingObject(ICommand t, ICommand f, PlayerController pc)
    {
        trueCommand = t;
        falseCommand = f;
        _pc = pc;
    }

    public override void Execute()
    {
        condition = _pc.isGrabbing;
        base.Execute();
    }
}
