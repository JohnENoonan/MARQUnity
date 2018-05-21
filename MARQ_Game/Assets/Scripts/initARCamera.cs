using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class initARCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CameraDevice.Instance.Start();
        //gameObject.GetComponent<VuforiaBehaviour>().enabled = true;
        //gameObject.SetActive(true);
        foreach (TrackableBehaviour tb in TrackerManager.Instance.GetStateManager().GetTrackableBehaviours())
        {
            Debug.Log("Trackable with name " + tb.TrackableName + " found");
            // add behavior script
            tb.gameObject.AddComponent<customVuforiaEventHandler>();
            // Add MeshCollider to tb.gameObject
        }
    }
	
}
