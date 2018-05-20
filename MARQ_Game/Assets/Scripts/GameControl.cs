using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using TMPro;

/*
 * This class serves as the games state saving device. It is a Singleton Object that must be present
 * in every scene
*/
public class GameControl : MonoBehaviour {

    public static GameControl control; // singleton
    public int team; // tema number used for ordering events
    private string flowFilename = "dummyData.json";
    private string cluesFliename = "clues.json";
    GameEventCollection events; // array of events that will occur
    private int index = 0; // index in the array of events
    // ui elements that this affects
    GameObject repeat, dialogue;
    Image image;
    GameObject questionPanel, textInput, qrInput;
    // flags and variables
    public bool isWrong = false;
    




    public int getIndex() { return index; }
    public void setIndex(int i) { index = i; }
    public GameEvent getEvent(int index) { return events.get(index); }

    // get the contents of filename, return as string
    private string getJSON(ref string filename)
    {
        string path = Application.dataPath + "/Data/" + filename;
        //Debug.Log("File path is " + path);
        if (File.Exists(path))
        {
            return File.ReadAllText(path);
        }
        else
        {
            Debug.LogError("Could not find file \"" + filename + "\"");
            return "Broken data";
        }
    }

    private void loadData()
    {
        // assign team
        if (PlayerPrefs.HasKey("team")) { team = PlayerPrefs.GetInt("team"); }
        else // can't get pref, but this might be because of testing 
        {
            //TODO this is hard coded for testing, must be updated for game
            //Debug.LogError("Could not get team attribute");
            team = 0;
            Debug.Log("Hard coded team to be 0");
        }
        // load data from file
        events = JsonUtility.FromJson<GameEventCollection>(getJSON(ref flowFilename));
        // load reapeat UI btn
        Transform canvas = GameObject.Find("Canvas").transform;
        repeat = canvas.GetChild(4).gameObject;
        Debug.Assert(repeat.name == "repeat btn");
        // get dialogue elements
        Transform ssGrp = canvas.GetChild(1).gameObject.transform;
        dialogue = ssGrp.GetChild(2).gameObject;
        image = ssGrp.GetChild(1).gameObject.GetComponent<Image>();
        Debug.Assert(dialogue.name == "ss text");
        // get question elements
        questionPanel = GameObject.Find("question panel");
        questionPanel.SetActive(false);
        textInput = questionPanel.transform.GetChild(0).gameObject;
        qrInput = questionPanel.transform.GetChild(1).gameObject;
        // set ui to first event
        setUIElements();
    }

    // initialization that enforces singleton and loads data
	void Awake () {
        // enforce singleton
		if (control == null)
        { // if this instance is the first
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        { // if object is not the one destroy it
            Destroy(gameObject);
        }
        loadData();
    }

    public bool validateAnswer(string input)
    {
        return events.get(index).validateAnswer(input);
    }

    // using the index set the text and image elements 
    public void setUIElements()
    {
        setDialogue(events.get(index).text);
        // if need to change image
        if (image.sprite.name != events.get(index).image)
        {
            image.sprite = Resources.Load<Sprite>("CharImages/" + events.get(index).image);
        }        
    }

    // helper that sets char dialogue to input
    public void setDialogue(string input)
    {
        dialogue.GetComponent<TextMeshProUGUI>().SetText(input);
    }

    // used to turn repeat btn off or on
    public void toggleRepeat()
    {
        repeat.SetActive(!repeat.activeSelf);
    }

    // this function deals with qr questions and is dealt with inside vuforia's DefaultTrackableEventHandler
    public void handleQRQuestion(string input)
    {

    }

    // try and move to next event in queue
    public void nextEvent()
    {
        // if a wrong answer was given and then clicked, show question again
        if (isWrong)
        {
            setDialogue(events.get(index).text);
            isWrong = false;
        }
        if (index < 0) { index = 0; }
        else if (events.get(index).type == "dialogue")
        {
            index++; // move to next event
            setUIElements(); // set elements accordingly
            // if it's dialogue all is done, otherwise need to get answer
            if (events.get(index).type != "dialogue")
            {
                // show repeat dialogue option
                repeat.SetActive(true);
                questionPanel.SetActive(true);
                // show answer boxes according to event
                switch (events.get(index).type)
                {
                    // for each event set required element to active
                    case "text question":
                        Debug.Log("text question");
                        textInput.SetActive(true);
                        break;
                    case "cite question":
                        Debug.Log("cite question");
                        break;
                    case "qr question":
                        Debug.Log("qr question");
                        break;
                }
            }
        }
    }

}
