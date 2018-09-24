using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// class attached to puzzle grp, used 
public class puzzleController : MonoBehaviour {

    public GameObject helpGameObject, answerTextInput;
    TextMeshProUGUI helpText;
    void Awake()
    {
        helpText = helpGameObject.GetComponent<TextMeshProUGUI>();
    }

    // see if given correct answer
    public void evaluateFinalAnswer()
    {
        Debug.Log("in eval");
        string input = answerTextInput.GetComponent<TMP_InputField>().text;
        if (input == "1976") // this is hard coded, needs to be able to accept all permutations
        {
            Debug.Log("got right answer");
            GameControl.control.handleFinalAnswer();
        }
        else
        {
            // give negative feedback
        }

    }
        

    // assign text to the help button
    public void setHelpText(string input)
    {
        helpText.SetText(input);
    }
}
