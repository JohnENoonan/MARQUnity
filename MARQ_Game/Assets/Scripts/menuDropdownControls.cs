using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class menuDropdownControls : MonoBehaviour {

    GameObject dropPanel; // panel that houses all the buttons beside the initial one
    //GameObject textInput;
    GameObject questionPanel;
    GameObject textInput;

	// init ui elements
	void Awake () {
        // get drop down panel
        GameObject tempobj = gameObject.transform.GetChild(3).gameObject;
        //Debug.Log("menubar = " + tempobj.name);
        dropPanel = tempobj.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        //Debug.Log("panel = " + dropPanel.name);
        // get question elements
        questionPanel = gameObject.transform.GetChild(5).gameObject;
        Debug.Log(questionPanel.name);
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
        // go to camera scene
        SceneManager.LoadScene("cameraScene");
    }
    // onclick repeat btn
    public void repeatDialogue()
    {
        int i = GameControl.control.getIndex();
        while (i > 0) // find first instance of not dialogue
        {
            i--;
            if (GameControl.control.getEvent(i).type != "dialogue")
            {
                break;
            }
        }
        if (i != 0) { i++; } // move one past to get to dialogue
        // update content
        GameControl.control.setIndex(i);
        GameControl.control.setUIElements();
        // hide any question
       questionPanel.SetActive(false);
        // repeat btn is hidden on click from built in
    }

    // called on submission of text
    public void submitText()
    {
        TMP_InputField textobj = textInput.GetComponent<TMP_InputField>();
        Debug.Log(textobj.text);
        // validate text
        // if correct
        if (GameControl.control.validateAnswer(textobj.text))
        {
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
