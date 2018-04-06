using UnityEngine;
using System.Collections;

public class UndoCommand : Command
{
    public UndoCommand(CommandWithUndo command)
    {
        this.CommandName = "Undo " + command.CommandName;
    }
}
