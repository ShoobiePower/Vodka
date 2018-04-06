using UnityEngine;
using System.Collections;

public interface ICommandWithUndo : ICommand
{
    UndoCommand UndoCommand { get; set; }
}
