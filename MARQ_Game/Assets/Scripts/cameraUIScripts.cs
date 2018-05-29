using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;
using UnityEngine.UI;

public class cameraUIScripts : MonoBehaviour {

	public void backBtn()
    {
        // turn off vuforia camera
        CameraDevice.Instance.Stop();
        CameraDevice.Instance.Deinit();
        //gameObject.transform.parent.transform.parent.gameObject.SetActive(false);
        // re-enable scene
        // go back to main scene
        PlayerPrefs.DeleteKey("index");
        SceneManager.LoadScene("mainMenuScene");
        
        Camera.main.enabled = true;
        GameControl.control.getEvent(3).printObject();
        // toggle main menu on
        GameControl.control.gameObject.transform.GetChild(0).gameObject.SetActive(true);      

    }
}
