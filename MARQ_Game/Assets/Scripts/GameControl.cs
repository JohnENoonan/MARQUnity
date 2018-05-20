using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

/*
 * This class serves as the games state saving device. It is a Singleton Object that must be present
 * in every scene
*/
public class GameControl : MonoBehaviour {

    public static GameControl control; // singleton
    public int team;
    private string flowFilename = "dummyData.json";
    private string cluesFliename = "clues.json";
    GameEventCollection events; // array of events that will occur
    private int index = 0; // index in the array of events


    // get the contents of filename, return as string
    private string getJSON(ref string filename)
    {
        string path = Application.dataPath + "/Data/" + filename;
        Debug.Log("File path is " + path);
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
        // load data
        events = JsonUtility.FromJson<GameEventCollection>(getJSON(ref flowFilename));
        //events.printContents();
	}

    // try and move to next event in queue
    public void nextEvent()
    {
        if (index < 0) { index = 0; }
        else if (events.get(index).type == "dialogue")
        {
            index++; // move to next event
            // if it's dialogue just move on, otherwise need to check
            if (events.get(index).type != "dialogue") 
            {
                // show repeat dialogue option
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
                var repeat = document.getElementById("repeatDiv");
                document.getElementById("questionDiv").hidden = false;
                repeat.hidden = false;
                document.getElementById("repeatText").innerHTML = "Repeat";
                repeat.style.width = "100px";
                repeat.style.height = "100px";
                repeat.style.background = "red";
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
