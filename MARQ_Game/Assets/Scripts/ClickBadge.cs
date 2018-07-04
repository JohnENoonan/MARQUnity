using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBadge : MonoBehaviour {

	void OnMouseDown()
    {
        Debug.Log("Clicked " + gameObject.name);
    }
}
