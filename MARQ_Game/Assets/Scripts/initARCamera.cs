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
        // creat badge geo
        //Mesh holderMesh = new Mesh();
        //ObjImporter newMesh = new ObjImporter();
        //holderMesh = newMesh.ImportFile("C:/Users/cvpa2/Desktop/ng/output.obj");

        //MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
        //MeshFilter filter = gameObject.AddComponent<MeshFilter>();
        //filter.mesh = holderMesh;
        //foreach (TrackableBehaviour tb in TrackerManager.Instance.GetStateManager().GetTrackableBehaviours())
        //{
        //    Debug.Log("Trackable with name " + tb.TrackableName + " found");
        //    // add behavior script
        //    tb.gameObject.AddComponent<customVuforiaEventHandler>();
        //    //tb.gameObject.transform.ch
        //    // Add MeshCollider to tb.gameObject
        //}
    }
	
}
