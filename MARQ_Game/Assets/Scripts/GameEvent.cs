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
        Debug.Log("validate recieved: " + input);
        Debug.Log("type: " + input.GetType());
        Debug.Assert(answer != null);
        answer = answer.ToLower();
        // get all possible answers
        string[] answers = answer.Split(new[] { "||" }, StringSplitOptions.None);
        foreach(string ans in answers)
        {
            Debug.Log("comparing input: " + input + " to answer: " + ans);
            if (ans == input) // if answers match it is a correct solution
            {
                return true; 
            }
            else if (ans == "accept") // return true if all answers are accepted
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
