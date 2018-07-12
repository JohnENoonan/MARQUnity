using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// serves as the constant class that controls the camera scene, primarily incharge of validating QR codes
public class CameraControl : MonoBehaviour {

    public static CameraControl control;
    Hashtable map; //maps image name to the badge
    GameObject textObj, imgObj, ssGrp;


    void Awake()
    {
        if (control == null)
        { // if this instance is the first
            control = this;
        }
        else if (control != this)
        { // if object is not the one destroy it
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);

        //get badge data
        string badgeData = JsonHelper.getFileString("badges.txt");
        string[] data = badgeData.Split('\n');
        map = new Hashtable();
        parseToMap(data);
        Debug.Log("Game team is " + GameControl.control.team);
        Debug.Log("Dialogue is: \"" + GameControl.control.getEvent(GameControl.control.getIndex()).text + "\"");
        Debug.Log("Created Camera controller");
        // get UI elements
        Transform canvas = GameObject.Find("Canvas").transform;
        ssGrp = canvas.GetChild(0).Find("ss text bg").gameObject;
        Debug.Assert(ssGrp.name == "ss text bg");
        textObj = ssGrp.transform.Find("ss text").gameObject;
        imgObj = ssGrp.transform.Find("ss image").gameObject;
        ssGrp.SetActive(false);
    }

    // helper to init the map from target name to badge name
    private void parseToMap(string[] data)
    {
        foreach (string line in data)
        {
            string[] split = line.Split(',');
            map.Add(split[0], split[1]);
            //Debug.Log("added map[" + split[0] + "] = " + split[1]);
        }
    }

    // specific to answering QR questions
    public bool validateAnswer(string input)
    {
        string answer = GameControl.control.getCurrAnswer();
        answer = answer.ToLower();
        if (answer == input)
        {
            return true;
        }
        return false;
    }

    IEnumerator fadeMessage()
    {
        // fade
        float seconds = 8;
        //Wait for 4 seconds
        yield return new WaitForSeconds(seconds);

        ssGrp.SetActive(false);
    }

    public void giveFeedback(string message)
    {
        ssGrp.SetActive(true);
        //Debug.Log("image is " + GameControl.control.getCurrEvent().image);
        imgObj.GetComponent<Image>().sprite = Resources.Load<Sprite>(
            "CharImages/" + GameControl.control.getCurrEvent().image);
        textObj.GetComponent<TextMeshProUGUI>().SetText(message);
        StartCoroutine(fadeMessage());
    }

    private int getQRQuestion()
    {
        int i = GameControl.control.getIndex();
        while(GameControl.control.getEvent(i).type != "qr question")
        {
            ++i;
        }
        return i;
    }

    // called when an image is found
    public void handleScan(string input)
    {
        Debug.Log("Scan recieved '" + input + "'");
        // check if they found a badge
        if (map.ContainsKey(input))
        {
            // if is a new badge to players
            string badgename = map[input].ToString();
            badgename = badgename.Remove(badgename.Length - 1);
            if (!GameControl.control.hasBadge(badgename))
            {
                // add badge
                GameControl.control.addBadge(badgename);
                // give message
                string msg = "Congratulations, you found a new Badge! For more information go ahead and click it." +
                      "You can now find it in the badge book.";
                giveFeedback(msg);
            }
            // otherwise it has been found already, just show the model
        }
        // if it is not a badge it must be answer to some question
        else
        {
            // if this is the correct answer
            if (validateAnswer(input))
            {
                // get the qr question event
                
                int index = getQRQuestion();
                Debug.Log("Set index to: " + index);
                Debug.Assert(GameControl.control.getEvent(index).answer == input);
                giveFeedback(GameControl.control.getEvent(index+1).text);
                GameControl.control.handleQRAnswer();
            }
            else
            {
                // handle incorrect
            }
        }

        // if looking for an answer
        //if (PlayerPrefs.HasKey("answer"))
        //{
        //    Debug.Log("answer is " + validateAnswer(input, PlayerPrefs.GetString("answer")));
        //    if (validateAnswer(input, PlayerPrefs.GetString("answer")))
        //        PlayerPrefs.SetInt("correct", 1);
        //}
        //else if (map.ContainsKey(input)) // see if it is a newfound badge or event
        //{
        //    Debug.Log("badge: " + input + " found");
        //    if ((string)map[input] != "found")
        //    {
        //        Debug.Log("badge: " + input + " is new");
        //        map[input] = "found";
        //        string badge = "";
        //        if (PlayerPrefs.HasKey("received"))
        //        {
        //            badge = PlayerPrefs.GetString("received");
        //        }
        //        PlayerPrefs.SetString("received", badge + '|' + input);
        //        Debug.Log("badges in playerprefs: " + PlayerPrefs.GetString("received"));
        //    }

        //}
        //else
        //{
        //    PlayerPrefs.SetInt("correct", 0);
        //    Debug.Log("QR code is neither the answer nor a badge");
        //}
    }

}
