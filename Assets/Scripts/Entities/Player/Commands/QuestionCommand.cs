using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestionCommand : ICommand
{
    protected ICommand trueCommand, falseCommand;
    protected bool condition;

    public void Execute(float val = 0)
    {
        if(condition) trueCommand.Execute(val);
        else falseCommand.Execute(val);
    }
}
