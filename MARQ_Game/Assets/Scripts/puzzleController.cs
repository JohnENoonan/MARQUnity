using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// class attached to puzzle grp, used 
public class puzzleController : MonoBehaviour {

    public GameObject helpGameObject;
    TextMeshProUGUI helpText;
    void Awake()
    {
        helpText = helpGameObject.GetComponent<TextMeshProUGUI>();
    }

    public void setHelpText(string input)
    {
        helpText.SetText(input);
    }
}
