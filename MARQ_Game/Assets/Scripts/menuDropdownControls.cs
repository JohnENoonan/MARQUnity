using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.SceneManagement;

public class menuDropdownControls : MonoBehaviour {

    GameObject dropPanel; // panel that houses all the buttons beside the initial one

	// init dropPanel 
	void Awake () {
        GameObject tempobj = gameObject.transform.GetChild(2).gameObject;
        Debug.Log("menubar = " + tempobj.name);
        dropPanel = tempobj.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        Debug.Log("panel = " + dropPanel.name);
    }

    // when top button is clicked reverse dropdown status
    public void toggleMenuPanel()
    {
        dropPanel.SetActive(!dropPanel.activeSelf);
    }
	
    public void gotoCamera()
    {
        // start vuforia
        VuforiaRuntime.Instance.InitVuforia();
        // go to camera scene
        SceneManager.LoadScene("cameraScene");
    }
	

}
