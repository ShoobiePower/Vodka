using UnityEngine;
using System.Collections;


public class JsonDialogueLoader : Loader
{ // Days are numbered, soon I'll replace this with NewJsonDialogue Loader.

    private static JsonDialogueLoader instance = null;
    private static readonly object padloc = new object();

    public enum responceType { RUMOR, QUESTRETURN, CHAINRETURN, DRINKWITH,DRINKWITHOUT, GOQUEST, TALK, ABOUTTOLEAVE, NOMOREQUEST, WAITINBAR }; 
   


    public static JsonDialogueLoader Instance
    {
        get
        {
            lock (padloc)
            {

                if (instance == null)
                {
                    instance = new JsonDialogueLoader();
                    instance.init();

                }
                return instance;
            }
        }
    }




    public override void init()
    {
        loadJson("/JsonFiles/Dialog.json");
    }

    public string dioOut(responceType type, int patronID)
    {
        string dioToSend = @jsonObject[patronID][(int)type][Random.Range(0, jsonObject[patronID][(int)type].Count)].str;
        return dioToSend;
    }

    public string dioOut(responceType type, string patronName)
    {
        string dioToSend = @jsonObject[patronName][(int)type][Random.Range(0, jsonObject[patronName][(int)type].Count)].str;
        return dioToSend;
    }

    public string specificDioOutByIndex(responceType type, byte indexToGet, string patronName)
    {
        string dioToSend = @jsonObject[patronName][(int)type][indexToGet].str;
        if (dioToSend == null) { dioToSend = dioOut(type, patronName); }
        return dioToSend;
    }
}


