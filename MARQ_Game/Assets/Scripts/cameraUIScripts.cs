using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;
using UnityEngine.UI;

// script attached to back button in camera scene to reinit main menu scene

public class cameraUIScripts : MonoBehaviour {

	public void backBtn()
    {
        // turn off vuforia camera
        CameraDevice.Instance.Stop();
        CameraDevice.Instance.Deinit();
        // go back to main scene
        SceneManager.LoadScene("mainMenuScene");
        // restart camera
        Camera.main.enabled = true;
        GameControl.control.getEvent(3).printObject();
        // toggle main menu on
        GameControl.control.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        GameControl.control.setUIElements();  
    }

    
}
