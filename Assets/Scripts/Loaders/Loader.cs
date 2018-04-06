using UnityEngine;
using System.Collections;
using System;
using System.IO;

public abstract class Loader : ILoader
{
    string path;
    string jsonString;
    protected JSONObject jsonObject;

    public abstract void init();

    public void loadJson(string filePath)
    {
        path = Application.streamingAssetsPath + filePath;

        #if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_IOS
        jsonString = File.ReadAllText(path);
        #endif

        #if UNITY_ANDROID
        WWW webReader = new WWW(path);
        while (!webReader.isDone) { };
        jsonString = webReader.text;
        #endif

        jsonObject = new JSONObject(jsonString);
    }

    public JSONObject targetObjectFromJson(string nameOfTarget)
    {
        return jsonObject[nameOfTarget];
    }

    public JSONObject targetObjectFromJson(byte ownersId, string nameOfTarget)
    {
        return jsonObject[ownersId][nameOfTarget];
    }
}
