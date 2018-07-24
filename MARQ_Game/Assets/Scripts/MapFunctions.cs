using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class is attached to the map buttons that select which floor to look at
public class MapFunctions : MonoBehaviour {

    GameObject floorImg; // image of the floor map
    string floor; // name of floor

    void Awake()
    {
        floor = gameObject.name;
        GameObject panel = gameObject.transform.parent.parent.gameObject;
        Debug.Assert(panel.name == "map content");
        floorImg = panel.transform.GetChild(2).gameObject;
        Debug.Assert(floorImg.name == "individual floor");
    }

    // attached to the invisible buttons that select what floor to view
    public void floorSelection()
    {
        // toggle elements
        floorImg.SetActive(true);
        gameObject.transform.parent.gameObject.SetActive(false); // turn off cross section and buttons
        Sprite floorSprite = Resources.Load<Sprite>(floor);
        // assign the floors sprite
        floorImg.GetComponent<UnityEngine.UI.Image>().sprite = floorSprite;
    }
}
