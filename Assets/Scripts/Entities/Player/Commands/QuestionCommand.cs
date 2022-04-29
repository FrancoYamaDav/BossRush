using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestionCommand : ICommand
{
    protected ICommand trueCommand, falseCommand;
    protected bool condition;

    public virtual void Execute()
    {
        if(condition) trueCommand.Execute();
        else falseCommand.Execute();
    }

    public virtual void OnExit()
    {
        if (condition) trueCommand.OnExit();
        else falseCommand.OnExit();
    }
}
