using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*
 * This static class has methods to aid reading files. Mostly used to read script. Works when playing on android or testing on PC
*/
public static class JsonHelper
{
    // regardless of what is environment return data from file as string
    public static string getFileString(string filename)
    {
        string filePath = Application.streamingAssetsPath + "/" + filename;
        Debug.Log("attempting to open " + filePath);
        string jsonString;

        if (Application.platform == RuntimePlatform.Android) //Need to extract file from apk first
        {
            WWW reader = new WWW(filePath);
            while (!reader.isDone) { }

            jsonString = reader.text;
        }
        else
        {
            jsonString = File.ReadAllText(filePath);
        }
        return jsonString;
    }
}
