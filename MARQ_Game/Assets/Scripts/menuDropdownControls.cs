using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

/*
    This script is attached to the canvas object in mainMenuScene. Used for interactive updates on UI elements mostly independent of GameControl
*/
public class menuDropdownControls : MonoBehaviour {

    public GameObject dropPanel; // panel that houses all the buttons beside the initial one
    public GameObject questionPanel;
    public GameObject textInput;

	void Awake () {
        // check for correct panels
        Debug.Assert(questionPanel.name == "question panel");
        Debug.Assert(textInput.name == "textInput");
    }
    
    // function to set all children to false
    public void setChildrenActivity(GameObject parent)
    {
        bool activity = false;
        foreach (Transform child in parent.transform)
        {
            GameObject cgameobject = child.gameObject;
            cgameobject.SetActive(activity);
        }
        parent.SetActive(false);
    }

    // when dropdown button is clicked reverse dropdown status 
    public void toggleMenuPanel()
    {
        dropPanel.SetActive(!dropPanel.activeSelf);
    }

	// onclick camera btn
    public void gotoCamera()
    {
        // start vuforia
        VuforiaRuntime.Instance.InitVuforia();
        // turn off canvas leaving only gameobject container with nothing to show
        gameObject.SetActive(false);
        // go to camera scene
        SceneManager.LoadScene("cameraScene");
    }

    // onclick repeat btn, go back to first dialogue element leading to the question
    // attached to repeat button. If there is currently a wrong answer, just go back to the question
    public void repeatDialogue()
    {
        int i = GameControl.control.getIndex();
        // if a wrong answer is given reset super searcher text to the question
        if (GameControl.control.isWrong)
        {
            GameControl.control.setDialogue(GameControl.control.getEvent(i).text);
            GameControl.control.isWrong = false;
            return;
        }
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
        GameControl.control.contentBox.SetActive(false);
        GameControl.control.toggleRepeat();
        // hide any question
       questionPanel.SetActive(false);
        // repeat btn is hidden on click from built in
    }


    // called on submission of text, used in TMP object
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
            GameControl.control.contentBox.SetActive(false);
        }
        else // if wrong give wrong answer text
        {
            GameControl.control.setDialogue(GameControl.control.getEvent(GameControl.control.getIndex()).wrong);
            GameControl.control.isWrong = true;
        }
    }
}
