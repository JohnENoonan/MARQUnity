using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


/*
 * This class describes events as given by the script. Reads events from the json file and parses keys into appropriate 
 * variables. Each individual GameEvent is one JSON object in the script.
 */ 

[Serializable]
public class GameEvent
{
    public string type; // type of event
    public string image; // SS image to show
    public string text; // text to display in super searcher text box
    public string answer; // answer to question if the type is a question type
    public string wrong; // text to display on wrong answer

    // returns true if input is the answer for this event
    public bool validateAnswer(string input)
    {
        input = input.ToLower();
        answer = answer.ToLower();
        // get all possible answers from script
        string[] answers = answer.Split(new[] { "||" }, StringSplitOptions.None);
        foreach(string ans in answers)
        {
            Debug.Log("comparing input: " + input + " to answer: " + ans);
            //Debug.Log(input.Length.ToString() + " : " + input.Equals("accept").ToString() + " : " + (input == "accept").ToString());
            if (ans == input) // if answers match it is a correct solution
            {
                return true; 
            }
            else if (input == "accept") // return true if all answers are accepted, this is for debugging
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

// GameEventCollection is an abstraction used to store GameEvents. 
// This class exist so the parsing from json can be done serialized

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
