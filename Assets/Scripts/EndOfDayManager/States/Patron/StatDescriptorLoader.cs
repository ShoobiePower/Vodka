using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class StatDescriptorLoader {


    //    private static StatDescriptorLoader instance = null;
    //    private static readonly object padloc = new object();

    //private Patron.StatTypes typeToFind;
    //    //{ GREET, TALK, ADVENTURE, DECLINE, DRINK, ACCEPT, RUMOR, EXIT, DRUNK, ABOUTDRUNK };
    //    string path;
    //    string jsonString;
    //    JSONObject statToReturn;


    //    public static StatDescriptorLoader Instance
    //    {
    //        get
    //        {
    //            lock (padloc)
    //            {

    //                if (instance == null)
    //                {
    //                    instance = new StatDescriptorLoader();
    //                    instance.StatDescriptorInit();

    //                }
    //                return instance;
    //            }
    //        }
    //    }




    //    public void StatDescriptorInit()
    //    {
    //    #if UNITY_EDITOR || UNITY_STANDALONE_WIN
    //    path = Application.streamingAssetsPath + "/JsonFiles/StatDescriptionLookUp.json";
    //    #endif

    //    #if UNITY_ANDROID
    //    path = Path.Combine("jar:file://" + Application.streamingAssetsPath + "!assets/", Application.streamingAssetsPath + "/JsonFiles/StatDescriptionLookUp.json");
    //    #endif

    //        jsonString = File.ReadAllText(path);
    //        statToReturn = new JSONObject(jsonString);
    //    }

    //    public string statOut(Patron.StatTypes statToFind, int levelOfStat)
    //    {
    //     byte numberToFind = numberModifyer((int)statToFind,levelOfStat);
    //     string statToSend = statToReturn[(byte)statToFind][numberToFind].str; // possibly the worst? Ill see if I can optimize this at some point. Kinda a mess to read too.
    //      return statToSend;
    //    }

    //public byte numberModifyer(int statToFind,int levelOfStat)
    //{
    //    byte byteToReturn = (byte)(levelOfStat * 0.4f); // Produces the numbers 1-2 weakest , 3-4 normal, 5-7 Stronger, 8-9 very strong, 10+ strongest stat!

    //    if (byteToReturn > statToReturn[statToFind].Count)
    //    {
    //        byteToReturn = (byte)(statToReturn[statToFind].Count - 1);
    //    }

    //    if (byteToReturn < 0)
    //    {
    //        byteToReturn = 0;
    //    }

    //    return byteToReturn;
    //}
 }


