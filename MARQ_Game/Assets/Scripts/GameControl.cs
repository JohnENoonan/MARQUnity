using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class serves as the games state saving device. It is a Singleton Object that must be present
 * in every scene
*/
public class GameControl : MonoBehaviour {

    public static GameControl control; // singleton
    public int team;

	void Awake () {
		if (control == null)
        { // if this instance is the first
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        { // if object is not the one destroy it
            Destroy(gameObject);
        }
	}
	
}
