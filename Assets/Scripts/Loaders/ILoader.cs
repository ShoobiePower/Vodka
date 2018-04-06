using UnityEngine;
using System.Collections;

public interface ILoader
{

    void loadJson(string filePath);
    JSONObject targetObjectFromJson(string nameOfTarget);
    JSONObject targetObjectFromJson(byte ownersId, string nameOfTarget);
}
