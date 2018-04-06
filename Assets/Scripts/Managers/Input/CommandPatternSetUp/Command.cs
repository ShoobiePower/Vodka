using UnityEngine;
using System.Collections;

    public class Command : ICommand
    {
        public string CommandName;       //Name For logging

        public Command()
        {

        }

        public virtual void Execute(Colleague colleagueToTarget) // Changed from game object to new bar manager
        {
            this.Log();     //Log that command happened;
        }

        protected virtual string Log()
        {

            string LogString = string.Format("{0} executed.", CommandName);
#if DEBUG
            //Only write to console if run in Debug
            //Debug.Log(LogString);
#endif
            return LogString;
        }
    }

