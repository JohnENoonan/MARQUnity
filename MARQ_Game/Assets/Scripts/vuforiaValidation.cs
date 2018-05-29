using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class VuforiaValidation {

    Hashtable map;

    public VuforiaValidation()
    {
        //get badge data
        string badgeData = JsonHelper.getFileString("badges.txt");
        string[] data = badgeData.Split('\n');
        foreach (string line in data)
        {
            Debug.Log("line in badges: " + line);
        }
        parseToMap(data);
        //string path = Application.streamingAssetsPath + "/badges.txt.txt" ;
        ////Debug.Log("File path is " + path);
        //if (File.Exists(path))
        //{
        //    map = new Hashtable();
        //    string[] data = File.ReadAllLines(path);
            
        //    parseToMap(data);
        //}
        //else
        //{
        //    Debug.LogError("Could not find file \"badges\"");
        //}
    }

    private void parseToMap(string[] data)
    {
        foreach (string line in data)
        {
            string[] split = line.Split(',');
            map.Add(split[0], split[1]);
        }
    }

    public bool validateAnswer(string input, string answer)
    {
        string[] answers = answer.Split(new[] { "||" }, StringSplitOptions.None);
        foreach (string ans in answers)
        {
            Debug.Log("comparing input: " + input + " to answer: " + ans);
            if (ans == input) // if answers match it is a correct solution
            {
                return true;
            }
        }
        return false;
    }

    // called when an image is found
    public void handleScan(string input)
    {
        // if looking for an answer
        if (PlayerPrefs.HasKey("answer"))
        {
            Debug.Log("answer is " + validateAnswer(input, PlayerPrefs.GetString("answer")));
            if (validateAnswer(input, PlayerPrefs.GetString("answer")))
                PlayerPrefs.SetInt("correct", 1);
        }
        else if (map.ContainsKey(input)) // see if it is a newfound badge or event
        {
            Debug.Log("badge: " + input + " found");
            if ((string)map[input] != "found")
            {
                Debug.Log("badge: " + input + " is new");
                map[input] = "found";
                string badge = "";
                if (PlayerPrefs.HasKey("received"))
                {
                    badge = PlayerPrefs.GetString("received");
                }
                PlayerPrefs.SetString("received", badge + '|' + input);
                Debug.Log("badges in playerprefs: " + PlayerPrefs.GetString("received"));
            }
            
        }
        else
        {
            PlayerPrefs.SetInt("correct", 0);
            Debug.Log("QR code is neither the answer nor a badge");
        }
    }

    
}
