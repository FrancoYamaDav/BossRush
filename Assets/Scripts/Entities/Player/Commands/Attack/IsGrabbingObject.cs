using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrabbingObject : QuestionCommand
{
    public IsGrabbingObject(ICommand t, ICommand f, bool con)
    {
        trueCommand = t;
        falseCommand = f;
        condition = con;
    }
}
