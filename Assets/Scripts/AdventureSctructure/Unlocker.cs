using UnityEngine;
using System.Collections;

public class Unlocker
{
    private string nameOfthingToUnlock;
    public string NameOfThingToUnlock { get { return nameOfthingToUnlock; } set { nameOfthingToUnlock = value; } }

    private string specifierForNameOfThingToUnlock;
    public string SpecifierForNameOfThingToUnlock { get { return specifierForNameOfThingToUnlock; } set { specifierForNameOfThingToUnlock = value; } }


    public enum WhenToUnlock {WIN, LOSE, WINORLOSE };
    private WhenToUnlock whenIsthisUnlocked; 
    public WhenToUnlock WhenIsThisUnlocked { get { return whenIsthisUnlocked; } }


    public void setWhenToUnlock(string whenToUnlockIn)
    {
        switch (whenToUnlockIn.ToLower())
        {

            case "win":
                {
                    whenIsthisUnlocked = WhenToUnlock.WIN;
                    break;
                }
            case "fail":
                {
                    whenIsthisUnlocked = WhenToUnlock.LOSE;
                    break;
                }
            case "both":
                {
                    whenIsthisUnlocked = WhenToUnlock.WINORLOSE;
                    break;

                }
            default:
                {
                    Debug.Log("when Unlock Fallthrough:");
                    whenIsthisUnlocked = WhenToUnlock.WIN;
                    break;

                }
        }
    }

    public enum WhatUnlocks {INSTANTRUMOR,RUMOR,PATRON,EVENT,LOCATION, CONVERSATION}
    private WhatUnlocks whatDoesThisUnlock;
    public WhatUnlocks WhatDoesThisUnlock { get { return whatDoesThisUnlock; }  }


    public void setWhatThisUnlocks(string whatToUnlockIn)
    {
        switch (whatToUnlockIn)
        {
            case "RUMORONRETURN":
                {
                    whatDoesThisUnlock = WhatUnlocks.INSTANTRUMOR;
                    break;
                }

            case "RUMORUNLOCK":
                {
                    whatDoesThisUnlock = WhatUnlocks.RUMOR;
                    break;
                }
            case "PATRONUNLOCK":
                {
                    whatDoesThisUnlock = WhatUnlocks.PATRON;
                    break;
                }
            case "EVENTUNLOCK":
                {
                    whatDoesThisUnlock = WhatUnlocks.EVENT;
                    break;
                }
            case "LOCATIONUNLOCK":
                {
                    whatDoesThisUnlock = WhatUnlocks.LOCATION;
                    break;
                }
            case "CONVERSATIONUNLOCK":
                {
                    whatDoesThisUnlock = WhatUnlocks.CONVERSATION;
                    break;
                }

        }
    }

}

