using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

// event handler for target found on vuforia
public class customVuforiaEventHandler : DefaultTrackableEventHandler{


    VuforiaValidation validator;

    protected override void Start()
    {
        base.Start();
        validator = new VuforiaValidation();
    }


    override protected void OnTrackingFound()
    {
        base.OnTrackingFound();

        // handle events for scanning
        validator.handleScan(mTrackableBehaviour.TrackableName);
    }

}


