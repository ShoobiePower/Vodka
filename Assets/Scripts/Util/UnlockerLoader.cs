using UnityEngine;
using System.Collections;

public class UnlockerLoader{

    public Unlocker createUnlocker(string whatThisUnlocks, JSONObject unlockerComponents)
    {
        Unlocker unlockerToAdd = new Unlocker();
        unlockerToAdd.setWhenToUnlock(unlockerComponents[0].str);
        unlockerToAdd.NameOfThingToUnlock = unlockerComponents[1].str;
        unlockerToAdd.setWhatThisUnlocks(whatThisUnlocks);
        if (unlockerComponents[2] != null) { unlockerToAdd.SpecifierForNameOfThingToUnlock = unlockerComponents[2].str; }
        return unlockerToAdd;
    }

}
