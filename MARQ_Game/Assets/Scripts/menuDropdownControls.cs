using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

/*
    This script is attached to the canvas object in mainMenuScene 
*/
public class menuDropdownControls : MonoBehaviour {

    GameObject dropPanel; // panel that houses all the buttons beside the initial one
    //GameObject textInput;
    GameObject questionPanel;
    GameObject textInput;

	// init ui elements
	void Awake () {
        // get drop down panel
        GameObject tempobj = gameObject.transform.GetChild(3).gameObject;
        dropPanel = tempobj.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        // get question elements
        questionPanel = gameObject.transform.GetChild(4).gameObject;
        textInput = questionPanel.transform.GetChild(0).gameObject;
        Debug.Assert(textInput.name == "textInput");
    }

    // when top button is clicked reverse dropdown status
    public void toggleMenuPanel()
    {
        dropPanel.SetActive(!dropPanel.activeSelf);
    }

	// onclick camera btn
    public void gotoCamera()
    {
        // start vuforia
        VuforiaRuntime.Instance.InitVuforia();
        // write needed data
        //PlayerPrefs.SetInt("index", GameControl.control.getIndex());
        //if (GameControl.control.getEvent(GameControl.control.getIndex()).type != "qr question")
        //{
        //    PlayerPrefs.DeleteKey("answer");
        //}
        //else
        //{
        //    PlayerPrefs.SetString("answer", GameControl.control.getEvent(GameControl.control.getIndex()).answer);
        //}
        // set main to inactive, note this script is attached to canvas
        gameObject.SetActive(false);
        // go to camera scene
        SceneManager.LoadScene("cameraScene");
    }

    // onclick repeat btn, go back to first dialogue element leading to the question
    public void repeatDialogue()
    {
        int i = GameControl.control.getIndex();
        while (i > 0) // find first instance of not dialogue
        {
            string oldSS = GameControl.control.getEvent(i).image.Split('_')[0];
            i--;
            // if instance is not dialogue
            if (GameControl.control.getEvent(i).type != "dialogue")
            {
                break;
            }
            string newSS = GameControl.control.getEvent(i).image.Split('_')[0];
            // if has gone back to a previous searcher
            if (oldSS != newSS)
            {
                break;
            }
        }
        if (i != 0) { i++; } // move one past to get to dialogue
        // update content
        GameControl.control.setIndex(i);
        GameControl.control.setUIElements();
        GameControl.control.toggleRepeat();
        // hide any question
       questionPanel.SetActive(false);
        // repeat btn is hidden on click from built in
    }


    // called on submission of text
    public void submitText()
    {
        TMP_InputField textobj = textInput.GetComponent<TMP_InputField>();
        // validate text
        // if correct
        if (GameControl.control.validateAnswer(textobj.text))
        {
            // move to next event and update UI
            GameControl.control.setIndex(GameControl.control.getIndex() + 1);
            GameControl.control.setUIElements();
            // set text input to inactive
            textobj.text = "";
            textInput.SetActive(false);
            // set repeat to inactive
            GameControl.control.toggleRepeat();
        }
        else // if wrong give wrong answer text
        {
            GameControl.control.setDialogue(GameControl.control.getEvent(GameControl.control.getIndex()).wrong);
            GameControl.control.isWrong = true;
        }
    }
}
