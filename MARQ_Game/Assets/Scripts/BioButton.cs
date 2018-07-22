using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// script attached t obuttons in bio page. Used to display correct image on scroller
public class BioButton : MonoBehaviour {

    GameObject frontObj, backObj;
    Sprite frontimg, backimg, lockedimg;
    string searcher;
    string front_text = "_stat_front";
    string back_text = "_stat_back";
    // get the image objects
    void Awake () {
        // image container
        GameObject temp = gameObject.transform.parent.parent.GetChild(1).GetChild(0).gameObject;
        frontObj = temp.transform.GetChild(0).gameObject;
        backObj = temp.transform.GetChild(1).gameObject;
        searcher = gameObject.name.Split('_')[0];
        frontimg = Resources.Load<Sprite>("SearcherStats/" + searcher + front_text);
        backimg = Resources.Load<Sprite>("SearcherStats/" + searcher + back_text);
        lockedimg = Resources.Load<Sprite>("SearcherStats/locked_stat");
    }

    // on click update images in slider to this searcher or to locked
    public void updateImages()
    {
        if (GameControl.control.searcherIsUnlocked(searcher))
        {
            frontObj.GetComponent<Image>().sprite = frontimg;
            backObj.GetComponent<Image>().sprite = backimg;
        }
        else
        {
            frontObj.GetComponent<Image>().sprite = lockedimg;
            backObj.GetComponent<Image>().sprite = lockedimg;
        }
    }
	

}
