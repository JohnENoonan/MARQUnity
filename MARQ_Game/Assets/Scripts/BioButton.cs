using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BioButton : MonoBehaviour {

    GameObject frontObj, backObj;
    Sprite frontimg, backimg;
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
    }

    public void updateImages()
    {
        frontObj.GetComponent<Image>().sprite = frontimg;
        backObj.GetComponent<Image>().sprite = backimg;
    }
	

}
