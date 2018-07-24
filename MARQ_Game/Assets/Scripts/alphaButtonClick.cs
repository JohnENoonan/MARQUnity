using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// this class is attached to buttons that only should be clicked on the non alpha image
public class alphaButtonClick : MonoBehaviour {

    public float AlphaThreshold = 0.1f;

    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
    }
}

