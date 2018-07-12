using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using TMPro;
using System.Text.RegularExpressions;

/*
 * This class serves as the games state saving device. It is a Singleton Object that must be present
 * in every scene. It is used as the parent to perserve main UI
*/
public class GameControl : MonoBehaviour {

    public static GameControl control; // singleton
    public int team; // tema number used for ordering events
    private string flowFilename = "script.json";
    private string cluesFliename = "clues.json";
    GameEventCollection events; // array of events that will occur
    private int index = 0; // index in the array of events
    private string currAnswer = null; // the answer to the current question, null if no answer is expected

    // ui elements that this affects
    GameObject repeat, dialogue, nextDialogue;
    Image image;
    GameObject questionPanel, textInput, qrInput, badgeCount, badgePanel;

    // flags and variables
    public bool isWrong = false;
    int answerIndex; // index of the question that needs to be answered
    List<string> badges; // list of badges players have collected
    List<string> searchers;
    
    // get and set functions
    public int getIndex() { return index; }
    public string getCurrAnswer() { return currAnswer; }
    public void setIndex(int i) { index = i; }
    public GameEvent getEvent(int index) { return events.get(index); }
    public GameEvent getCurrEvent() { return events.get(index); }
    public void setDialogue(string input)
    {
        dialogue.GetComponent<TextMeshProUGUI>().SetText(input);
    }
    public bool hasBadge(string badgeName){ return badges.Contains(badgeName); }

    public void addBadge(string newBadge){
        badges.Add(newBadge);
        Debug.Log("Added " + newBadge + " now has size " + badges.Count);
        // set badge to unlocked
        string badgeToLoad = string.Format("{0}_{1}", newBadge, "badge");
        // get badge to change image for
        foreach (Transform child in badgePanel.transform)
        {
            Debug.Log("comparing: '" + child.name + "' to actual : '" + badgeToLoad + "'");
            if (child.name.Equals(badgeToLoad, StringComparison.Ordinal)){
                child.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Badges/" + badgeToLoad);
                break;
            }
        }
    }
    
    public void addSearcher(string searcher)
    {
        searchers.Add(searcher);
        // handle UI stuff
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
        // load event data from file
        events = JsonUtility.FromJson<GameEventCollection>(JsonHelper.getFileString(flowFilename));
        // load reapeat UI btn
        Transform canvas = GameObject.Find("Canvas").transform;
        repeat = canvas.GetChild(2).GetChild(2).gameObject;
        Debug.Assert(repeat.name == "repeat btn");
        // get dialogue elements
        Transform ssGrp = canvas.GetChild(1).gameObject.transform;
        dialogue = ssGrp.GetChild(2).gameObject;
        image = ssGrp.GetChild(1).gameObject.GetComponent<Image>();
        nextDialogue = canvas.GetChild(2).GetChild(1).gameObject;
        Debug.Assert(dialogue.name == "ss text");
        Debug.Assert(nextDialogue.name == "next text btn");
        // get counter element
        badgeCount = canvas.GetChild(0).transform.GetChild(2).transform.GetChild(1).
                              transform.GetChild(1).gameObject;
        Debug.Assert(badgeCount.name == "counter");
        // get badge book
        Transform badgeGrp = canvas.Find("badge grp");
        badgePanel = badgeGrp.GetChild(1).GetChild(0).gameObject;
        Debug.Assert(badgePanel.name == "book bg");
        // get question elements
        questionPanel = GameObject.Find("question panel");
        questionPanel.SetActive(false);
        textInput = questionPanel.transform.GetChild(0).gameObject;
        qrInput = questionPanel.transform.GetChild(1).gameObject;
        // init private variables
        badges = new List<string>();
        searchers = new List<string>();
        // set ui to first event
        setUIElements();
    }

    // initialization that enforces singleton and loads data
	void Awake () {
        // enforce singleton
        if (control == null)
        { // if this instance is the first
            Debug.Log("Made the one and only version of GameControl");
            control = this;
            loadData();
        }
        else if (control != this)
        { // if object is not the one destroy it
            Debug.Log("Going to destroy this");
            Destroy(gameObject);
        }        
        DontDestroyOnLoad(gameObject);
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
        // update badge count
        badgeCount.GetComponent<TMP_Text>().text = badges.Count.ToString() + "/10";
    }


    // used to turn repeat btn off or on
    public void toggleRepeat()
    {
        repeat.SetActive(!repeat.activeSelf);
        nextDialogue.SetActive(!nextDialogue.activeSelf);
    }

    public bool validateAnswer(string input) { return events.get(index).validateAnswer(input); }

    public void handleQRAnswer()
    {
        setIndex(answerIndex+1);
        //index++; // move to next event
        //nextEvent();
        setUIElements();
        toggleRepeat();
        qrInput.SetActive(false);
    }

    // this function deals with qr questions and is dealt with inside vuforia's DefaultTrackableEventHandler
    public void handleQRQuestion()
    {
        index = answerIndex;
        setUIElements();
    }

    public void prepareQuestion()
    {
        // show repeat dialogue option
        repeat.SetActive(true);
        nextDialogue.SetActive(false);
        questionPanel.SetActive(true);
        currAnswer = events.get(index).answer;
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
                Debug.Log("qr question, index : " + index);
                qrInput.SetActive(true);
                answerIndex = index;
                handleQRQuestion();
                break;
        }
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
            //TODO handle end of game
            index++; // move to next event
            // check to see if it is a new super searcher
            if (index > 0)
            {
                string currSearcher = events.get(index).image.Split('_')[0];
                if (currSearcher !=
                    events.get(index - 1).image.Split('_')[0])
                {
                    // is a new searcher, update bio page
                    addSearcher(currSearcher);
                }
            }
            setUIElements(); // set elements accordingly
            // if it's dialogue all is done, otherwise need to get answer
            if (events.get(index).type != "dialogue")
            {
                prepareQuestion();
            }
        }
        else if (events.get(index).type != "dialogue")
        {
            Debug.Log("Event is " + events.get(index).type + "and was " + events.get(index).type);
        }
    }

}
