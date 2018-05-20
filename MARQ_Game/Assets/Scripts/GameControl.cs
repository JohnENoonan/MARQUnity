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
        // load data
        events = JsonUtility.FromJson<GameEventCollection>(getJSON(ref flowFilename));
        // load reapeat UI btn
        Transform canvas = GameObject.Find("Canvas").transform;
        repeat = canvas.GetChild(4).gameObject;
        Debug.Assert(repeat.name == "repeat btn");
        Transform ssGrp = canvas.GetChild(1).gameObject.transform;
        dialogue = ssGrp.GetChild(2).gameObject;
        image = ssGrp.GetChild(1).gameObject.GetComponent<Image>();
        Debug.Log("image = " + image.name);
        Debug.Assert(dialogue.name == "ss text");
        // set ui to first event
        setUIElements();
        //Debug.Assert(image.name == "ss image");
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

    // using the index set the text and image elements 
    public void setUIElements()
    {
        dialogue.GetComponent<TextMeshProUGUI>().SetText(events.get(index).text);
        // if need to change image
        Debug.Log("currimage is \"" + image.name + "\" which should become \"" + events.get(index).image + "\"");
        if (image.name != events.get(index).image)
        {
            image.sprite = Resources.Load<Sprite>("CharImages/" + events.get(index).image);
        }
        else { Debug.Log("if was false"); }
        
        
    }

    // try and move to next event in queue
    public void nextEvent()
    {
        Debug.Log("Clicked btn");
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
                // show answer boxes according to event
                switch (events.get(index).type)
                {
                    case "number question":
                        Debug.Log("num question");
                        
                        break;
                    case "text question":
                        Debug.Log("text question");
                       
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
        /*
         * next(){
            if (this.index < 0){
              this.index = 0;
            }
            // if on dialogue can just move on
            else if (this.getType() == "dialogue"){
              this.index++;
              // if current is not dialogue
              if (this.getType() != "dialogue"){
                //give option to repeat dialogue
                
                // creat appropriate forms
                switch(this.getType()){
                  case "number question":
                    console.log("num question");
                    this.handleNumberQ();
                    break;
                  case "text question":
                    console.log("text question");
                    this.handleTextQ();
                    break;
                  case "cite question":
                    console.log("cite question");
                    break;
                  case "qr question":
                    console.log("qr question");
                    break;
                }
              }
            }
          }
         */
    }

}
