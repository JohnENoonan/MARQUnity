using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public static CameraControl control;
    public VuforiaValidation validator;

	void Awake()
    {
        if (control == null)
        { // if this instance is the first
            control = this;
        }
        else if (control != this)
        { // if object is not the one destroy it
            Destroy(gameObject);
        }
        // init validator
        validator = new VuforiaValidation();
        Debug.Log("Created Camera controller");
    }

    void Start()
    {
        //GameObject gamecontrol = GameObject.Find("GameControl");
        //while (GameControl.control == null) { }
        //Debug.Log("trying to get team: " + GameControl.control.team);
        //gamecontrol.GetComponent("GameControl");

    }

}
