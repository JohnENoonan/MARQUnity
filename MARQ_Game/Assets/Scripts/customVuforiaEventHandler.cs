using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

// event handler for target found on vuforia
public class customVuforiaEventHandler : DefaultTrackableEventHandler{


    //VuforiaValidation validator;

    protected override void Start()
    {
        base.Start();
        //validator = new VuforiaValidation();
    }


    override protected void OnTrackingFound()
    {
        base.OnTrackingFound();
        //TODO
        // instead of each one having its own validator make one validator and let it do work
        // handle events for scanning
        Debug.Log("About to call handlescan from controller");
        CameraControl.control.validator.handleScan(mTrackableBehaviour.TrackableName);
        //validator.handleScan(mTrackableBehaviour.TrackableName);
    }

}


