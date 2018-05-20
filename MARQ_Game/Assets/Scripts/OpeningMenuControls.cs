using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpeningMenuControls : MonoBehaviour {

    // call on play btn
	public void playGame()
    {
        int team = GameObject.Find("Dropdown").GetComponent<Dropdown>().value;
        Debug.Log("set team to: " + team);
        PlayerPrefs.SetInt("team", team); // write team to playerprefs
        // load next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
