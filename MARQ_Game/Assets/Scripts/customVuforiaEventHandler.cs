using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

// event handler for target found on vuforia
public class customVuforiaEventHandler : DefaultTrackableEventHandler{

    override protected void OnTrackingFound()
    {
        base.OnTrackingFound();
        // handle events for scanning
        Debug.Log("About to call handlescan from controller");
        CameraControl.control.handleScan(mTrackableBehaviour.TrackableName);
        //validator.handleScan(mTrackableBehaviour.TrackableName);
    }

}


