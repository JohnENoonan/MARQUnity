using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;



[Serializable]
public class GameEvent
{
    public string type;
    public string image;
    public string text;
    public string answer;
    public string wrong;

    // returns true if input is the answer for this event
    public bool validateAnswer(string input)
    {
        input = input.ToLower();
        // get all possible answers
        string[] answers = input.Split(new[] { "||" }, StringSplitOptions.None);
        foreach(string answer in answers)
        {
            if (answer == input) // if answers match it is a correct solution
            {
                return true; 
            }
        }
        return false;
    }

    public void printObject()
    {
        Debug.Log("Type: " + type + "\nImg: " + image + "\nText: " + text + "\nAns: " + answer + "\nWrong: " + wrong);
    }
}

[Serializable]
public class GameEventCollection
{
    public GameEvent[] Items; // array of all events

    public GameEvent get(int index)
    {
        return Items[index];
    }

    public void printContents()
    {
        foreach (GameEvent g in Items)
        {
            g.printObject();
        }
    }
}
