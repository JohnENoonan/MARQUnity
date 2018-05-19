using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpeningMenuControls : MonoBehaviour {

    // call on play btn
	public void playGame()
    {
        GameControl.control.team = GameObject.Find("Dropdown").GetComponent<Dropdown>().value;
        Debug.Log("set team to: " + GameControl.control.team);
        // load next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
