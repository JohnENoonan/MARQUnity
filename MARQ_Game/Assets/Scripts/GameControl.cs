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
    public GameObject repeat, dialogue, nextDialogue, questionPanel, 
                      textInput, qrInput, badgeCount, badgePanel, bioButtons;
    Image ssImage, nameTag;

    // flags and variables
    public bool isWrong = false;
    int answerIndex; // index of the question that needs to be answered
    List<string> badges; // list of badges players have collected
    List<string> searchers;
    
    ///////// get and set functions ////////
    // get index that curr event is 
    public int getIndex() { return index; }
    // get answer to most recent answer
    public string getCurrAnswer() { return currAnswer; }
    // set game index
    public void setIndex(int i) { index = i; }
    // get event at an index
    public GameEvent getEvent(int index) { return events.get(index); }
    // get current event
    public GameEvent getCurrEvent() { return events.get(index); }
    // set dialogue of super searcher speech 
    public void setDialogue(string input)
    {
        dialogue.GetComponent<TextMeshProUGUI>().SetText(input);
    }
    // bool to see if player has a badge based on the name. Must match exactly
    public bool hasBadge(string badgeName){ return badges.Contains(badgeName); }
    // add badge to players collection. Should be called from scan
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
    
    // add a searcher to players collection. Needed to access SS bio
    public void addSearcher(string searcher)
    {
        searchers.Add(searcher);
        // set bio button to unlocked image
        foreach (Transform button in bioButtons.transform)
        {
            if (button.name.Equals(searcher + "_btn")){
                button.GetComponent<Image>().sprite = Resources.Load<Sprite>("CharImages/" + searcher + "_unlocked");
                Debug.Log("unlocked " + searcher);
            }
        }
    }

    // determine if a searcher is unlocked by name
    public bool searcherIsUnlocked(string searcher)
    {
        return searchers.Contains(searcher);
    }

    // helper called to grab all needed parts (GameObject) at awake
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
        // get dialogue elements
        Transform ssGrp = canvas.GetChild(1).gameObject.transform;
        ssImage = ssGrp.GetChild(1).gameObject.GetComponent<Image>();
        nameTag = ssImage.transform.GetChild(0).gameObject.GetComponent<Image>();
        Debug.Log(nameTag.name);
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
        if (ssImage.sprite.name != events.get(index).image)
        {
            ssImage.sprite = Resources.Load<Sprite>("CharImages/" + events.get(index).image);
            // update name tag
            string name = events.get(index).image.Split('_')[0];
            nameTag.sprite = Resources.Load<Sprite>("CharImages/" + name + "_name");
            if (!searchers.Contains(name))
            {
                addSearcher(name);
            }
        }
        // update badge count
        badgeCount.GetComponent<TMP_Text>().text = badges.Count.ToString() + "/10";
    }


    // used to turn repeat btn off or on. Does inverse of next dialogue button
    public void toggleRepeat()
    {
        repeat.SetActive(!repeat.activeSelf);
        nextDialogue.SetActive(!nextDialogue.activeSelf);
    }

    // given a text input determine if it is the answer to the current question
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

    // when a question is ready, init and show related elements needed to answer
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

    // try and move to next event in queue in script
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
