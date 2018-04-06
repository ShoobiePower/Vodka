using UnityEngine;
using System.Collections;

public class CommandWithUndo : Command, ICommandWithUndo
{
    protected Colleague TargetColleague { get; set; }                   
    public UndoCommand UndoCommand { get; set; }

    public CommandWithUndo() : base()
    {

    }

    public override void Execute(Colleague targetColleague)
    {
        this.TargetColleague = targetColleague;   
        base.Execute(targetColleague);
    }
    public void UnExecute()
    {
        this.UndoCommand.Execute(TargetColleague);
    }
}
