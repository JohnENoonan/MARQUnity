using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.SceneManagement;

public class menuDropdownControls : MonoBehaviour {

    GameObject dropPanel; // panel that houses all the buttons beside the initial one

	// init dropPanel 
	void Awake () {
        GameObject tempobj = gameObject.transform.GetChild(3).gameObject;
        Debug.Log("menubar = " + tempobj.name);
        dropPanel = tempobj.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        Debug.Log("panel = " + dropPanel.name);
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
        // repeat btn is hidden on click from built in
    }


}
