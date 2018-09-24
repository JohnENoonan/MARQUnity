using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// class attached to puzzle piece buttons in puzzle grp. Has on click functions
public class puzzlePieceOnClick : MonoBehaviour {

    string number;
    public Sprite frontimg, backimg;

	// Use this for initialization
	void Awake () {
        
    }

    public void initPuzzlePiece()
    {
        number = gameObject.name.Split('_')[0];
        frontimg = Resources.Load<Sprite>("Puzzles/" + number + "_front");
        backimg = Resources.Load<Sprite>("Puzzles/" + number + "_back");

        gameObject.GetComponent<Button>().onClick.AddListener(onClick);
        Debug.Log(number + " initialized");
    }

    // unlock the puzzle piece 
    public void unlockPiece()
    {
        if (frontimg == null)
        {
            initPuzzlePiece();
        }
        gameObject.GetComponent<Image>().sprite = frontimg;
    }
	
    // onclick if clue is unlocked flip puzzle
	void onClick()
    {
        Image img = gameObject.GetComponent<Image>();
        string spriteName = img.sprite.name;
        // if it is unlocked
        Debug.Log(number + " clicked: " + spriteName);
        if (!spriteName.EndsWith("grey"))
        {
            if (spriteName.EndsWith("front"))
            {
                img.sprite = backimg;
                gameObject.transform.SetSiblingIndex(2);
            }
            else
            {
                img.sprite = frontimg;
                gameObject.transform.SetSiblingIndex(0);
            }
        }
    }
}
